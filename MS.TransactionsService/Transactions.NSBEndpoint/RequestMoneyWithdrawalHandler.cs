using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Threading.Tasks;
using Transactions.Domain;
using Transactions.Messages.Commands;
using Transactions.Messages.Events;
using UpgFisi.Common.Domain;

namespace Transactions.NSBEndpoint
{
    public class RequestMoneyWithdrawalHandler : IHandleMessages<RequestMoneyWithdrawalCommand> 
    {
        static readonly ILog log = LogManager.GetLogger<PerformMoneyDepositRequestHandler>();

        public async Task Handle(RequestMoneyWithdrawalCommand message, IMessageHandlerContext context)
        {
            log.Info($"ENTRANDO A RETIRO, TransferId = {message.TransferId}");
            log.Info($"Received RequestMoneyWithdrawalCommand, TransferId = {message.TransferId}");
            var nhibernateSession = context.SynchronizedStorageSession.Session();
            var transferAggregate = new Transfer(
                message.TransferId,
                new AccountId(message.DestinationAccountId),
                new AccountId(message.DestinationAccountId),
                Money.Dollars(message.Amount),
                TransferState.STARTED,
                message.Description,
                DateTime.UtcNow
            );
            nhibernateSession.Save(transferAggregate);
            var moneyTransferRequestedEvent = new MoneyWithdrawalRequestedEvent
            (
                message.TransferId,
                message.DestinationAccountId,
                message.Amount,
                message.Description
            );
            await context.Publish(moneyTransferRequestedEvent);
        }
    }
}
