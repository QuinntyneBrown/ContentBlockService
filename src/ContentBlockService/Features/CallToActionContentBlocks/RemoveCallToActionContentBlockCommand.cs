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
    public class RemoveCallToActionContentBlockCommand
    {
        public class RemoveCallToActionContentBlockRequest : IRequest<RemoveCallToActionContentBlockResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveCallToActionContentBlockResponse { }

        public class RemoveCallToActionContentBlockHandler : IAsyncRequestHandler<RemoveCallToActionContentBlockRequest, RemoveCallToActionContentBlockResponse>
        {
            public RemoveCallToActionContentBlockHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveCallToActionContentBlockResponse> Handle(RemoveCallToActionContentBlockRequest request)
            {
                var callToActionContentBlock = await _context.CallToActionContentBlocks.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                callToActionContentBlock.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveCallToActionContentBlockResponse();
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
