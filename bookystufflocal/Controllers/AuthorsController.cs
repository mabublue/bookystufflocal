using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bookystufflocal.domain.DomainLayer.BaseModels;
using bookystufflocal.domain.DomainLayer.Library;
using bookystufflocal.domain.Queries;
using bookystufflocal.domain.Queries.Library;
using MediatR;
using Microsoft.AspNetCore.Http;
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

            return await GetResponse(new AuthorListPagedQuery { NumberOfRecordsPerPage = 10 });
        }

        private async Task<IActionResult> GetResponse(IPagedQuery<IEnumerable<Author>> query)
        {
            var response = new ApiResponse<IEnumerable<Author>>();

            try
            {
                response.Data = await _mediator.Send(query);
                response.Success = true;
                return new OkObjectResult(response);
            }
            catch (Exception e)
            {
                response.Exception = e.Message;
                response.InnerException = e.InnerException?.Message;
                response.Success = false;
                response.Message = "An Exception Occured";
                return new ObjectResult(new { StatusCodes.Status500InternalServerError, response });
            }
        }
    }
}
