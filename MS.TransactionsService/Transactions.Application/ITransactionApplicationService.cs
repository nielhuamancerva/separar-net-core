using System.Threading.Tasks;
using Transactions.Application.Dtos;
using System;
using System.Collections.Generic;
namespace Transactions.Application
{
    public interface ITransactionApplicationService
    {
        Task<PerformMoneyTransferResponseDto> PerformTransfer(PerformMoneyTransferRequestDto dto);
        Task<PerformMoneyTransferResponseDto> PerformDeposit(PerformMoneyDepositRequestDto dto);
        Task<PerformMoneyTransferResponseDto> PerformWithdrawal(PerformMoneyWithdrawalRequestDto dto);
    }
}