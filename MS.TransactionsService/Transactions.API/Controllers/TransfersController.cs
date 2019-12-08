using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Transactions.Application;
using Transactions.Application.Dtos;
using NServiceBus.Logging;
namespace Transactions.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransfersController : ControllerBase
    {
        static readonly ILog log = LogManager.GetLogger<TransfersController>();
        private readonly ITransactionApplicationService _transactionApplicationService;

        public TransfersController(ITransactionApplicationService transactionApplicationService)
        {
            _transactionApplicationService = transactionApplicationService;
        }

        // POST api/transfers
        [HttpPost]
        public async Task<IActionResult> PerformMoneyTransfer([FromBody] PerformMoneyTransferRequestDto dto)
        {
            PerformMoneyTransferResponseDto response = await _transactionApplicationService.PerformTransfer(dto);
            return Ok(response);
        }

        // GET api/transfers
        [HttpGet]
        public IActionResult Home()
        {            
            return Ok();
        }
    }
}
