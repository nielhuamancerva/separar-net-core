using System.Threading.Tasks;
using Accounts.Messages.Commands;
using Accounts.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using Transactions.Messages.Commands;
using Transactions.Messages.Events;

namespace Transactions.NSBEndpoint
{
    public class MoneyTransferSaga :
        Saga<MoneyTransferSagaData>,
        IAmStartedByMessages<MoneyTransferRequestedEvent>, 
        IHandleMessages<MoneyWithdrawnEvent>,
        IHandleMessages<WithdrawMoneyRejectedEvent>,
        IHandleMessages<MoneyDepositedEvent>,
        IHandleMessages<DepositMoneyRejectedEvent>,
        IHandleMessages<SourceAccountNotFoundEvent>,
        IHandleMessages<DestinationAccountNotFoundEvent>
    {
        static readonly ILog log = LogManager.GetLogger<MoneyTransferSaga>();

        public async Task Handle(MoneyTransferRequestedEvent message, IMessageHandlerContext context)
        {
            log.Info($"MoneyTransferRequestedEvent, TransferId = {message.TransferId}");
            Data.TransferId = message.TransferId;
            Data.SourceAccountId = message.SourceAccountId;
            Data.DestinationAccountId = message.DestinationAccountId;
            Data.Amount = message.Amount;
            var command = new WithdrawMoneyCommand(
                Data.SourceAccountId, 
                Data.TransferId,
                Data.Amount
            );
            await context.Send(command).ConfigureAwait(false);
        }

        public async Task Handle(SourceAccountNotFoundEvent message, IMessageHandlerContext context)
        {
            log.Info($"SourceAccountNotFoundEvent, TransactionId = {message.TransactionId}");
            var command = new RejectMoneyTransferCommand(
                Data.TransferId
            );
            await context.SendLocal(command).ConfigureAwait(false);
            MarkAsComplete();
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

        public async Task Handle(MoneyDepositedEvent message, IMessageHandlerContext context)
        {
            log.Info($"MoneyDepositedEvent, TransactionId = {message.TransactionId}");
            var command = new CompleteMoneyTransferCommand(
                Data.TransferId
            );
            await context.SendLocal(command).ConfigureAwait(false);
            MarkAsComplete();
        }

        public async Task Handle(DepositMoneyRejectedEvent message, IMessageHandlerContext context)
        {
            log.Info($"DepositMoneyRejectedEvent, TransferId = {message.TransactionId}");
            var command = new RejectMoneyTransferCommand(
                Data.TransferId
            );
            await context.SendLocal(command).ConfigureAwait(false);
            MarkAsComplete();
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<MoneyTransferSagaData> mapper)
        {
            mapper.ConfigureMapping<MoneyTransferRequestedEvent>(message => message.TransferId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<MoneyWithdrawnEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<WithdrawMoneyRejectedEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<MoneyDepositedEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<DepositMoneyRejectedEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<SourceAccountNotFoundEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<DestinationAccountNotFoundEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);
        }
    }
}