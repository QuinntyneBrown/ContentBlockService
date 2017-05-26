using MediatR;
using ContentBlockService.Data;
using ContentBlockService.Data.Model;
using ContentBlockService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace ContentBlockService.Features.HeadlineContentBlocks
{
    public class RemoveHeadlineContentBlockCommand
    {
        public class RemoveHeadlineContentBlockRequest : IRequest<RemoveHeadlineContentBlockResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveHeadlineContentBlockResponse { }

        public class RemoveHeadlineContentBlockHandler : IAsyncRequestHandler<RemoveHeadlineContentBlockRequest, RemoveHeadlineContentBlockResponse>
        {
            public RemoveHeadlineContentBlockHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveHeadlineContentBlockResponse> Handle(RemoveHeadlineContentBlockRequest request)
            {
                var headlineContentBlock = await _context.HeadlineContentBlocks.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                headlineContentBlock.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveHeadlineContentBlockResponse();
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
