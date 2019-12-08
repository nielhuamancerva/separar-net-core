using NServiceBus;

namespace Accounts.Messages.Commands
{
    public class DepositMoneyCommand : ICommand
    {
        public string AccountId { get; private set; }
        public string TransactionId { get; private set; }
        public decimal Amount { get; private set; }

        public DepositMoneyCommand(string accountId, string transactionId, decimal amount)
        {
            AccountId = accountId;
            TransactionId = transactionId;
            Amount = amount;
        }
    }
}