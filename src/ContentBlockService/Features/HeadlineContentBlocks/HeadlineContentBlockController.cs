using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ContentBlockService.Features.Core;
using static ContentBlockService.Features.HeadlineContentBlocks.AddOrUpdateHeadlineContentBlockCommand;
using static ContentBlockService.Features.HeadlineContentBlocks.GetHeadlineContentBlocksQuery;
using static ContentBlockService.Features.HeadlineContentBlocks.GetHeadlineContentBlockByIdQuery;
using static ContentBlockService.Features.HeadlineContentBlocks.RemoveHeadlineContentBlockCommand;
using static ContentBlockService.Features.HeadlineContentBlocks.GetHeadlineContentBlockBySlugQuery;

namespace ContentBlockService.Features.HeadlineContentBlocks
{
    [Authorize]
    [RoutePrefix("api/headlineContentBlock")]
    public class HeadlineContentBlockController : ApiController
    {
        public HeadlineContentBlockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateHeadlineContentBlockResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateHeadlineContentBlockRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateHeadlineContentBlockResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateHeadlineContentBlockRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetHeadlineContentBlocksResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetHeadlineContentBlocksRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetHeadlineContentBlockByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetHeadlineContentBlockByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }


        [Route("getBySlug")]
        [HttpGet]
        [ResponseType(typeof(GetHeadlineContentBlockBySlugResponse))]
        public async Task<IHttpActionResult> GetBySlug([FromUri]GetHeadlineContentBlockBySlugRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveHeadlineContentBlockResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveHeadlineContentBlockRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
