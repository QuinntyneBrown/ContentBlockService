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
    public class RemoveMegaHeaderContentBlockCommand
    {
        public class RemoveMegaHeaderContentBlockRequest : IRequest<RemoveMegaHeaderContentBlockResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveMegaHeaderContentBlockResponse { }

        public class RemoveMegaHeaderContentBlockHandler : IAsyncRequestHandler<RemoveMegaHeaderContentBlockRequest, RemoveMegaHeaderContentBlockResponse>
        {
            public RemoveMegaHeaderContentBlockHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveMegaHeaderContentBlockResponse> Handle(RemoveMegaHeaderContentBlockRequest request)
            {
                var megaHeaderContentBlock = await _context.MegaHeaderContentBlocks.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                megaHeaderContentBlock.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveMegaHeaderContentBlockResponse();
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
