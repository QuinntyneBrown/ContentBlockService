using ContentBlockService.Data;
using ContentBlockService.Data.Model;
using ContentBlockService.Features.Core;
using MediatR;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ContentBlockService.Features.ContentBlocks
{
    public class AddOrUpdateContentBlockCommand
    {
        public class AddOrUpdateContentBlockRequest : IRequest<AddOrUpdateContentBlockResponse>
        {
            public ContentBlockApiModel ContentBlock { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateContentBlockResponse { }

        public class AddOrUpdateContentBlockHandler : IAsyncRequestHandler<AddOrUpdateContentBlockRequest, AddOrUpdateContentBlockResponse>
        {
            public AddOrUpdateContentBlockHandler(ContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateContentBlockResponse> Handle(AddOrUpdateContentBlockRequest request)
            {
                var entity = await _context.ContentBlocks
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.ContentBlock.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.ContentBlocks.Add(entity = new ContentBlock() { TenantId = tenant.Id });
                }

                entity.Name = request.ContentBlock.Name;

                entity.Slug = request.ContentBlock.Name.GenerateSlug();

                entity.Url = request.ContentBlock.Url;

                entity.ImageUrl = request.ContentBlock.ImageUrl;

                entity.HTMLContent = request.ContentBlock.HTMLContent;

                entity.Heading1 = request.ContentBlock.Heading1;

                entity.Heading2 = request.ContentBlock.Heading2;

                await _context.SaveChangesAsync();

                return new AddOrUpdateContentBlockResponse();
            }

            private readonly ContentBlockServiceContext _context;
            private readonly ICache _cache;
        }
    }
}