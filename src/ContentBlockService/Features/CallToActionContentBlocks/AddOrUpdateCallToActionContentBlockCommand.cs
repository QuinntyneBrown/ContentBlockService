using ContentBlockService.Data;
using ContentBlockService.Data.Model;
using ContentBlockService.Features.Core;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ContentBlockService.Features.CallToActionContentBlocks
{
    public class AddOrUpdateCallToActionContentBlockCommand
    {
        public class Request : IRequest<Response>
        {
            public CallToActionContentBlockApiModel CallToActionContentBlock { get; set; }
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
                var entity = await _context.CallToActionContentBlocks
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.CallToActionContentBlock.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.CallToActionContentBlocks.Add(entity = new CallToActionContentBlock() { TenantId = tenant.Id });
                }

                entity.Name = request.CallToActionContentBlock.Name;

                entity.Body = request.CallToActionContentBlock.Body;

                entity.FinalNote = request.CallToActionContentBlock.FinalNote;

                entity.Headline = request.CallToActionContentBlock.Headline;

                entity.ButtonCaption = request.CallToActionContentBlock.ButtonCaption;

                entity.CallToAction = request.CallToActionContentBlock.CallToAction;

                entity.Slug = request.CallToActionContentBlock.Name.GenerateSlug();

                await _context.SaveChangesAsync();

                return new Response();
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
