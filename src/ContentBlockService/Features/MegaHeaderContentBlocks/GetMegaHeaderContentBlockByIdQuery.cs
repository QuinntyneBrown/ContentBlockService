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
    public class GetMegaHeaderContentBlockByIdQuery
    {
        public class GetMegaHeaderContentBlockByIdRequest : IRequest<GetMegaHeaderContentBlockByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetMegaHeaderContentBlockByIdResponse
        {
            public MegaHeaderContentBlockApiModel MegaHeaderContentBlock { get; set; } 
        }

        public class GetMegaHeaderContentBlockByIdHandler : IAsyncRequestHandler<GetMegaHeaderContentBlockByIdRequest, GetMegaHeaderContentBlockByIdResponse>
        {
            public GetMegaHeaderContentBlockByIdHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetMegaHeaderContentBlockByIdResponse> Handle(GetMegaHeaderContentBlockByIdRequest request)
            {                
                return new GetMegaHeaderContentBlockByIdResponse()
                {
                    MegaHeaderContentBlock = MegaHeaderContentBlockApiModel.FromMegaHeaderContentBlock(await _context.MegaHeaderContentBlocks
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
