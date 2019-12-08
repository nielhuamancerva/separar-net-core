using NServiceBus;

namespace Transactions.Messages.Events
{
    public class MoneyTransferRejectedEvent  : IEvent
    {
        public string TransferId { get; protected set; }

        public MoneyTransferRejectedEvent(string transferId)
        {
            TransferId = transferId;
        }
    }
}
