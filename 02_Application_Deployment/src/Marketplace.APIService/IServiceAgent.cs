using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.APIService
{
    public interface IServiceAgent
    {
        string BaseUrl { get; set; }

        Task<bool> SmartContract2Async(string order, string bindingID);
        Task<bool> SmartContract2Async(string order, string bindingID, CancellationToken cancellationToken);
        Task<bool> SmartContract3Async(string value);
        Task<bool> SmartContract3Async(string value, CancellationToken cancellationToken);
        Task<HttpResponseMessage> SmartContractAsync(string id);
        Task<HttpResponseMessage> SmartContractAsync(string id, CancellationToken cancellationToken);
    }
}