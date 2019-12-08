using NServiceBus;

namespace Accounts.Messages.Events
{
    public class DepositMoneyRejectedEvent : IEvent
    {
        public string TransactionId { get; protected set; }

        public DepositMoneyRejectedEvent(string transactionId)
        {
            TransactionId = transactionId;
        }
    }
}
