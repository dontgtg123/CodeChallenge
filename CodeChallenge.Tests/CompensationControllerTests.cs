using CodeChallenge.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net;
using CodeCodeChallenge.Tests.Integration.Helpers;
using CodeCodeChallenge.Tests.Integration.Extensions;
using System.Text;
using System;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // Arrange
            var _employee = new Employee()
            {
                EmployeeId = "testid",
                Department = "Complaints",
                FirstName = "Debbie",
                LastName = "Downer",
                Position = "Receiver",
            };

            var compensation = new Compensation()
            {
                employee = _employee,
                effectiveDate = new DateTime(2024, 3, 27),
                salary = 12345.67f
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newComp = response.DeserializeContent<Compensation>();
            Assert.AreEqual(compensation.employee.EmployeeId, newComp.employee.EmployeeId);
            Assert.AreEqual(compensation.salary, newComp.salary);
            Assert.AreEqual(compensation.effectiveDate, newComp.effectiveDate);
        }

        [TestMethod]
        public void GetCompensationByEmployeeId_Returns_OK()
        {
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";

            int expectedNumberOfReports = 4;

            var getRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeId}");
            var response = getRequestTask.Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingstruct = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(employeeId, reportingstruct.employee.EmployeeId);
            Assert.AreEqual(expectedNumberOfReports, reportingstruct.numberOfReports);
        }
    }
}
