using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ContentBlockService.Features.Core;
using static ContentBlockService.Features.MegaHeaderContentBlocks.AddOrUpdateMegaHeaderContentBlockCommand;
using static ContentBlockService.Features.MegaHeaderContentBlocks.GetMegaHeaderContentBlocksQuery;
using static ContentBlockService.Features.MegaHeaderContentBlocks.GetMegaHeaderContentBlockByIdQuery;
using static ContentBlockService.Features.MegaHeaderContentBlocks.RemoveMegaHeaderContentBlockCommand;

namespace ContentBlockService.Features.MegaHeaderContentBlocks
{
    [Authorize]
    [RoutePrefix("api/megaHeaderContentBlock")]
    public class MegaHeaderContentBlockController : ApiController
    {
        public MegaHeaderContentBlockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateMegaHeaderContentBlockResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateMegaHeaderContentBlockRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateMegaHeaderContentBlockResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateMegaHeaderContentBlockRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetMegaHeaderContentBlocksResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetMegaHeaderContentBlocksRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetMegaHeaderContentBlockByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetMegaHeaderContentBlockByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveMegaHeaderContentBlockResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveMegaHeaderContentBlockRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
