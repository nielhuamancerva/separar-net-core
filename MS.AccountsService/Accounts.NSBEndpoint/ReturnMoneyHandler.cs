using System.Threading.Tasks;
using Accounts.Domain;
using Accounts.Messages.Commands;
using Accounts.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Accounts.NSBEndpoint
{
    public class ReturnMoneyHandler : IHandleMessages<ReturnMoneyCommand>
    {
        static readonly ILog log = LogManager.GetLogger<ReturnMoneyCommand>();

        public async Task Handle(ReturnMoneyCommand message, IMessageHandlerContext context)
        {
            log.Info($"ReturnMoneyCommand, AccountId = {message.AccountId}");
            var nhibernateSession = context.SynchronizedStorageSession.Session();
            var accountAggregate = nhibernateSession.Get<Account>(message.AccountId);
            if (accountAggregate == null)
            {
                var sourceAccountNotFoundEvent = new SourceAccountNotFoundEvent(message.AccountId);
                await context.Publish(sourceAccountNotFoundEvent);
            }
            else
            {
                accountAggregate.ReturnMoney(message.Amount);
                nhibernateSession.Save(accountAggregate);
                var moneyReturnedEvent = new MoneyReturnedEvent
                (
                    message.AccountId,
                    message.Amount
                );
                await context.Publish(moneyReturnedEvent);
            }
        }
    }
}