using NServiceBus;

namespace Accounts.Messages.Commands
{
    public class WithdrawMoneyCommand : ICommand
    {
        public string AccountId { get; private set; }
        public string TransactionId { get; private set; }
        public decimal Amount { get; private set; }

        public WithdrawMoneyCommand(string accountId, string transactionId, decimal amount)
        {
            AccountId = accountId;
            TransactionId = transactionId;
            Amount = amount;
        }
    }
}