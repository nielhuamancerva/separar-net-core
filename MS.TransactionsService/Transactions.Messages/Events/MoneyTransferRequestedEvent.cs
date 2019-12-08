using NServiceBus;

namespace Transactions.Messages.Events
{
    public class MoneyTransferRequestedEvent : IEvent
    {
        public string TransferId { get; protected set; }
        public string SourceAccountId { get; protected set; }
        public string DestinationAccountId { get; protected set; }
        public decimal Amount { get; protected set; }
        public string Description { get; protected set; }

        public MoneyTransferRequestedEvent(string transferId, string sourceAccountId, string destinationAccountId, decimal amount, string description)
        {
            TransferId = transferId;
            SourceAccountId = sourceAccountId;
            DestinationAccountId = destinationAccountId;
            Amount = amount;
            Description = description;
        }
    }
}