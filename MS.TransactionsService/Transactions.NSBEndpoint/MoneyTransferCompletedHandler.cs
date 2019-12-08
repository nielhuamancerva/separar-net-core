using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Transactions.Messages.Events;

namespace Transactions.NSBEndpoint
{    
    public class MoneyTransferCompletedHandler : IHandleMessages<MoneyTransferCompletedEvent>
    {
        static readonly ILog log = LogManager.GetLogger<RequestMoneyTransferHandler>();

        public Task Handle(MoneyTransferCompletedEvent message, IMessageHandlerContext context)
        {
            log.Info($"MoneyTransferCompletedEvent!!!, TransferId = {message.TransferId}");
            return Task.CompletedTask;
        }
    }
}