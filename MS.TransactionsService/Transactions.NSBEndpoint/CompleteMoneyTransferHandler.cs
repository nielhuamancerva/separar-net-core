using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Transactions.Domain;
using Transactions.Messages.Commands;
using Transactions.Messages.Events;
using Accounts.Messages.Events;
namespace Transactions.NSBEndpoint
{
    public class CompleteMoneyTransferHandler : IHandleMessages<CompleteMoneyTransferCommand>
    {
        static readonly ILog log = LogManager.GetLogger<CompleteMoneyTransferCommand>();

        public async Task Handle(CompleteMoneyTransferCommand message, IMessageHandlerContext context)
        {
            log.Info($"CompleteMoneyTransferCommand, TransferId = {message.TransferId}");
            var nhibernateSession = context.SynchronizedStorageSession.Session();
            var transferAggregate = nhibernateSession.Get<Transfer>(message.TransferId);
            transferAggregate.Complete();
            transferAggregate.ChangeUpdateAtUtc();
            nhibernateSession.Save(transferAggregate);
            var moneyTransferCompletedEvent = new MoneyTransferCompletedEvent
            (
                message.TransferId
            );
            await context.Publish(moneyTransferCompletedEvent);
            var queryTransferEvent = new QueryTransferEvent( 
                transferAggregate.TransferId,
                transferAggregate.SourceAccountId.Id.ToString(),
                transferAggregate.DestinationAccountId.Id.ToString(),
                transferAggregate.Amount.Amount
                );
            await context.Publish(queryTransferEvent);
        }
    }
}