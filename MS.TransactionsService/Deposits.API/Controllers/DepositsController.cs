using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Transactions.Application;
using Transactions.Application.Dtos;
using NServiceBus.Logging;

namespace Deposits.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositsController : ControllerBase
    {
        static readonly ILog log = LogManager.GetLogger<DepositsController>();
        private readonly ITransactionApplicationService _transactionApplicationService;

        public DepositsController(ITransactionApplicationService transactionApplicationService)
        {
            _transactionApplicationService = transactionApplicationService;
        }

        // POST api/deposits
        [HttpPost]
        public async Task<IActionResult> PerformMoneyDeposit([FromBody] PerformMoneyDepositRequestDto dto)
        {
            PerformMoneyTransferResponseDto response = await _transactionApplicationService.PerformDeposit(dto);
            return Ok(response); 
        }

        // GET api/deposits
        [HttpGet]
        public IActionResult Home()
        {
            return Ok();
        }
    }
}
