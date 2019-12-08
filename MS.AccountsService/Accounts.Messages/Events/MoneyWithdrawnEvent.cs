using NServiceBus;

namespace Accounts.Messages.Events
{
    public class MoneyWithdrawnEvent : IEvent
    {
        public string AccountId { get; protected set; }
        public string TransactionId { get; protected set; }
        public decimal Amount { get; protected set; }
        public decimal Balance { get; protected set; }

        public MoneyWithdrawnEvent(string accountId, string transactionId, decimal amount, decimal balance)
        {
            AccountId = accountId;
            TransactionId = transactionId;
            Amount = amount;
            Balance = balance;
        }
    }
}