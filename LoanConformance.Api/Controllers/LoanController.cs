using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanConformance.BusinessLogic;
using LoanConformance.BusinessLogic.Impl;
using LoanConformance.Data;
using LoanConformance.Models;
using LoanConformance.Models.Query;

namespace LoanConformance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILogger<LoanController> _logger;

        private readonly List<IConformanceProcessor> _processors;

        public LoanController(ILogger<LoanController> logger, IDataAccess dataAccess)
        {
            _logger = logger;
            _processors = new List<IConformanceProcessor>
            {
                new GlobalsTest(dataAccess),
                new AprTest(dataAccess),
                new FeeTest(dataAccess)
            };
        }

        [HttpPut]
        [Route("process")]
        public ConformanceResult ProcessLoan(ConformanceQuery query)
        {
            var globalTest = _processors
                .First(x => x.ChecksCompliance)
                .ProcessConformanceData(query);

            if (globalTest.ComplianceCheckNeeded)
            {
                var complianceChecks = _processors
                    .Where(x => !x.ChecksCompliance);
                var complianceResult = new ConformanceResult();

                complianceResult = complianceChecks
                    .Aggregate(complianceResult, 
                        (current, check) => 
                            current + check.ProcessConformanceData(query));

                return complianceResult;
            }

            return new ConformanceResult
            {
                ComplianceCheckNeeded = true,
                Complies = false,
                FailureReasons = globalTest.FailureReasons
            };
        }
    }
}
