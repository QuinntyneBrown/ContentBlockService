using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ContentBlockService.Features.Core;

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
        [ResponseType(typeof(AddOrUpdateCallToActionContentBlockCommand.Response))]
        public async Task<IHttpActionResult> Add(AddOrUpdateCallToActionContentBlockCommand.Request request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateCallToActionContentBlockCommand.Response))]
        public async Task<IHttpActionResult> Update(AddOrUpdateCallToActionContentBlockCommand.Request request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetCallToActionContentBlocksQuery.Response))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetCallToActionContentBlocksQuery.Request();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [AllowAnonymous]
        [ResponseType(typeof(GetCallToActionContentBlockByIdQuery.Response))]
        public async Task<IHttpActionResult> GetById([FromUri] GetCallToActionContentBlockByIdQuery.Request request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getBySlug")]
        [HttpGet]
        [AllowAnonymous]
        [ResponseType(typeof(GetCallToActionContentBlockBySlugQuery.Response))]
        public async Task<IHttpActionResult> GetBySlug([FromUri] GetCallToActionContentBlockBySlugQuery.Request request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveCallToActionContentBlockCommand.Response))]
        public async Task<IHttpActionResult> Remove([FromUri] RemoveCallToActionContentBlockCommand.Request request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
