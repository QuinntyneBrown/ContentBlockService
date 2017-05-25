using ContentBlockService.Data.Model;

namespace ContentBlockService.Features.CallToActionContentBlocks
{
    public class CallToActionContentBlockApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromCallToActionContentBlock<TModel>(CallToActionContentBlock callToActionContentBlock) where
            TModel : CallToActionContentBlockApiModel, new()
        {
            var model = new TModel();
            model.Id = callToActionContentBlock.Id;
            model.TenantId = callToActionContentBlock.TenantId;
            model.Name = callToActionContentBlock.Name;
            return model;
        }

        public static CallToActionContentBlockApiModel FromCallToActionContentBlock(CallToActionContentBlock callToActionContentBlock)
            => FromCallToActionContentBlock<CallToActionContentBlockApiModel>(callToActionContentBlock);

    }
}
