using MediatR;
using ContentBlockService.Data;
using ContentBlockService.Data.Model;
using ContentBlockService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace ContentBlockService.Features.CallToActionContentBlocks
{
    public class AddOrUpdateCallToActionContentBlockCommand
    {
        public class AddOrUpdateCallToActionContentBlockRequest : IRequest<AddOrUpdateCallToActionContentBlockResponse>
        {
            public CallToActionContentBlockApiModel CallToActionContentBlock { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateCallToActionContentBlockResponse { }

        public class AddOrUpdateCallToActionContentBlockHandler : IAsyncRequestHandler<AddOrUpdateCallToActionContentBlockRequest, AddOrUpdateCallToActionContentBlockResponse>
        {
            public AddOrUpdateCallToActionContentBlockHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateCallToActionContentBlockResponse> Handle(AddOrUpdateCallToActionContentBlockRequest request)
            {
                var entity = await _context.CallToActionContentBlocks
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.CallToActionContentBlock.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.CallToActionContentBlocks.Add(entity = new CallToActionContentBlock() { TenantId = tenant.Id });
                }

                entity.Name = request.CallToActionContentBlock.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateCallToActionContentBlockResponse();
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
