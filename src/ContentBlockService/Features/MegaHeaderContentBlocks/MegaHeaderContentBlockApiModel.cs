using ContentBlockService.Data.Model;

namespace ContentBlockService.Features.MegaHeaderContentBlocks
{
    public class MegaHeaderContentBlockApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromMegaHeaderContentBlock<TModel>(MegaHeaderContentBlock megaHeaderContentBlock) where
            TModel : MegaHeaderContentBlockApiModel, new()
        {
            var model = new TModel();
            model.Id = megaHeaderContentBlock.Id;
            model.TenantId = megaHeaderContentBlock.TenantId;
            model.Name = megaHeaderContentBlock.Name;
            return model;
        }

        public static MegaHeaderContentBlockApiModel FromMegaHeaderContentBlock(MegaHeaderContentBlock megaHeaderContentBlock)
            => FromMegaHeaderContentBlock<MegaHeaderContentBlockApiModel>(megaHeaderContentBlock);

    }
}
