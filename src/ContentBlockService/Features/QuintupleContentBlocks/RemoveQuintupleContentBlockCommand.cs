using MediatR;
using ContentBlockService.Data;
using ContentBlockService.Data.Model;
using ContentBlockService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace ContentBlockService.Features.QuintupleContentBlocks
{
    public class RemoveQuintupleContentBlockCommand
    {
        public class RemoveQuintupleContentBlockRequest : IRequest<RemoveQuintupleContentBlockResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveQuintupleContentBlockResponse { }

        public class RemoveQuintupleContentBlockHandler : IAsyncRequestHandler<RemoveQuintupleContentBlockRequest, RemoveQuintupleContentBlockResponse>
        {
            public RemoveQuintupleContentBlockHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveQuintupleContentBlockResponse> Handle(RemoveQuintupleContentBlockRequest request)
            {
                var quintupleContentBlock = await _context.QuintupleContentBlocks.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                quintupleContentBlock.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveQuintupleContentBlockResponse();
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
