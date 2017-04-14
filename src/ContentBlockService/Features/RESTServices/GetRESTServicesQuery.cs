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
    public class GetRESTServicesQuery
    {
        public class GetRESTServicesRequest : IRequest<GetRESTServicesResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetRESTServicesResponse
        {
            public ICollection<RESTServiceApiModel> RESTServices { get; set; } = new HashSet<RESTServiceApiModel>();
        }

        public class GetRESTServicesHandler : IAsyncRequestHandler<GetRESTServicesRequest, GetRESTServicesResponse>
        {
            public GetRESTServicesHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetRESTServicesResponse> Handle(GetRESTServicesRequest request)
            {
                var rESTServices = await _context.RESTServices
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetRESTServicesResponse()
                {
                    RESTServices = rESTServices.Select(x => RESTServiceApiModel.FromRESTService(x)).ToList()
                };
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
