using ContentBlockService.Data.Model;

namespace ContentBlockService.Features.HeadlineContentBlocks
{
    public class HeadlineContentBlockApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromHeadlineContentBlock<TModel>(HeadlineContentBlock headlineContentBlock) where
            TModel : HeadlineContentBlockApiModel, new()
        {
            var model = new TModel();
            model.Id = headlineContentBlock.Id;
            model.TenantId = headlineContentBlock.TenantId;
            model.Name = headlineContentBlock.Name;
            return model;
        }

        public static HeadlineContentBlockApiModel FromHeadlineContentBlock(HeadlineContentBlock headlineContentBlock)
            => FromHeadlineContentBlock<HeadlineContentBlockApiModel>(headlineContentBlock);

    }
}
