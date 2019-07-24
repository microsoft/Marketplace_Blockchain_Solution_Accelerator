using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.BC.Offchain.Repo.Models;

namespace Marketplace.APIService
{
    public interface ITransactionIndexerServiceAgent
    {
        IEnumerable<ContractTransaction> GetTransaction(string BindingID);
        IEnumerable<ContractTransaction> GetTransactionByConsignmentCode(string ConsignmentCode);
        IEnumerable<ContractTransaction> GetTransactionByOrderCode(string OrderCode);
        IEnumerable<ContractTransaction> GetTransactionBySupplierId(string SupplierID);
        IEnumerable<Vendors> GetVendors();
        Task<bool> LogTransaction(ContractTransaction transactionInformation);
    }
}