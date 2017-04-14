using MediatR;
using ContentBlockService.Data;
using ContentBlockService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace ContentBlockService.Features.RESTServices
{
    public class GetRESTServiceByIdQuery
    {
        public class GetRESTServiceByIdRequest : IRequest<GetRESTServiceByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetRESTServiceByIdResponse
        {
            public RESTServiceApiModel RESTService { get; set; } 
        }

        public class GetRESTServiceByIdHandler : IAsyncRequestHandler<GetRESTServiceByIdRequest, GetRESTServiceByIdResponse>
        {
            public GetRESTServiceByIdHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetRESTServiceByIdResponse> Handle(GetRESTServiceByIdRequest request)
            {                
                return new GetRESTServiceByIdResponse()
                {
                    RESTService = RESTServiceApiModel.FromRESTService(await _context.RESTServices
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
