using FluentNHibernate.Mapping;
using Transactions.Domain;

namespace Transactions.Infrastructure.Mappings
{
    public class TransferStateMap //: ClassMap<TransferState>
    {
        /*public TransferStateMap()
        {
            Id(x => x.TransferStateId).Column("transfer_state_id");
            Component(x => x.Name, m =>
            {
                m.Map(x => x.Name, "state_name");
            });
            Map(x => x.CreatedAt).Column("created_at_utc");
            Map(x => x.UpdatedAt).Column("updated_at_utc");
        }*/
    }
}
