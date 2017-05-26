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
    public class GetHeadlineContentBlocksQuery
    {
        public class GetHeadlineContentBlocksRequest : IRequest<GetHeadlineContentBlocksResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetHeadlineContentBlocksResponse
        {
            public ICollection<HeadlineContentBlockApiModel> HeadlineContentBlocks { get; set; } = new HashSet<HeadlineContentBlockApiModel>();
        }

        public class GetHeadlineContentBlocksHandler : IAsyncRequestHandler<GetHeadlineContentBlocksRequest, GetHeadlineContentBlocksResponse>
        {
            public GetHeadlineContentBlocksHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetHeadlineContentBlocksResponse> Handle(GetHeadlineContentBlocksRequest request)
            {
                var headlineContentBlocks = await _context.HeadlineContentBlocks
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetHeadlineContentBlocksResponse()
                {
                    HeadlineContentBlocks = headlineContentBlocks.Select(x => HeadlineContentBlockApiModel.FromHeadlineContentBlock(x)).ToList()
                };
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
