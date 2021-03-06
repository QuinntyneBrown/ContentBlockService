using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ContentBlockService.Features.Core;
using static ContentBlockService.Features.Resources.AddOrUpdateResourceCommand;
using static ContentBlockService.Features.Resources.GetResourcesQuery;
using static ContentBlockService.Features.Resources.GetResourceByIdQuery;
using static ContentBlockService.Features.Resources.RemoveResourceCommand;

namespace ContentBlockService.Features.Resources
{
    [Authorize]
    [RoutePrefix("api/resource")]
    public class ResourceController : ApiController
    {
        public ResourceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateResourceResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateResourceRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateResourceResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateResourceRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetResourcesResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetResourcesRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetResourceByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetResourceByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveResourceResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveResourceRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
