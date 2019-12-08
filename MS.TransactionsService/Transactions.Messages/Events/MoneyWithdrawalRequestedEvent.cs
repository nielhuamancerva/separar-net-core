using NServiceBus;

namespace Transactions.Messages.Events
{
    public class MoneyWithdrawalRequestedEvent : IEvent
    {
        public string TransferId { get; protected set; }
        public string DestinationAccountId { get; protected set; }
        public decimal Amount { get; protected set; }
        public string Description { get; protected set; } 

        public MoneyWithdrawalRequestedEvent(string transferId, string destinationAccountId, decimal amount, string description)
        {
            TransferId = transferId;
            DestinationAccountId = destinationAccountId;
            Amount = amount;
            Description = description;
        }
    }
}
