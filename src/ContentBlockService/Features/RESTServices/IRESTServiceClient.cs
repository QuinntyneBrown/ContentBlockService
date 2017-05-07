using ContentBlockService.Features.ContentBlocks;
using System.Threading.Tasks;

namespace ContentBlockService.Features.RESTServices
{
    public interface IRESTServiceClient
    {
        Task<ContentBlockApiModel> GetById(string resource, string id);

        Task<ContentBlockApiModel> Get(string resource, int? skip, int? limit);
    }
}
