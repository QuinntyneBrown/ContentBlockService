using Microsoft.Owin;
using ContentBlockService.Data;
using System.Threading.Tasks;
using System.Web.Http;

namespace ContentBlockService.Features.Core
{
    public class TenantMiddleware : OwinMiddleware
    {
        public TenantMiddleware(OwinMiddleware next)
            : base(next) { }

        public override async Task Invoke(IOwinContext context)
        {
            var quoteServiceContext = (ContentBlockServiceContext)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ContentBlockServiceContext));
            
            var values = context.Request.Headers.GetValues("Tenant");
            if (values != null) {                
                context.Environment.Add("Tenant", ((string[])(values))[0]);                
            }
           
            await Next.Invoke(context);
        }
    }
}
