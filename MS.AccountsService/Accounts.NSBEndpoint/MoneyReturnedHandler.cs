using System.Threading.Tasks;
using Accounts.Messages.Commands;
using Accounts.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Accounts.NSBEndpoint
{
    public class MoneyReturnedHandler : IHandleMessages<MoneyReturnedEvent>
    {
        static readonly ILog log = LogManager.GetLogger<ReturnMoneyCommand>();

        public Task Handle(MoneyReturnedEvent message, IMessageHandlerContext context)
        {
            log.Info($"MoneyReturnedEvent, AccountId = {message.AccountId}");
            return Task.CompletedTask;
        }
    }
}