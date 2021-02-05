﻿using System.Collections.Generic;
using System.Linq;
using LoanConformance.BusinessLogic;
using LoanConformance.BusinessLogic.Impl;
using LoanConformance.Data;
using LoanConformance.Models.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoanConformance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILogger<LoanController> _logger;

        private readonly List<IComplianceProcessor> _processors;

        public LoanController(ILogger<LoanController> logger, IDataAccess dataAccess)
        {
            _logger = logger;
            _processors = new List<IComplianceProcessor>
            {
                new ValidationTest(),
                new GlobalsTest(dataAccess),
                new AprTest(dataAccess),
                new FeeTest(dataAccess)
            };
        }

        [HttpPut]
        [Route("process")]
        public ComplianceResult ProcessLoan(ComplianceQuery query)
        {
            var complianceResult = new ComplianceResult();
            complianceResult = _processors
                .Aggregate(complianceResult,
                    (current, check) =>
                        current + check.ProcessConformanceStep(query));
            return complianceResult;
        }
    }
}