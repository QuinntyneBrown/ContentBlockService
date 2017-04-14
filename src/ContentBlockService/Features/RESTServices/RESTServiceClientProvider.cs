using ContentBlockService.Features.RESTServices.Clients;
using System;

namespace ContentBlockService.Features.RESTServices
{
    public interface IRESTServiceClientProvider {
        IRESTServiceClient Get(string restServiceName);
    }

    public class RESTServiceClientProvider : IRESTServiceClientProvider
    {
        public RESTServiceClientProvider(IEventServiceCLient eventServiceClient)
        {
            _eventServiceClient = eventServiceClient;
        }

        public IRESTServiceClient Get(string restServiceName)
        {
            if (restServiceName == "EventService")
                return _eventServiceClient;

            throw new NotSupportedException();
        }

        protected readonly IEventServiceCLient _eventServiceClient;
    }
}
