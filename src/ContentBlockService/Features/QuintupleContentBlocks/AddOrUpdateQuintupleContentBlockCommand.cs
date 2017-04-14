using MediatR;
using ContentBlockService.Data;
using ContentBlockService.Data.Model;
using ContentBlockService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace ContentBlockService.Features.QuintupleContentBlocks
{
    public class AddOrUpdateQuintupleContentBlockCommand
    {
        public class AddOrUpdateQuintupleContentBlockRequest : IRequest<AddOrUpdateQuintupleContentBlockResponse>
        {
            public QuintupleContentBlockApiModel QuintupleContentBlock { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateQuintupleContentBlockResponse { }

        public class AddOrUpdateQuintupleContentBlockHandler : IAsyncRequestHandler<AddOrUpdateQuintupleContentBlockRequest, AddOrUpdateQuintupleContentBlockResponse>
        {
            public AddOrUpdateQuintupleContentBlockHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateQuintupleContentBlockResponse> Handle(AddOrUpdateQuintupleContentBlockRequest request)
            {
                var entity = await _context.QuintupleContentBlocks
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.QuintupleContentBlock.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.QuintupleContentBlocks.Add(entity = new QuintupleContentBlock() { TenantId = tenant.Id });
                }

                entity.Name = request.QuintupleContentBlock.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateQuintupleContentBlockResponse();
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
