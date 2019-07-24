using Marketplace.BC.Offchain.Repo;
using Marketplace.BC.Offchain.Repo.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.APIService
{
    public class TransactionIndexerServiceAgent : ITransactionIndexerServiceAgent
    {
        private readonly IRepository<ContractTransaction, Guid> _transactionIndexRepository;

        //TODO : Move connection string to appsettings

        private string mongoconnString;

        public TransactionIndexerServiceAgent(string connectionString)
        {
            // TODO - move to appsettings and replace
            mongoconnString = connectionString;


            _transactionIndexRepository =
              new BusinessTransactionRepository<ContractTransaction, Guid>(new MongoClient(mongoconnString));
        }

        //for Indexer
        public async Task<bool> LogTransaction(ContractTransaction transactionInformation)
        {
            await _transactionIndexRepository.SaveAsync(transactionInformation);

            return true;
        }


        //for Tracker
        public IEnumerable<ContractTransaction> GetTransaction(string BindingID)
        {
            var result = _transactionIndexRepository.FindAll(new GenericSpecification<ContractTransaction>(x => x.BindingId == BindingID));
            return result;
        }

        //for Tracker
        public IEnumerable<ContractTransaction> GetTransactionBySupplierId(string SupplierID)
        {
            var result = _transactionIndexRepository.FindAll(new GenericSpecification<ContractTransaction>(x => x.BusinessContractDTO.AdminDetail.Supplier == SupplierID));
            return result;
        }

        //for Tracker
        public IEnumerable<ContractTransaction> GetTransactionByOrderCode(string OrderCode)
        {
            var result = _transactionIndexRepository.FindAll(new GenericSpecification<ContractTransaction>(x => x.BusinessContractDTO.AdminDetail.OrderCode == OrderCode));
            return result;
        }

        //for Tracker
        public IEnumerable<ContractTransaction> GetTransactionByConsignmentCode(string ConsignmentCode)
        {
            var result = _transactionIndexRepository.FindAll(new GenericSpecification<ContractTransaction>(x => x.BusinessContractDTO.AdminDetail.ConsignmentCode == ConsignmentCode));
            return result;
        }



        public IEnumerable<Vendors> GetVendors()
        {
            var result = _transactionIndexRepository.GetAllVendors();
            return result;
        }



    }
}
