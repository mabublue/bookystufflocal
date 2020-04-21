using System.Collections.Generic;
using System.Threading.Tasks;
using bookystufflocal.domain.DomainLayer.BaseModels;
using bookystufflocal.domain.DomainLayer.Library;
using bookystufflocal.domain.Queries.Library;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace bookystufflocal.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ILogger<AuthorsController> _logger;
        private readonly IMediator _mediator;

        public AuthorsController(ILogger<AuthorsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogDebug("AuthorsController - Get()");

            var response = new ApiResponse<IEnumerable<Author>>
            {
                Data = await _mediator.Send(new AuthorListPagedQuery {NumberOfRecordsPerPage = 10}),
                Success = true
            };

            return new OkObjectResult(response);
        }
    }
}
