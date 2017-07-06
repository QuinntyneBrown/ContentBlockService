using MediatR;
using ContentBlockService.Data;
using ContentBlockService.Data.Model;
using ContentBlockService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace ContentBlockService.Features.MegaHeaderContentBlocks
{
    public class AddOrUpdateMegaHeaderContentBlockCommand
    {
        public class AddOrUpdateMegaHeaderContentBlockRequest : IRequest<AddOrUpdateMegaHeaderContentBlockResponse>
        {
            public MegaHeaderContentBlockApiModel MegaHeaderContentBlock { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateMegaHeaderContentBlockResponse { }

        public class AddOrUpdateMegaHeaderContentBlockHandler : IAsyncRequestHandler<AddOrUpdateMegaHeaderContentBlockRequest, AddOrUpdateMegaHeaderContentBlockResponse>
        {
            public AddOrUpdateMegaHeaderContentBlockHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateMegaHeaderContentBlockResponse> Handle(AddOrUpdateMegaHeaderContentBlockRequest request)
            {
                var entity = await _context.MegaHeaderContentBlocks
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.MegaHeaderContentBlock.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.MegaHeaderContentBlocks.Add(entity = new MegaHeaderContentBlock() { TenantId = tenant.Id });
                }

                entity.Name = request.MegaHeaderContentBlock.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateMegaHeaderContentBlockResponse();
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
