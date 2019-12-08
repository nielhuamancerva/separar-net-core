using NServiceBus;

namespace Transactions.Messages.Commands
{
    public class RequestMoneyTransferCommand : ICommand
    {
        public string TransferId { get; private set; }
        public string SourceAccountId { get; private set; }
        public string DestinationAccountId { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; }

        public RequestMoneyTransferCommand(string transferId, string sourceAccountId, string destinationAccountId, decimal amount, string description)
        {
            TransferId = transferId;
            SourceAccountId = sourceAccountId;
            DestinationAccountId = destinationAccountId;
            Amount = amount;
            Description = description;
        }
    }
}