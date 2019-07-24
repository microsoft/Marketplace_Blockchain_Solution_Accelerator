using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.BC.Offchain.Repo.Models;

namespace Marketplace.APIService
{
    public interface IBusinessLogicLayer
    {
        IEnumerable<ContractTransaction> GetAllAdminDetails(int numberOfConsignmentsPerPage = 10);
        IEnumerable<ContractTransaction> GetSupplierDetails(string supplierId, int numberOfConsignmentsPerPage = 10);
        Task<bool> UpdateSupplierStatus(string orderId, string consignmentId, string supplierId, string status);
    }
}