using CodeChallenge.Models;
using CodeChallenge.Repositories;
using Microsoft.Extensions.Logging;
using System;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compRepository;
        private readonly ILogger<CompensationService> _logger;

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository employeeRepository)
        {
            _compRepository = employeeRepository;
            _logger = logger;
        }
        public Compensation Create(Compensation comp)
        {
            if (comp != null)
            {
                _compRepository.Add(comp);
                _compRepository.SaveAsync().Wait();
            }

            return comp;
        }

        public Compensation GetByEmployeeId(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return _compRepository.GetByEmployeeId(id);
            }

            return null;
        }
    }
}