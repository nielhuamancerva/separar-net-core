using System.Threading.Tasks;
using Accounts.Messages.Commands;
using Accounts.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;
using Transactions.Messages.Commands;
using Transactions.Messages.Events;

namespace Transactions.NSBEndpoint
{
    public class MoneyDepositSaga :
        Saga<MoneyTransferSagaData>,
        IAmStartedByMessages<MoneyDepositRequestedEvent>,
        IHandleMessages<MoneyDepositedEvent>,
        IHandleMessages<DepositMoneyRejectedEvent>,
        IHandleMessages<DestinationAccountNotFoundEvent>
    {
        static readonly ILog log = LogManager.GetLogger<MoneyDepositSaga>();

        public async Task Handle(MoneyDepositRequestedEvent message, IMessageHandlerContext context)
        {
            log.Info($"MoneyTransferRequestedEvent, TransferId = {message.TransferId}");
            Data.TransferId = message.TransferId;
            Data.DestinationAccountId = message.DestinationAccountId;
            Data.Amount = message.Amount;
            var command = new DepositMoneyCommand(
                Data.DestinationAccountId,
                Data.TransferId,
                Data.Amount
            );
            await context.Send(command).ConfigureAwait(false);
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
            mapper.ConfigureMapping<MoneyDepositRequestedEvent>(message => message.TransferId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<MoneyDepositedEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<DepositMoneyRejectedEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);

            mapper.ConfigureMapping<DestinationAccountNotFoundEvent>(message => message.TransactionId)
                .ToSaga(sagaData => sagaData.TransferId);
        }
    }
}