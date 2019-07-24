namespace Marketplace.APIService
{
    using Marketplace.BC.Offchain.Repo;
    using Marketplace.BC.Offchain.Repo.Models;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BusinessLogicLayer : IBusinessLogicLayer
    {
        private readonly IRepository<ContractTransaction, Guid> _actorRepository;

        //TODO : update connection string
        private string mongoconnString;

        public BusinessLogicLayer(string connectionString)
        {
            // TODO - move to appsettings.json
            mongoconnString = connectionString;

            _actorRepository =
             new BusinessTransactionRepository<ContractTransaction, Guid>(new MongoClient(mongoconnString));
        }

        public IEnumerable<ContractTransaction> GetAllAdminDetails(int numberOfConsignmentsPerPage = 10)
        {
            var result = _actorRepository.GetAll();
            return result;
        }

        public IEnumerable<ContractTransaction> GetSupplierDetails(string supplierId, int numberOfConsignmentsPerPage = 10)
        {
           var result = _actorRepository.FindAll(new GenericSpecification<ContractTransaction>(x => x.BusinessContractDTO.AdminDetail.Supplier == supplierId));
           return result;
        }

        public async Task<bool> UpdateSupplierStatus(string orderId, string consignmentId, string supplierId, string status)
        {
            var consignemntsForSupplier = _actorRepository.FindAll(new GenericSpecification<ContractTransaction>(x => x.BusinessContractDTO.AdminDetail.Supplier == supplierId));
            var updateConsignmentStatus = consignemntsForSupplier.FirstOrDefault(x => x.BusinessContractDTO.AdminDetail.ConsignmentCode == consignmentId);
            updateConsignmentStatus.BusinessContractDTO.AdminDetail.ConsignmentStatus = status;
            updateConsignmentStatus.OrderDetails.OrderStatus = status;
            updateConsignmentStatus.OrderDetails.Consignments[0].Status = status;
            await _actorRepository.SaveAsync(updateConsignmentStatus);
            return true;
        }
    }
}
