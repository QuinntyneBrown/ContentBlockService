using MediatR;
using ContentBlockService.Data;
using ContentBlockService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace ContentBlockService.Features.CallToActionContentBlocks
{
    public class GetCallToActionContentBlocksQuery
    {
        public class GetCallToActionContentBlocksRequest : IRequest<GetCallToActionContentBlocksResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetCallToActionContentBlocksResponse
        {
            public ICollection<CallToActionContentBlockApiModel> CallToActionContentBlocks { get; set; } = new HashSet<CallToActionContentBlockApiModel>();
        }

        public class GetCallToActionContentBlocksHandler : IAsyncRequestHandler<GetCallToActionContentBlocksRequest, GetCallToActionContentBlocksResponse>
        {
            public GetCallToActionContentBlocksHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetCallToActionContentBlocksResponse> Handle(GetCallToActionContentBlocksRequest request)
            {
                var callToActionContentBlocks = await _context.CallToActionContentBlocks
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetCallToActionContentBlocksResponse()
                {
                    CallToActionContentBlocks = callToActionContentBlocks.Select(x => CallToActionContentBlockApiModel.FromCallToActionContentBlock(x)).ToList()
                };
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
