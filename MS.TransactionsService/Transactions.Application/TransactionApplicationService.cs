using NServiceBus;
using System;
using System.Threading.Tasks;
using Transactions.Application.Dtos;
using Transactions.Messages.Commands;
using NServiceBus.Logging;
using System.Collections.Generic;
namespace Transactions.Application
{
    public class TransactionApplicationService : ITransactionApplicationService
    {
        static readonly ILog log = LogManager.GetLogger<TransactionApplicationService>();
        private readonly IMessageSession _messageSession;

        public TransactionApplicationService(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        public async Task<PerformMoneyTransferResponseDto> PerformTransfer(PerformMoneyTransferRequestDto dto)
        {
            try
            {
                var transferId = Guid.NewGuid().ToString();
                var command = new RequestMoneyTransferCommand(
                    transferId,
                    dto.SourceAccountId,
                    dto.DestinationAccountId,
                    dto.Amount,
                    dto.Description
                );
                await _messageSession.Send(command).ConfigureAwait(false);
                return new PerformMoneyTransferResponseDto
                {
                    Response = "OK"
                };
            }
            catch (Exception ex)
            {
                return new PerformMoneyTransferResponseDto
                {
                    Response = "ERROR: " + ex.Message + " -- " + ex.StackTrace
                };
            }
        }

        public async Task<PerformMoneyTransferResponseDto> PerformDeposit(PerformMoneyDepositRequestDto dto)
        {
            try
            {
                var transferId = Guid.NewGuid().ToString();
                var command = new RequestMoneyDepositCommand(
                    transferId,
                    dto.DestinationAccountId,
                    dto.Amount,
                    dto.Description
                );
                log.Info("PerformDeposit DestinationAccountId " + dto.DestinationAccountId + ",Amount" + dto.Amount);
                await _messageSession.Send(command).ConfigureAwait(false);
                return new PerformMoneyTransferResponseDto
                {
                    Response = "Depósito realizado correctamente"
                };
            }
            catch (Exception ex)
            {
                return new PerformMoneyTransferResponseDto
                {
                    Response = "ERROR: PerformMoneyDepositRequestDto(" + dto.DestinationAccountId + ") " + ex.Message + " -- " + ex.StackTrace
                };
            }
        }

        public async Task<PerformMoneyTransferResponseDto> PerformWithdrawal(PerformMoneyWithdrawalRequestDto dto)
        {
            try
            {
                var transferId = Guid.NewGuid().ToString();
                var command = new RequestMoneyWithdrawalCommand( 
                    transferId,
                    dto.DestinationAccountId,
                    dto.Amount,
                    dto.Description
                );
                log.Info("PerformWithdrawal DestinationAccountId " + dto.DestinationAccountId + ",Amount" + dto.Amount);
                await _messageSession.Send(command).ConfigureAwait(false);
                return new PerformMoneyTransferResponseDto
                {
                    Response = "Retiro realizado correctamente"
                };
            }
            catch (Exception ex)
            {
                return new PerformMoneyTransferResponseDto
                {
                    Response = "ERROR: PerformMoneyWithdrawalRequestDto(" + dto.DestinationAccountId + ") " + ex.Message + " -- " + ex.StackTrace
                };
            }
        }

    }
}