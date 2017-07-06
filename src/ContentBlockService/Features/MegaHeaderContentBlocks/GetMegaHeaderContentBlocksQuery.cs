using MediatR;
using ContentBlockService.Data;
using ContentBlockService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace ContentBlockService.Features.MegaHeaderContentBlocks
{
    public class GetMegaHeaderContentBlocksQuery
    {
        public class GetMegaHeaderContentBlocksRequest : IRequest<GetMegaHeaderContentBlocksResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetMegaHeaderContentBlocksResponse
        {
            public ICollection<MegaHeaderContentBlockApiModel> MegaHeaderContentBlocks { get; set; } = new HashSet<MegaHeaderContentBlockApiModel>();
        }

        public class GetMegaHeaderContentBlocksHandler : IAsyncRequestHandler<GetMegaHeaderContentBlocksRequest, GetMegaHeaderContentBlocksResponse>
        {
            public GetMegaHeaderContentBlocksHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetMegaHeaderContentBlocksResponse> Handle(GetMegaHeaderContentBlocksRequest request)
            {
                var megaHeaderContentBlocks = await _context.MegaHeaderContentBlocks
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetMegaHeaderContentBlocksResponse()
                {
                    MegaHeaderContentBlocks = megaHeaderContentBlocks.Select(x => MegaHeaderContentBlockApiModel.FromMegaHeaderContentBlock(x)).ToList()
                };
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
