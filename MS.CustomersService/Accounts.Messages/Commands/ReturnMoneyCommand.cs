using NServiceBus;

namespace Accounts.Messages.Commands
{
    public class ReturnMoneyCommand : ICommand
    {
        public string AccountId { get; private set; }
        public decimal Amount { get; private set; }

        public ReturnMoneyCommand(string accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}