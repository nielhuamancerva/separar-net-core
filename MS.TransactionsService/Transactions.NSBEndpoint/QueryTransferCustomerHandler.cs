using System.Threading.Tasks;
using Accounts.Messages.Commands;
using Accounts.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using Customers.Messages.Events;
using Transactions.Domain;
namespace Transactions.NSBEndpoint
{
    class QueryTransferCustomerHandler: IHandleMessages<QueryTransferDataCustomerEvent>
    {
        static readonly ILog log = LogManager.GetLogger<QueryTransferCustomerHandler>();

        public async Task Handle(QueryTransferDataCustomerEvent message, IMessageHandlerContext context)
        {
            log.Info($"TRANSACTION COMPLETE QUERY QueryTransferDataEvent, SourceAccountName = {message.SourceAccountName}");
            var nhibernateSession = context.SynchronizedStorageSession.Session();
            var transfer = new TransferView(); 
            transfer.TransferId = message.TransferId;
            transfer.SourceAccountId = message.SourceAccountId;
            transfer.SourceAccountName = message.SourceAccountName;
            transfer.SourceAccountOwnerId = message.SourceAccountOwnerId;
            transfer.SourceAccountOwnerName = message.SourceAccountOwnerName;
            transfer.Amount = message.Amount;
            transfer.DestinationAccountId = message.DestinationAccountId;
            transfer.DestinationAccountName = message.DestinationAccountName;
            transfer.DestinationAccountOwnerId = message.DestinationAccountOwnerId;
            transfer.DestinationAccountOwnerName = message.DestinationAccountOwnerName;
            transfer.StartedAtUtc = new System.DateTime();
            nhibernateSession.Save(transfer);
            //nhibernateSession.CreateSQLQuery("INSERT INTO transfer_view(transaction_id, source_account_id,source_account_name,source_account_owner_id,source_account_owner_name,destination_account_id,destination_account_name,destination_account_owner_id,destination_account_owner_name,amount) VALUES ('" + message.TransferId + "', '" + message.DestinationAccountId + "','"+message.SourceAccountName+ "','" + message.SourceAccountOwnerId + "','" + message.SourceAccountOwnerName + "', '" + message.DestinationAccountId + "','" + message.DestinationAccountName + "','" + message.DestinationAccountOwnerId + "','" + message.DestinationAccountOwnerName + "', " + message.Amount + ")").ExecuteUpdate();
            await Task.CompletedTask;
        }
    }
}
