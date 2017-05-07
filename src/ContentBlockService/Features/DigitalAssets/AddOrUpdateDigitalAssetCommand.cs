using ContentBlockService.Data;
using ContentBlockService.Data.Model;
using ContentBlockService.Features.Core;
using MediatR;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ContentBlockService.Features.DigitalAssets
{
    public class AddOrUpdateDigitalAssetCommand
    {
        public class AddOrUpdateDigitalAssetRequest : IRequest<AddOrUpdateDigitalAssetResponse>
        {
            public DigitalAssetApiModel DigitalAsset { get; set; }
        }

        public class AddOrUpdateDigitalAssetResponse { }

        public class AddOrUpdateDigitalAssetHandler : IAsyncRequestHandler<AddOrUpdateDigitalAssetRequest, AddOrUpdateDigitalAssetResponse>
        {
            public AddOrUpdateDigitalAssetHandler(IContentBlockServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateDigitalAssetResponse> Handle(AddOrUpdateDigitalAssetRequest request)
            {
                var entity = await _context.DigitalAssets
                    .SingleOrDefaultAsync(x => x.Id == request.DigitalAsset.Id && x.IsDeleted == false);
                
                if (entity == null) _context.DigitalAssets.Add(entity = new DigitalAsset());

                entity.Name = request.DigitalAsset.Name;

                entity.Folder = request.DigitalAsset.Folder;

                await _context.SaveChangesAsync();

                return new AddOrUpdateDigitalAssetResponse() { };
            }

            private readonly IContentBlockServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
