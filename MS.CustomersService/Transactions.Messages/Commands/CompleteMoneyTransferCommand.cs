using NServiceBus;

namespace Transactions.Messages.Commands
{
    public class CompleteMoneyTransferCommand : ICommand
    {
        public string TransferId { get; private set; }
        
        public CompleteMoneyTransferCommand(string transferId)
        {
            TransferId = transferId;
        }
    }
}