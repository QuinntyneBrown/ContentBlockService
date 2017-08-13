using ContentBlockService.Data;
using ContentBlockService.Features.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace ContentBlockService.Features.CallToActionContentBlocks
{
    public class GetCallToActionContentBlocksQuery
    {
        public class Request : IRequest<Response> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class Response
        {
            public ICollection<CallToActionContentBlockApiModel> CallToActionContentBlocks { get; set; } = new HashSet<CallToActionContentBlockApiModel>();
        }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var callToActionContentBlocks = await _context.CallToActionContentBlocks
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new Response()
                {
                    CallToActionContentBlocks = callToActionContentBlocks.Select(x => CallToActionContentBlockApiModel.FromCallToActionContentBlock(x)).ToList()
                };
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
