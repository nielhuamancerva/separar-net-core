using System;
using UpgFisi.Common.Domain;

namespace Transactions.Domain
{
    public class TransferId : Identity
    {
        public TransferId() : base()
        {
        }

        public TransferId(string id) : base(id)
        {
        }

        public TransferId(Guid guid) : base(guid)
        {
        }
    }
}