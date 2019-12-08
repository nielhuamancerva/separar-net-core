using System;

namespace Customers.Domain
{
    public class Customer
    {
        public virtual string CustomerId { get; protected set; }
        public virtual FirstName FirstName { get; protected set; }
        public virtual LastName LastName { get; protected set; }
        public virtual IdentityDocument IdentityDocument { get; protected set; }
        public virtual bool Active { get; protected set; }
        public virtual DateTime CreatedAtUtc { get; protected set; }
        public virtual DateTime UpdatedAtUtc { get; protected set; }
    }
}