using FluentNHibernate.Mapping;
using Transactions.Domain;
namespace Transactions.Infrastructure.Mappings
{
    public class TransferViewMap : ClassMap<TransferView>
    {
        public TransferViewMap()
        {
            Table("transfer_view");
            Id(x => x.TransferId).Column("transfer_id");
            Map(x => x.SourceAccountId).Column("source_account_id");
            Map(x => x.SourceAccountName).Column("source_account_name");
            Map(x => x.SourceAccountOwnerId).Column("source_account_owner_id");
            Map(x => x.SourceAccountOwnerName).Column("source_account_owner_name");
            Map(x => x.Amount).Column("amount");
            Map(x => x.DestinationAccountId).Column("destination_account_id");
            Map(x => x.DestinationAccountName).Column("destination_account_name");
            Map(x => x.DestinationAccountOwnerId).Column("destination_account_owner_id");
            Map(x => x.DestinationAccountOwnerName).Column("destination_account_owner_name");
            Map(x => x.StartedAtUtc).Column("operation_create");
        }
    }
}
