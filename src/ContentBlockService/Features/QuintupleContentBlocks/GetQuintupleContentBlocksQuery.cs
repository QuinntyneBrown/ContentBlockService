using MediatR;
using ContentBlockService.Data;
using ContentBlockService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace ContentBlockService.Features.QuintupleContentBlocks
{
    public class GetQuintupleContentBlocksQuery
    {
        public class GetQuintupleContentBlocksRequest : IRequest<GetQuintupleContentBlocksResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetQuintupleContentBlocksResponse
        {
            public ICollection<QuintupleContentBlockApiModel> QuintupleContentBlocks { get; set; } = new HashSet<QuintupleContentBlockApiModel>();
        }

        public class GetQuintupleContentBlocksHandler : IAsyncRequestHandler<GetQuintupleContentBlocksRequest, GetQuintupleContentBlocksResponse>
        {
            public GetQuintupleContentBlocksHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetQuintupleContentBlocksResponse> Handle(GetQuintupleContentBlocksRequest request)
            {
                var quintupleContentBlocks = await _context.QuintupleContentBlocks
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetQuintupleContentBlocksResponse()
                {
                    QuintupleContentBlocks = quintupleContentBlocks.Select(x => QuintupleContentBlockApiModel.FromQuintupleContentBlock(x)).ToList()
                };
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
