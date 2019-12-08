using NServiceBus;

namespace Transactions.Messages.Events
{
    public class MoneyTransferCompletedEvent  : IEvent
    {
        public string TransferId { get; protected set; }

        public MoneyTransferCompletedEvent(string transferId)
        {
            TransferId = transferId;
        }
    }
}
