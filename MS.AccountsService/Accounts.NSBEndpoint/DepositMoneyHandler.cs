using System.Threading.Tasks;
using Accounts.Domain;
using Accounts.Messages.Commands;
using Accounts.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Accounts.NSBEndpoint
{
    public class DepositMoneyHandler : IHandleMessages<DepositMoneyCommand>
    {
        static readonly ILog log = LogManager.GetLogger<DepositMoneyCommand>();

        public async Task Handle(DepositMoneyCommand message, IMessageHandlerContext context)
        {
            log.Info($"DepositMoneyCommand, TransferId = {message.TransactionId}");
            var nhibernateSession = context.SynchronizedStorageSession.Session();
            var accountAggregate = nhibernateSession.Get<Account>(message.AccountId);
            if (accountAggregate == null)
            {
                var destinationAccountNotFoundEvent = new DestinationAccountNotFoundEvent(message.TransactionId);
                await context.Publish(destinationAccountNotFoundEvent);
            }
            else
            {
                accountAggregate.DepositMoney(message.Amount);
                accountAggregate.ChangeUpdateAtUtc();
                nhibernateSession.Save(accountAggregate);
                var moneyDepositedEvent = new MoneyDepositedEvent
                (
                    message.AccountId,
                    message.TransactionId,
                    message.Amount,
                    accountAggregate.Balance.Amount
                );
                await context.Publish(moneyDepositedEvent);
            }
        }
    }
}