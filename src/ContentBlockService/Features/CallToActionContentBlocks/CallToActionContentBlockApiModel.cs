using ContentBlockService.Data.Model;

namespace ContentBlockService.Features.CallToActionContentBlocks
{
    public class CallToActionContentBlockApiModel
    {        
        public int Id { get; set; }

        public int? TenantId { get; set; }

        public string Name { get; set; }

        public string Headline { get; set; }

        public string Body { get; set; }

        public string ButtonCaption { get; set; }

        public string CallToAction { get; set; }

        public string FinalNote { get; set; }

        public string Slug { get; set; }

        public static TModel FromCallToActionContentBlock<TModel>(CallToActionContentBlock callToActionContentBlock) where
            TModel : CallToActionContentBlockApiModel, new()
        {
            var model = new TModel();

            model.Id = callToActionContentBlock.Id;

            model.TenantId = callToActionContentBlock.TenantId;

            model.Name = callToActionContentBlock.Name;

            model.Headline = callToActionContentBlock.Headline;

            model.CallToAction = callToActionContentBlock.CallToAction;

            model.Body = callToActionContentBlock.Body;

            model.FinalNote = callToActionContentBlock.FinalNote;

            model.ButtonCaption = callToActionContentBlock.ButtonCaption;

            model.Slug = callToActionContentBlock.Slug;

            return model;
        }

        public static CallToActionContentBlockApiModel FromCallToActionContentBlock(CallToActionContentBlock callToActionContentBlock)
            => FromCallToActionContentBlock<CallToActionContentBlockApiModel>(callToActionContentBlock);
    }
}