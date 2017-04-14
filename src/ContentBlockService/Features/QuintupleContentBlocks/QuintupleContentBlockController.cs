using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ContentBlockService.Features.Core;
using static ContentBlockService.Features.QuintupleContentBlocks.AddOrUpdateQuintupleContentBlockCommand;
using static ContentBlockService.Features.QuintupleContentBlocks.GetQuintupleContentBlocksQuery;
using static ContentBlockService.Features.QuintupleContentBlocks.GetQuintupleContentBlockByIdQuery;
using static ContentBlockService.Features.QuintupleContentBlocks.RemoveQuintupleContentBlockCommand;

namespace ContentBlockService.Features.QuintupleContentBlocks
{
    [Authorize]
    [RoutePrefix("api/quintupleContentBlock")]
    public class QuintupleContentBlockController : ApiController
    {
        public QuintupleContentBlockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateQuintupleContentBlockResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateQuintupleContentBlockRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateQuintupleContentBlockResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateQuintupleContentBlockRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetQuintupleContentBlocksResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetQuintupleContentBlocksRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetQuintupleContentBlockByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetQuintupleContentBlockByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveQuintupleContentBlockResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveQuintupleContentBlockRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
