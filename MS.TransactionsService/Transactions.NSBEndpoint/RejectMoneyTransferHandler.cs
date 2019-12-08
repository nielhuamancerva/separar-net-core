using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Transactions.Domain;
using Transactions.Messages.Commands;
using Transactions.Messages.Events;

namespace Transactions.NSBEndpoint
{
    public class RejectMoneyTransferHandler : IHandleMessages<RejectMoneyTransferCommand>
    {
        static readonly ILog log = LogManager.GetLogger<CompleteMoneyTransferCommand>();

        public async Task Handle(RejectMoneyTransferCommand message, IMessageHandlerContext context)
        {
            log.Info($"RejectMoneyTransferCommand, TransferId = {message.TransferId}");
            var nhibernateSession = context.SynchronizedStorageSession.Session();
            var transferAggregate = nhibernateSession.Get<Transfer>(message.TransferId);
            transferAggregate.Reject();
            transferAggregate.ChangeUpdateAtUtc();
            nhibernateSession.Save(transferAggregate);
            var moneyTransferRejectedEvent = new MoneyTransferRejectedEvent
            (
                message.TransferId
            );
            await context.Publish(moneyTransferRejectedEvent);
        }
    }
}