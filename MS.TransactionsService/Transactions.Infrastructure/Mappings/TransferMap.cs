using FluentNHibernate.Mapping;
using Transactions.Domain;

namespace Transactions.Infrastructure.Mappings
{
    public class TransferMap : ClassMap<Transfer>
    {
        public TransferMap()
        {
            Id(x => x.TransferId).Column("transfer_id");
            Component(x => x.SourceAccountId, m =>
            {
                m.Map(x => x.Id, "source_account_id");
            });
            Component(x => x.DestinationAccountId, m =>
            {
                m.Map(x => x.Id, "destination_account_id");
            });
            Component(x => x.Amount, m =>
            {
                m.Map(x => x.Amount, "amount");
            });
            Map(x => x.TransferState).CustomType<int>().Column("transfer_state_id");
            Map(x => x.StartedAtUtc).Column("started_at_utc");
            Map(x => x.UpdatedAtUtc).Column("updated_at_utc");
        }
    }
}