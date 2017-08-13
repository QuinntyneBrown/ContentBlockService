using ContentBlockService.Data;
using ContentBlockService.Features.Core;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ContentBlockService.Features.CallToActionContentBlocks
{
    public class RemoveCallToActionContentBlockCommand
    {
        public class Request : IRequest<Response>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class Response { }

        public class Handler : IAsyncRequestHandler<Request, Response>
        {
            public Handler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<Response> Handle(Request request)
            {
                var callToActionContentBlock = await _context.CallToActionContentBlocks.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                callToActionContentBlock.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new Response();
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
