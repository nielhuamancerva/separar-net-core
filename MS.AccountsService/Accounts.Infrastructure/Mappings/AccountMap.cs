using Accounts.Domain;
using FluentNHibernate.Mapping;

namespace Accounts.Infrastructure.Mappings
{
    public class AccountMap : ClassMap<Account>
    {
        public AccountMap()
        {
            Id(x => x.AccountId).Column("account_id");
            Component(x => x.AccountNumber, m =>
            {
                m.Map(x => x.Number, "number");
            });
            Component(x => x.Balance, m =>
            {
                m.Map(x => x.Amount, "balance");
            });
            Component(x => x.CustomerId, m =>
            {
                m.Map(x => x.Id, "customer_id");
            });
            Map(x => x.AccountState).CustomType<int>().Column("account_state_id");
            Map(x => x.OpenedAtUtc).Column("opened_at_utc");
            Map(x => x.UpdatedAtUtc).Column("updated_at_utc");
        }
    }
}