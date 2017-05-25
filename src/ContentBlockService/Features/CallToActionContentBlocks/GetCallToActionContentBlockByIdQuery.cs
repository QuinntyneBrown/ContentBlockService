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
    public class GetCallToActionContentBlockByIdQuery
    {
        public class GetCallToActionContentBlockByIdRequest : IRequest<GetCallToActionContentBlockByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetCallToActionContentBlockByIdResponse
        {
            public CallToActionContentBlockApiModel CallToActionContentBlock { get; set; } 
        }

        public class GetCallToActionContentBlockByIdHandler : IAsyncRequestHandler<GetCallToActionContentBlockByIdRequest, GetCallToActionContentBlockByIdResponse>
        {
            public GetCallToActionContentBlockByIdHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetCallToActionContentBlockByIdResponse> Handle(GetCallToActionContentBlockByIdRequest request)
            {                
                return new GetCallToActionContentBlockByIdResponse()
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
