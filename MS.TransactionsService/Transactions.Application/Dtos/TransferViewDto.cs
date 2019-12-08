using System;
using System.Collections.Generic;
using System.Text;

namespace Transactions.Application.Dtos
{
    public class TransferViewDto
    {
        public virtual string TransferId { get; set; }
        public virtual string SourceAccountId { get; set; }
        public virtual string SourceAccountName { get; set; }
        public virtual string SourceAccountOwnerId { get; set; }
        public virtual string SourceAccountOwnerName { get; set; }
        public virtual string DestinationAccountId { get; set; }
        public virtual string DestinationAccountName { get; set; }
        public virtual string DestinationAccountOwnerId { get; set; }
        public virtual string DestinationAccountOwnerName { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual DateTime StartedAtUtc { get; set; }
    }
}
