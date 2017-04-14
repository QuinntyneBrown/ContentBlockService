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
    public class GetQuintupleContentBlockByIdQuery
    {
        public class GetQuintupleContentBlockByIdRequest : IRequest<GetQuintupleContentBlockByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetQuintupleContentBlockByIdResponse
        {
            public QuintupleContentBlockApiModel QuintupleContentBlock { get; set; } 
        }

        public class GetQuintupleContentBlockByIdHandler : IAsyncRequestHandler<GetQuintupleContentBlockByIdRequest, GetQuintupleContentBlockByIdResponse>
        {
            public GetQuintupleContentBlockByIdHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetQuintupleContentBlockByIdResponse> Handle(GetQuintupleContentBlockByIdRequest request)
            {                
                return new GetQuintupleContentBlockByIdResponse()
                {
                    QuintupleContentBlock = QuintupleContentBlockApiModel.FromQuintupleContentBlock(await _context.QuintupleContentBlocks
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
