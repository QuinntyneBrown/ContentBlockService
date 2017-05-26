using ContentBlockService.Data.Model;

namespace ContentBlockService.Features.HeadlineContentBlocks
{
    public class HeadlineContentBlockApiModel
    {        
        public int Id { get; set; }

        public int? TenantId { get; set; }

        public string Name { get; set; }

        public string Headline1 { get; set; }

        public string Headline2 { get; set; }

        public string Slug { get; set; }

        public static TModel FromHeadlineContentBlock<TModel>(HeadlineContentBlock headlineContentBlock) where
            TModel : HeadlineContentBlockApiModel, new()
        {
            var model = new TModel();

            model.Id = headlineContentBlock.Id;

            model.TenantId = headlineContentBlock.TenantId;

            model.Name = headlineContentBlock.Name;

            model.Slug = headlineContentBlock.Slug;

            model.Headline1 = headlineContentBlock.Headline1;

            model.Headline2 = headlineContentBlock.Headline2;

            return model;
        }

        public static HeadlineContentBlockApiModel FromHeadlineContentBlock(HeadlineContentBlock headlineContentBlock)
            => FromHeadlineContentBlock<HeadlineContentBlockApiModel>(headlineContentBlock);
    }
}
