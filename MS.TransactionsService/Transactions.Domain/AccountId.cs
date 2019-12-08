using System;
using UpgFisi.Common.Domain;

namespace Transactions.Domain
{
    public class AccountId : Identity
    {
        public AccountId() : base()
        {
        }

        public AccountId(string id) : base(id)
        {
        }

        public AccountId(Guid guid) : base(guid)
        {
        }
    }
}