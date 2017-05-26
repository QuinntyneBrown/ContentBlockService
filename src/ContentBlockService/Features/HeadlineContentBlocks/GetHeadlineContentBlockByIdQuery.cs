using MediatR;
using ContentBlockService.Data;
using ContentBlockService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace ContentBlockService.Features.HeadlineContentBlocks
{
    public class GetHeadlineContentBlockByIdQuery
    {
        public class GetHeadlineContentBlockByIdRequest : IRequest<GetHeadlineContentBlockByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetHeadlineContentBlockByIdResponse
        {
            public HeadlineContentBlockApiModel HeadlineContentBlock { get; set; } 
        }

        public class GetHeadlineContentBlockByIdHandler : IAsyncRequestHandler<GetHeadlineContentBlockByIdRequest, GetHeadlineContentBlockByIdResponse>
        {
            public GetHeadlineContentBlockByIdHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetHeadlineContentBlockByIdResponse> Handle(GetHeadlineContentBlockByIdRequest request)
            {                
                return new GetHeadlineContentBlockByIdResponse()
                {
                    HeadlineContentBlock = HeadlineContentBlockApiModel.FromHeadlineContentBlock(await _context.HeadlineContentBlocks
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
