using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;
namespace Accounts.Messages.Events
{
    public class QueryTransferAccountEvent : IEvent 
    {
        public  string TransferId { get; private set; }
        public  string SourceAccountId { get; private set; }
        public  string SourceAccountName { get; private set; }
        public  string SourceAccountOwnerId { get; private set; }
        public  string SourceAccountOwnerName { get; private set; }
        public  string DestinationAccountId { get; private set; }
        public  string DestinationAccountName { get; private set; }
        public  string DestinationAccountOwnerId { get; private set; } 
        public  string DestinationAccountOwnerName { get; private set; }
        public  decimal Amount { get; private set; } 

        public QueryTransferAccountEvent(
            string transferId, 
            string sourceAccountId, 
            string sourceAccountName,
            string sourceAccountOwnerId,
            string destinationAccountId,
            string destinationAccountName,
            string destinationAccountOwnerId,
            decimal amount)
        {
            TransferId = transferId;
            SourceAccountId = sourceAccountId;
            SourceAccountName = sourceAccountName;
            SourceAccountOwnerId = sourceAccountOwnerId;
            DestinationAccountId = destinationAccountId;
            DestinationAccountName = destinationAccountName;
            DestinationAccountOwnerId = destinationAccountOwnerId;
            Amount = amount;
        }
    }
}
