using System.Text;
using Accounts.Messages.Events;
using NServiceBus;
using System.Threading.Tasks;
using NServiceBus.Logging;
using Customers.Domain;
using Customers.Messages.Events;
namespace Customers.NSBEndpoint
{
    public class QueryTransferAccountHandler : IHandleMessages<QueryTransferAccountEvent>
    {
        static readonly ILog log = LogManager.GetLogger<QueryTransferAccountHandler>();

        public async Task Handle(QueryTransferAccountEvent message, IMessageHandlerContext context)
        {
            log.Info($"CUSTOMER QueryTransferDataEvent, TransferId = {message.TransferId}");
            var nhibernateSession = context.SynchronizedStorageSession.Session();
            var sourceCustomerAgregate = nhibernateSession.Get<Customer>(message.SourceAccountOwnerId);
            var destinationCustomerAgregate = nhibernateSession.Get<Customer>(message.DestinationAccountOwnerId);
            var queryTransferEvent = new QueryTransferDataCustomerEvent(
                message.TransferId,
                message.SourceAccountId,
                message.SourceAccountName,
                message.SourceAccountOwnerId,
                sourceCustomerAgregate.FirstName.Name + " " + sourceCustomerAgregate.LastName.Name,
                message.DestinationAccountId,
                message.DestinationAccountName,
                message.DestinationAccountOwnerId,
                destinationCustomerAgregate.FirstName.Name +" "+ destinationCustomerAgregate.LastName.Name,
                message.Amount
                );
            await context.Publish(queryTransferEvent);
        }
    }
}
