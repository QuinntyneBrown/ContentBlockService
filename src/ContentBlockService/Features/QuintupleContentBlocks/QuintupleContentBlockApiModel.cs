using ContentBlockService.Data.Model;

namespace ContentBlockService.Features.QuintupleContentBlocks
{
    public class QuintupleContentBlockApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromQuintupleContentBlock<TModel>(QuintupleContentBlock quintupleContentBlock) where
            TModel : QuintupleContentBlockApiModel, new()
        {
            var model = new TModel();
            model.Id = quintupleContentBlock.Id;
            model.TenantId = quintupleContentBlock.TenantId;
            model.Name = quintupleContentBlock.Name;
            return model;
        }

        public static QuintupleContentBlockApiModel FromQuintupleContentBlock(QuintupleContentBlock quintupleContentBlock)
            => FromQuintupleContentBlock<QuintupleContentBlockApiModel>(quintupleContentBlock);

    }
}
