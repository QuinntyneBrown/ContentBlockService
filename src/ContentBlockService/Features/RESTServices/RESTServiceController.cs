using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ContentBlockService.Features.Core;
using static ContentBlockService.Features.RESTServices.AddOrUpdateRESTServiceCommand;
using static ContentBlockService.Features.RESTServices.GetRESTServicesQuery;
using static ContentBlockService.Features.RESTServices.GetRESTServiceByIdQuery;
using static ContentBlockService.Features.RESTServices.RemoveRESTServiceCommand;

namespace ContentBlockService.Features.RESTServices
{
    [Authorize]
    [RoutePrefix("api/rESTService")]
    public class RESTServiceController : ApiController
    {
        public RESTServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateRESTServiceResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateRESTServiceRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateRESTServiceResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateRESTServiceRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetRESTServicesResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetRESTServicesRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetRESTServiceByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetRESTServiceByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveRESTServiceResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveRESTServiceRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
