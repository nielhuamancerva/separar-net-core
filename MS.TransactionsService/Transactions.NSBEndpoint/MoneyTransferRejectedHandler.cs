using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Transactions.Messages.Events;

namespace Transactions.NSBEndpoint
{    
    public class MoneyTransferRejectedHandler : IHandleMessages<MoneyTransferRejectedEvent>
    {
        static readonly ILog log = LogManager.GetLogger<RequestMoneyTransferHandler>();

        public Task Handle(MoneyTransferRejectedEvent message, IMessageHandlerContext context)
        {
            log.Info($"MoneyTransferRejectedEvent*_*, TransferId = {message.TransferId}");
            return Task.CompletedTask;
        }
    }
}