using MediatR;
using ContentBlockService.Data;
using ContentBlockService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace ContentBlockService.Features.HeadlineContentBlocks
{
    public class GetHeadlineContentBlockBySlugQuery
    {
        public class GetHeadlineContentBlockBySlugRequest : IRequest<GetHeadlineContentBlockBySlugResponse>
        {
            public Guid TenantUniqueId { get; set; }
            public string Slug { get; set; }
        }

        public class GetHeadlineContentBlockBySlugResponse
        {
            public HeadlineContentBlockApiModel HeadlineContentBlock { get; set; }
        }

        public class GetHeadlineContentBlockBySlugHandler : IAsyncRequestHandler<GetHeadlineContentBlockBySlugRequest, GetHeadlineContentBlockBySlugResponse>
        {
            public GetHeadlineContentBlockBySlugHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetHeadlineContentBlockBySlugResponse> Handle(GetHeadlineContentBlockBySlugRequest request)
            {
                return new GetHeadlineContentBlockBySlugResponse()
                {
                    HeadlineContentBlock = HeadlineContentBlockApiModel.FromHeadlineContentBlock(await _context.HeadlineContentBlocks
                    .Include(x => x.Tenant)
                    .SingleAsync(x => x.Slug == request.Slug && x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
