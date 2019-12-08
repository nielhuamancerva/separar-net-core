using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Transactions.Application;
using Transactions.Application.Dtos;
using NServiceBus.Logging;
namespace Withdrawals.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawalsController : ControllerBase
    {
        static readonly ILog log = LogManager.GetLogger<WithdrawalsController>();
        private readonly ITransactionApplicationService _transactionApplicationService;

        public WithdrawalsController(ITransactionApplicationService transactionApplicationService)
        {
            _transactionApplicationService = transactionApplicationService;
        }

        // POST api/withdrawals
        [HttpPost]
        public async Task<IActionResult> PerformMoneyWithdrawal([FromBody] PerformMoneyWithdrawalRequestDto dto)
        {
            PerformMoneyTransferResponseDto response = await _transactionApplicationService.PerformWithdrawal(dto); 
            return Ok(response); 
        }

        // GET api/withdrawals
        [HttpGet]
        public IActionResult Home()
        {
            return Ok();
        }
    }
}
