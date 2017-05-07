using ContentBlockService.Data.Model;

namespace ContentBlockService.Features.ContentBlocks
{
    public class ContentBlockApiModel
    {        
        public int Id { get; set; }

        public int? TenantId { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Url { get; set; }

        public string IconUrl { get; set; }

        public string HTMLContent { get; set; }

        public string Heading1 { get; set; }

        public string Heading2 { get; set; }

        public static TModel FromContentBlock<TModel>(ContentBlock contentBlock) where
            TModel : ContentBlockApiModel, new()
        {
            var model = new TModel();

            model.Id = contentBlock.Id;

            model.TenantId = contentBlock.TenantId;

            model.Name = contentBlock.Name;

            model.Slug = contentBlock.Slug;

            model.Title = contentBlock.Title;

            model.ImageUrl = contentBlock.ImageUrl;

            model.IconUrl = contentBlock.IconUrl;

            model.Url = contentBlock.Url;

            model.HTMLContent = contentBlock.HTMLContent;

            model.Heading1 = contentBlock.Heading1;

            model.Heading2 = contentBlock.Heading2;

            return model;
        }

        public static ContentBlockApiModel FromContentBlock(ContentBlock contentBlock)
            => FromContentBlock<ContentBlockApiModel>(contentBlock);
    }
}
