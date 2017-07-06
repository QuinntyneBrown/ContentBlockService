using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ContentBlockService.Features.Core;
using static ContentBlockService.Features.CallToActionContentBlocks.AddOrUpdateCallToActionContentBlockCommand;
using static ContentBlockService.Features.CallToActionContentBlocks.GetCallToActionContentBlocksQuery;
using static ContentBlockService.Features.CallToActionContentBlocks.GetCallToActionContentBlockByIdQuery;
using static ContentBlockService.Features.CallToActionContentBlocks.RemoveCallToActionContentBlockCommand;
using static ContentBlockService.Features.CallToActionContentBlocks.GetCallToActionContentBlockBySlugQuery;

namespace ContentBlockService.Features.CallToActionContentBlocks
{
    [Authorize]
    [RoutePrefix("api/callToActionContentBlock")]
    public class CallToActionContentBlockController : ApiController
    {
        public CallToActionContentBlockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateCallToActionContentBlockResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateCallToActionContentBlockRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateCallToActionContentBlockResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateCallToActionContentBlockRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetCallToActionContentBlocksResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetCallToActionContentBlocksRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [AllowAnonymous]
        [ResponseType(typeof(GetCallToActionContentBlockByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetCallToActionContentBlockByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getBySlug")]
        [HttpGet]
        [AllowAnonymous]
        [ResponseType(typeof(GetCallToActionContentBlockBySlugResponse))]
        public async Task<IHttpActionResult> GetBySlug([FromUri]GetCallToActionContentBlockBySlugRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveCallToActionContentBlockResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveCallToActionContentBlockRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
