using System.Threading.Tasks;
using Accounts.Messages.Commands;
using Accounts.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using Transactions.Messages.Commands;
using Transactions.Messages.Events;

namespace Transactions.NSBEndpoint
{
    public class MoneyWithdrawalSaga :
        Saga<MoneyTransferSagaData>,
        IAmStartedByMessages<MoneyWithdrawalRequestedEvent>,
        IHandleMessages<MoneyWithdrawnEvent>,
        IHandleMessages<WithdrawMoneyRejectedEvent>,
        IHandleMessages<DestinationAccountNotFoundEvent>
    {
        static readonly ILog log = LogManager.GetLogger<MoneyWithdrawalSaga>();

        public async Task Handle(MoneyWithdrawalRequestedEvent message, IMessageHandlerContext context)
        {
            log.Info($"SAGA MoneyWithdrawalRequestedEvent, TransferId = {message.TransferId}");
            Data.TransferId = message.TransferId;
            Data.DestinationAccountId = message.DestinationAccountId;
            Data.Amount = message.Amount;
            var command = new WithdrawMoneyCommand(
                Data.DestinationAccountId,
                Data.TransferId,
                Data.Amount
            );
            await context.Send(command).ConfigureAwait(false);
        }

        public async Task Handle(MoneyWithdrawnEvent message, IMessageHandlerContext context)
        {
            log.Info($"MoneyWithdrawnEvent, TransactionId = {message.TransactionId}");
            var command = new DepositMoneyCommand(
                Data.DestinationAccountId,
                Data.TransferId,
                Data.Amount
            );
            await context.Send(command).ConfigureAwait(false);
        }


        public async Task Handle(WithdrawMoneyRejectedEvent message, IMessageHandlerContext context)
        {
            log.Info($"WithdrawMoneyRejectedEvent, TransactionId = {message.TransactionId}");
            var command = new RejectMoneyTransferCommand(
                Data.TransferId
            );
            await context.Send(command).ConfigureAwait(false);
        }

        public async Task Handle(DestinationAccountNotFoundEvent message, IMessageHandlerContext context)
        {
            log.Info($"DestinationAccountNotFoundEvent, TransactionId = {message.TransactionId}");
            var returnMoneyCommand = new ReturnMoneyCommand(
                Data.SourceAccountId,
                Data.Amount
            );
            await context.Send(returnMoneyCommand).ConfigureAwait(false);
            var rejectMoneyTransferCommand = new RejectMoneyTransferCommand(
                Data.TransferId
            );
            await context.SendLocal(rejectMoneyTransferCommand).ConfigureAwait(false);
            MarkAsComplete();
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<MoneyTransferSagaData> mapper)
        {
            mapper.ConfigureMapping<MoneyWithdrawalRequestedEvent>(message => message.TransferId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<MoneyWithdrawnEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<WithdrawMoneyRejectedEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<DestinationAccountNotFoundEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);
        }
    }
}
