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
    public class RemoveRESTServiceCommand
    {
        public class RemoveRESTServiceRequest : IRequest<RemoveRESTServiceResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveRESTServiceResponse { }

        public class RemoveRESTServiceHandler : IAsyncRequestHandler<RemoveRESTServiceRequest, RemoveRESTServiceResponse>
        {
            public RemoveRESTServiceHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveRESTServiceResponse> Handle(RemoveRESTServiceRequest request)
            {
                var rESTService = await _context.RESTServices.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                rESTService.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveRESTServiceResponse();
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
