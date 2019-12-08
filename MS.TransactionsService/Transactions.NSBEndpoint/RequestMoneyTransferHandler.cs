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
    public class RequestMoneyTransferHandler : IHandleMessages<RequestMoneyTransferCommand>
    {
        static readonly ILog log = LogManager.GetLogger<RequestMoneyTransferHandler>();

        public async Task Handle(RequestMoneyTransferCommand message, IMessageHandlerContext context)
        {
            log.Info($"ENTRANDO A PROCESAR RETIRO, TransferId = {message.TransferId}");
            log.Info($"Received RequestMoneyTransferCommand, TransferId = {message.TransferId}");
            var nhibernateSession = context.SynchronizedStorageSession.Session();
            var transferAggregate = new Transfer(
                message.TransferId,
                new AccountId(message.SourceAccountId),
                new AccountId(message.DestinationAccountId),
                Money.Dollars(message.Amount),
                TransferState.STARTED,
                message.Description,
                DateTime.UtcNow
            );
            log.Info($"new AccountId(message.SourceAccountId) = {new AccountId(message.SourceAccountId)}");
            log.Info($"new AccountId(message.DestinationAccountId) = {new AccountId(message.DestinationAccountId)}");
            nhibernateSession.Save(transferAggregate);
            var moneyTransferRequestedEvent = new MoneyTransferRequestedEvent
            (
                message.TransferId,
                message.SourceAccountId,
                message.DestinationAccountId,
                message.Amount,
                message.Description
            );
            await context.Publish(moneyTransferRequestedEvent);
        }
    }
}