using NServiceBus;

namespace Accounts.Messages.Events
{
    public class SourceAccountNotFoundEvent : IEvent
    {
        public string TransactionId { get; protected set; }

        public SourceAccountNotFoundEvent(string transactionId)
        {
            TransactionId = transactionId;
        }
    }
}
