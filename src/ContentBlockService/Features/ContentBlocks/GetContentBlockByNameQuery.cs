using MediatR;
using ContentBlockService.Data;
using ContentBlockService.Features.Core;
using System;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ContentBlockService.Features.ContentBlocks
{
    public class GetContentBlockByNameQuery
    {
        public class GetContentBlockByNameRequest : IRequest<GetContentBlockByNameResponse>
        {
            public string Name { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetContentBlockByNameResponse
        {
            public ContentBlockApiModel ContentBlock { get; set; }
        }

        public class GetContentBlockByNameHandler : IAsyncRequestHandler<GetContentBlockByNameRequest, GetContentBlockByNameResponse>
        {
            public GetContentBlockByNameHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetContentBlockByNameResponse> Handle(GetContentBlockByNameRequest request)
            {
                return new GetContentBlockByNameResponse()
                {
                    ContentBlock = ContentBlockApiModel.FromContentBlock(await _context.ContentBlocks
                    .Include(x => x.Tenant)
                    .SingleAsync(x => x.Name == request.Name && x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }
    }
}