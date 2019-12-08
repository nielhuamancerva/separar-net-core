using System.Collections.Generic;
using System;
using static System.String;
using UpgFisi.Common.Domain;

namespace Accounts.Domain
{
    public class AccountNumber : ValueObject<AccountNumber>
    {
        public virtual string Number { get; private set; }

        public static void CheckValidity(string number)
        {
            if (IsNullOrEmpty(number))
            {
                throw new ArgumentNullException(nameof(number), "Account Number cannot be empty");
            }

            if (number.Length < 10)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "Account Number cannot be shorter than 10 characters");
            }

            if (number.Length > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "Account Number cannot be longer than 100 characters");
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}