using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompensationService _compService;

        public CompensationController(ILogger<CompensationController> logger, ICompensationService compService)
        {
            _logger = logger;
            _compService = compService;
        }

        [HttpPost]
        public IActionResult CreateCompensation([FromBody] Compensation comp)
        {
            _logger.LogDebug($"Received compensation create request for '{comp.employee.FirstName} {comp.employee.LastName} : {comp.effectiveDate}'");

            _compService.Create(comp);

            return CreatedAtRoute("getCompensationByEmployeeId", new { id = comp.employee.EmployeeId}, comp);
        }

        [HttpGet("{id}", Name = "getCompensationByEmployeeId")]
        public IActionResult GetCompensationByEmployeeId(string id)
        {
            _logger.LogDebug($"Received compensation get request for '{id}");

            var compensation = _compService.GetByEmployeeId(id);

            if (compensation == null)
            {
                return NotFound();
            }

            return Ok(compensation);
        }
    }
}