using ContentBlockService.Features.ContentBlocks;
using System.Threading.Tasks;

namespace ContentBlockService.Features.RESTServices
{
    public interface IRESTServiceClient
    {
        Task<ContentBlockApiModel> Get(string resource, string id);
    }
}
