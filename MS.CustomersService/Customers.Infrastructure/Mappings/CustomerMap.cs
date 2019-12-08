using Customers.Domain;
using FluentNHibernate.Mapping;

namespace Customers.Infrastructure.Mappings
{
    public class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Id(x => x.CustomerId).Column("customer_id");
            Component(x => x.FirstName, m =>
            {
                m.Map(x => x.Name, "first_name");
            });
            Component(x => x.LastName, m =>
            {
                m.Map(x => x.Name, "last_name");
            });
            Component(x => x.IdentityDocument, m =>
            {
                m.Map(x => x.Number, "identity_document");
            });
            Map(x => x.Active).CustomType<bool>().Column("active");
            Map(x => x.CreatedAtUtc).Column("created_at_utc");
            Map(x => x.UpdatedAtUtc).Column("updated_at_utc");
        }
    }
}