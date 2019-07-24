using System;

namespace Marketplace.BC.Offchain.Repo.Models
{
    public class SampleDTO 
    {
        public string ContractID { get; set; }
        public string TransactionID { get; set; }
        public DateTime TransactedTime { get; set; }

        public AdminDetails AdminDetail { get; set; }

        public Order OrderDetail { get; set; }

        //added for testing
        public string Description { get; set; }
        public Guid Id { get ; set ; }



    }
}