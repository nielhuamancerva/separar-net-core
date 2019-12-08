using System.Threading.Tasks;
using Accounts.Domain;
using Accounts.Messages.Commands;
using Accounts.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace Accounts.NSBEndpoint
{
    public class WithdrawMoneyHandler : IHandleMessages<WithdrawMoneyCommand>
    {
        static readonly ILog log = LogManager.GetLogger<WithdrawMoneyCommand>();

        public async Task Handle(WithdrawMoneyCommand message, IMessageHandlerContext context)
        {
            log.Info($"WithdrawMoneyCommand, TransferId = {message.TransactionId}");
            var nhibernateSession = context.SynchronizedStorageSession.Session();
            var accountAggregate = nhibernateSession.Get<Account>(message.AccountId);
            if (accountAggregate == null)
            {
                var sourceAccountNotFoundEvent = new SourceAccountNotFoundEvent(message.TransactionId);
                await context.Publish(sourceAccountNotFoundEvent);
            }
            else
            {
                if (accountAggregate.CanWithdrawMoney(message.Amount))
                {
                    accountAggregate.WithdrawMoney(message.Amount);
                    accountAggregate.ChangeUpdateAtUtc();
                    nhibernateSession.Save(accountAggregate);
                    var moneyWithdrawnEvent = new MoneyWithdrawnEvent
                    (
                        message.AccountId,
                        message.TransactionId,
                        message.Amount,
                        accountAggregate.Balance.Amount
                    );
                    await context.Publish(moneyWithdrawnEvent);
                } else
                {
                    var withdrawMoneyRejectedEvent = new WithdrawMoneyRejectedEvent
                    (
                        message.TransactionId
                    );
                    await context.Publish(withdrawMoneyRejectedEvent);
                }
            }
        }
    }
}