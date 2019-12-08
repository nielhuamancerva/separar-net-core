using NServiceBus;

namespace Accounts.Messages.Events
{
    public class MoneyReturnedEvent : IEvent
    {
        public string AccountId { get; protected set; }
        public decimal Amount { get; protected set; }

        public MoneyReturnedEvent(string accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}