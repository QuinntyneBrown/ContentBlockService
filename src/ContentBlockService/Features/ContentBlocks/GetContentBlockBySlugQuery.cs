using ContentBlockService.Data;
using ContentBlockService.Features.Core;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ContentBlockService.Features.ContentBlocks
{
    public class GetContentBlockBySlugQuery
    {
        public class GetContentBlockBySlugRequest : IRequest<GetContentBlockBySlugResponse>
        {
            public string Slug { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetContentBlockBySlugResponse
        {
            public ContentBlockApiModel ContentBlock { get; set; }
        }

        public class GetContentBlockBySlugHandler : IAsyncRequestHandler<GetContentBlockBySlugRequest, GetContentBlockBySlugResponse>
        {
            public GetContentBlockBySlugHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetContentBlockBySlugResponse> Handle(GetContentBlockBySlugRequest request)
            {
                var contentBlock = await _context.ContentBlocks
                    .Include(x => x.Tenant)
                    .FirstOrDefaultAsync(x => x.Slug == request.Slug && x.Tenant.UniqueId == request.TenantUniqueId);
                
                return new GetContentBlockBySlugResponse()
                {
                    ContentBlock = contentBlock == null ? null: ContentBlockApiModel.FromContentBlock(contentBlock)
                };
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
