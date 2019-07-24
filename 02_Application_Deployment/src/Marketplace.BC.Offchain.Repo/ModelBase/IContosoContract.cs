using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.BC.Offchain.Repo.Models
{
    /// <summary>
    /// After desiging Smart Contract, The Business DTO Entities should be added 
    /// </summary>
    public interface IContosoContract
    {
        string ContractID { get; set; }
     
        string TransactionID { get; set; }

        DateTime TransactedTime { get; set; }

        AdminDetails AdminDetail { get; set; }
        /// <summary>
        /// /have to implemented
        /// </summary>
        string Description { get; set; }

    }
}
