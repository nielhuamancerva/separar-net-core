using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;
namespace Transactions.Messages.Commands
{
    public class RequestMoneyDepositCommand: ICommand
    {
        public string TransferId { get; private set; }
    public string DestinationAccountId { get; private set; }
    public decimal Amount { get; private set; }
    public string Description { get; private set; }

    public RequestMoneyDepositCommand(string transferId,string destinationAccountId, decimal amount, string description)
    {
        TransferId = transferId;
        DestinationAccountId = destinationAccountId;
        Amount = amount;
        Description = description;
    }
}
}
