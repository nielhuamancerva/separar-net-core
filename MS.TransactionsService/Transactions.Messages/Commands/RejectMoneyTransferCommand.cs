using NServiceBus;

namespace Transactions.Messages.Commands
{
    public class RejectMoneyTransferCommand : ICommand
    {
        public string TransferId { get; private set; }

        public RejectMoneyTransferCommand(string transferId)
        {
            TransferId = transferId;
        }
    }
}