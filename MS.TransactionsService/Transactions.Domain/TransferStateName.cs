using System;
using System.Collections.Generic;
using UpgFisi.Common.Domain;
using static System.String;

namespace Transactions.Domain
{
    public class TransferStateName : ValueObject<TransferStateName>
    {
        public virtual string Name { get; private set; }

        public TransferStateName()
        {
            Name = "";
        }

        public TransferStateName(string name)
        {
            Name = name;
        }

        public static void CheckValidity(string name)
        {
            if (IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Transfer State Name cannot be empty");
            }
            int maxCharacters = 50;
            if (name.Length > maxCharacters)
            {
                throw new ArgumentOutOfRangeException(nameof(name), "Transfer State Name cannot be longer than " + maxCharacters + " characters");
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}