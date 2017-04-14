using ContentBlockService.Data.Model;

namespace ContentBlockService.Features.RESTServices
{
    public class RESTServiceApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromRESTService<TModel>(RESTService rESTService) where
            TModel : RESTServiceApiModel, new()
        {
            var model = new TModel();
            model.Id = rESTService.Id;
            model.TenantId = rESTService.TenantId;
            model.Name = rESTService.Name;
            return model;
        }

        public static RESTServiceApiModel FromRESTService(RESTService rESTService)
            => FromRESTService<RESTServiceApiModel>(rESTService);

    }
}
