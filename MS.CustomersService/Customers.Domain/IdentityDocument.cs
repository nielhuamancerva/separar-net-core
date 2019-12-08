using System.Collections.Generic;
using System;
using static System.String;
using UpgFisi.Common.Domain;

namespace Customers.Domain
{
    public class IdentityDocument : ValueObject<IdentityDocument>
    {
        public virtual string Number { get; private set; }

        public static void CheckValidity(string number)
        {
            if (IsNullOrEmpty(number))
            {
                throw new ArgumentNullException(nameof(number), "Identity Document cannot be empty");
            }

            if (number.Length < 8)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "Identity Document cannot be shorter than 8 characters");
            }

            if (number.Length > 8)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "Identity Document cannot be longer than 8 characters");
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}