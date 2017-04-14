using System;
using System.Threading.Tasks;
using ContentBlockService.Features.ContentBlocks;
using System.Net.Http;

namespace ContentBlockService.Features.RESTServices.Clients
{
    public interface IEventServiceCLient : IRESTServiceClient { }

    public class EventServiceClient : IEventServiceCLient
    {
        public EventServiceClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<ContentBlockApiModel> Get(string resource, string id)
        {
            throw new NotImplementedException();
        }

        protected readonly HttpClient _client;
    }
}
