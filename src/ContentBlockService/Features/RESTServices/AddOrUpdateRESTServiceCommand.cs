using MediatR;
using ContentBlockService.Data;
using ContentBlockService.Data.Model;
using ContentBlockService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace ContentBlockService.Features.RESTServices
{
    public class AddOrUpdateRESTServiceCommand
    {
        public class AddOrUpdateRESTServiceRequest : IRequest<AddOrUpdateRESTServiceResponse>
        {
            public RESTServiceApiModel RESTService { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateRESTServiceResponse { }

        public class AddOrUpdateRESTServiceHandler : IAsyncRequestHandler<AddOrUpdateRESTServiceRequest, AddOrUpdateRESTServiceResponse>
        {
            public AddOrUpdateRESTServiceHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateRESTServiceResponse> Handle(AddOrUpdateRESTServiceRequest request)
            {
                var entity = await _context.RESTServices
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.RESTService.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.RESTServices.Add(entity = new RESTService() { TenantId = tenant.Id });
                }

                entity.Name = request.RESTService.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateRESTServiceResponse();
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
