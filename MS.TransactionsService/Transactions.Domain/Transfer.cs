using System;
using UpgFisi.Common.Domain; 

namespace Transactions.Domain
{
    public class Transfer
    {
        public virtual string TransferId { get; protected set; }
        public virtual AccountId SourceAccountId { get; protected set; }
        public virtual AccountId DestinationAccountId { get; protected set; }
        public virtual Money Amount { get; protected set; }
        public virtual TransferState TransferState { get; protected set; }
        public virtual string Description { get; protected set; }
        public virtual DateTime StartedAtUtc { get; protected set; }
        public virtual DateTime UpdatedAtUtc { get; protected set; }
        

        protected Transfer()
        {
        }

        public Transfer(
            string transferId,
            AccountId sourceAccountId,
            AccountId destinationAccountId,
            Money amount,
            TransferState transferState,
            string description,
            DateTime startedAtUtc
            )
        {
            TransferId = transferId;
            SourceAccountId = sourceAccountId;
            DestinationAccountId = destinationAccountId;
            Amount = amount;
            TransferState = transferState;
            Description = description;
            StartedAtUtc = startedAtUtc;
            UpdatedAtUtc = startedAtUtc;
            
        }

        public virtual void Complete()
        {
            TransferState = TransferState.COMPLETED;
        }

        public virtual void Reject()
        {
            TransferState = TransferState.REJECTED;
        }

        public virtual void ChangeUpdateAtUtc()
        {
            UpdatedAtUtc = DateTime.UtcNow;
        }
    }
}