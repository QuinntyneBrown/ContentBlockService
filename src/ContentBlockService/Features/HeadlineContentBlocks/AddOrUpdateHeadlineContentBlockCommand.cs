using MediatR;
using ContentBlockService.Data;
using ContentBlockService.Data.Model;
using ContentBlockService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace ContentBlockService.Features.HeadlineContentBlocks
{
    public class AddOrUpdateHeadlineContentBlockCommand
    {
        public class AddOrUpdateHeadlineContentBlockRequest : IRequest<AddOrUpdateHeadlineContentBlockResponse>
        {
            public HeadlineContentBlockApiModel HeadlineContentBlock { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateHeadlineContentBlockResponse { }

        public class AddOrUpdateHeadlineContentBlockHandler : IAsyncRequestHandler<AddOrUpdateHeadlineContentBlockRequest, AddOrUpdateHeadlineContentBlockResponse>
        {
            public AddOrUpdateHeadlineContentBlockHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateHeadlineContentBlockResponse> Handle(AddOrUpdateHeadlineContentBlockRequest request)
            {
                var entity = await _context.HeadlineContentBlocks
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.HeadlineContentBlock.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.HeadlineContentBlocks.Add(entity = new HeadlineContentBlock() { TenantId = tenant.Id });
                }

                entity.Name = request.HeadlineContentBlock.Name;

                entity.Slug = request.HeadlineContentBlock.Name.GenerateSlug();

                entity.Headline1 = request.HeadlineContentBlock.Headline1;

                entity.Headline2 = request.HeadlineContentBlock.Headline2;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateHeadlineContentBlockResponse();
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
