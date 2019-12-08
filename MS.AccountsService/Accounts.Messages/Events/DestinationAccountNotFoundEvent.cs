using NServiceBus;

namespace Accounts.Messages.Events
{
    public class DestinationAccountNotFoundEvent : IEvent
    {
        public string TransactionId { get; protected set; }

        public DestinationAccountNotFoundEvent(string transactionId)
        {
            TransactionId = transactionId;
        }
    }
}
