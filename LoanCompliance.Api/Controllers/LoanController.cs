using LoanCompliance.BusinessLogic;
using LoanCompliance.Models.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoanCompliance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanController : ControllerBase
    {
        private readonly ILogger<LoanController> _logger;

        private readonly IComplianceProcessor _processor;

        public LoanController(ILogger<LoanController> logger, IComplianceProcessor processor)
        {
            _logger = logger;
            _processor = processor;
        }

        [HttpPut]
        [Route("process")]
        public ComplianceResult ProcessLoan(ComplianceQuery query)
        {
            var complianceResult = _processor.ProcessComplianceStep(query);
            return complianceResult;
        }
    }
}