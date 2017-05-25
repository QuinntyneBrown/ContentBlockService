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
    public class GetCallToActionContentBlockBySlugQuery
    {
        public class GetCallToActionContentBlockBySlugRequest : IRequest<GetCallToActionContentBlockBySlugResponse>
        {
            public Guid TenantUniqueId { get; set; }
            public string Slug { get; set; }
        }

        public class GetCallToActionContentBlockBySlugResponse
        {
            public CallToActionContentBlockApiModel CallToActionContentBlock { get; set; }
        }

        public class GetCallToActionContentBlockBySlugHandler : IAsyncRequestHandler<GetCallToActionContentBlockBySlugRequest, GetCallToActionContentBlockBySlugResponse>
        {
            public GetCallToActionContentBlockBySlugHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetCallToActionContentBlockBySlugResponse> Handle(GetCallToActionContentBlockBySlugRequest request)
            {
                return new GetCallToActionContentBlockBySlugResponse()
                {
                    CallToActionContentBlock = CallToActionContentBlockApiModel.FromCallToActionContentBlock(await _context.CallToActionContentBlocks
                    .Include(x => x.Tenant)
                    .SingleAsync(x => x.Slug == request.Slug && x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
