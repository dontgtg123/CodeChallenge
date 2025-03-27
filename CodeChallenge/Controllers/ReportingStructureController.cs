using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/reportingstructure")]
    public class ReportingStructureController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public ReportingStructureController(ILogger<ReportingStructureController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet("{id}", Name = "getReportingStructById")]
        public IActionResult GetReportingStructById(string id)
        {
            _logger.LogDebug($"Received reporting structure get request for '{id}");

            var employee = _employeeService.GetById(id);

            if(employee == null)
            {
                return NotFound();
            }

            return Ok(new ReportingStructure(employee, TraverseReportingAndCount(employee)));
        }

        //Uses BFS for traversal
        private int TraverseReportingAndCount(Employee employee)
        {
            Queue<Employee> toCount = new Queue<Employee>();
            List<Employee> visited = new List<Employee>();

            toCount.Enqueue(employee);

            while (toCount.Count != 0)
            {
                Employee curr = toCount.Dequeue();

                if (curr.DirectReports != null)
                {
                    foreach (Employee e in curr.DirectReports)
                    {
                        if (!visited.Contains(e))
                        {
                            toCount.Enqueue(_employeeService.GetById(e.EmployeeId));
                        }
                    }
                }

                visited.Add(curr);
            }

            return visited.Count - 1;
        }
    }
}