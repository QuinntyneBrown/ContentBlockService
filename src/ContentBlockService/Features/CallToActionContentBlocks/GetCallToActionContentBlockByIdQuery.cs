using ContentBlockService.Data;
using ContentBlockService.Features.Core;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ContentBlockService.Features.CallToActionContentBlocks
{
    public class GetCallToActionContentBlockByIdQuery
    {
        public class Request : IRequest<Response> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class Response
        {
            public CallToActionContentBlockApiModel CallToActionContentBlock { get; set; } 
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
                return new Response()
                {
                    CallToActionContentBlock = CallToActionContentBlockApiModel.FromCallToActionContentBlock(await _context.CallToActionContentBlocks
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
