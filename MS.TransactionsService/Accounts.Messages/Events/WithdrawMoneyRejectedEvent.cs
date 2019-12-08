using NServiceBus;

namespace Accounts.Messages.Events
{
    public class WithdrawMoneyRejectedEvent : IEvent
    {
        public string TransactionId { get; protected set; }

        public WithdrawMoneyRejectedEvent(string transactionId)
        {
            TransactionId = transactionId;
        }
    }
}
