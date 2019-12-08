using System.Threading.Tasks;
using Accounts.Domain;
using Accounts.Messages.Commands;
using Accounts.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Accounts.NSBEndpoint
{
    class QueryTransferHandler: IHandleMessages<QueryTransferEvent>
    {
        static readonly ILog log = LogManager.GetLogger<ReturnMoneyCommand>();

        public async Task Handle(QueryTransferEvent message, IMessageHandlerContext context)
        {
            log.Info($"QUERY ACCOUNT QueryTransferEvent, TransferId = {message.TransferId}");
            var nhibernateSession = context.SynchronizedStorageSession.Session();
            var sourceAccount = nhibernateSession.Get<Account>(message.SourceAccountId);
            var destinationAccount = nhibernateSession.Get<Account>(message.DestinationAccountId);
            var queryTransferDataEvent = new QueryTransferAccountEvent
                 (
                     message.TransferId,
                     message.SourceAccountId,
                     sourceAccount.AccountNumber.Number,
                     sourceAccount.CustomerId.Id.ToString(),
                     message.DestinationAccountId,
                     destinationAccount.AccountNumber.Number,
                     destinationAccount.CustomerId.Id.ToString(),
                     message.Amount
                 );
            await context.Publish(queryTransferDataEvent);
        }
    }
}
