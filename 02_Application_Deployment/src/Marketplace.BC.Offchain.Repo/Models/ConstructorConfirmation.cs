

using System;
using System.Diagnostics;

namespace Marketplace.BC.Offchain.Repo.Models
{
    public sealed class ConstructorConfirmation
    {
        public ConstructorConfirmation()
        {

        }
        
        public TransactionConfirmation TransactionConfirmation { get; set; }
        
        public string Name { get; set; }
        
        public string BindingId { get; set; }
        
        public string NewContractOrTokenId { get; set; }
        
    }
}