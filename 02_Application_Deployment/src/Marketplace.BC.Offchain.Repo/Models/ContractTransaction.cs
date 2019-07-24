using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.BC.Offchain.Repo.Models
{
    public class ContractTransaction : IEntityModel<Guid>
    {

        public ContractTransaction(SampleDTO businessContract,  ConstructorConfirmation contractDeployed)
        {

            InitObject(businessContract);
            ContractID = Guid.NewGuid().ToString();
            businessContract.ContractID = ContractID;
            TransactionConfirmation = contractDeployed.TransactionConfirmation;
            Name = contractDeployed.Name;
            BindingId = contractDeployed.BindingId;
        }

        private void InitObject(SampleDTO businessContract)
        {
            Id = Guid.NewGuid();
            TransactionID = businessContract.TransactionID;
            BusinessContractDTO = businessContract;
            TransactionTime = businessContract.TransactedTime;
        }

        public ContractTransaction(string bindingId, string name, SampleDTO businessContract, TransactionConfirmation transactionConfirmation)
        {
            InitObject(businessContract);
            ContractID = businessContract.ContractID;
            TransactionConfirmation = transactionConfirmation;
            BindingId = bindingId;
            Name = name;
        }

        public const int BindingIdFieldNumber = 1;
        public const int NameFieldNumber = 2;
        public const int TransactionConfirmationFieldNumber = 3;
        public const int DtoFieldNumber = 4;
        
        //Key for Mongo......
        public Guid Id { get; set; }

        //Should be one per each deployed Smart Contract
        public string ContractID { get; set; }
        //Should be assigned per each transaction
        public string TransactionID { get; set; }
        //Business DTO
        public SampleDTO BusinessContractDTO { get; set; }

        public AdminDetails AdminDetails { get; set; }

        public Order OrderDetails { get; set; }

        public DateTime TransactionTime { get; set; }

        public TransactionConfirmation TransactionConfirmation { get; set; }
        public string Name { get; set; }
        public string BindingId { get; set; }
        
    }
}
