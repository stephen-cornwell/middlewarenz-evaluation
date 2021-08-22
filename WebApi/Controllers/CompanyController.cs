using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiddlewareNz.Evaluation.WebApi.Clients;
using MiddlewareNz.Evaluation.WebApi.Models;

namespace MiddlewareNz.Evaluation.WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("company")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyXmlApiClient _companyApiClient;

        public CompanyController(ICompanyXmlApiClient companyApiClient)
        {
            _companyApiClient = companyApiClient;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(Company), 200)]
        [ProducesResponseType(typeof(Error), 404)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _companyApiClient.Get(id, cancellationToken);

            if (result.HttpStatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(new Error("Not Found", $"A company with the id {id} was not found."));
            }

            if (result.HttpStatusCode != HttpStatusCode.OK)
            {
                return StatusCode(500, new Error("Internal Server Error", result.ResponseReason));
            }

            // Given the simplicity here, just instantiating the model seems appropriate. 
            // If it was more complex, or mappings were required elsewhere, I'd look to refactor
            // and use automapper or similar. 
            var company = new Company
            {
                Id = result.Content.Id,
                Name = result.Content.Name,
                Description = result.Content.Description
            };

            return Ok(company);
        }
    }
}
