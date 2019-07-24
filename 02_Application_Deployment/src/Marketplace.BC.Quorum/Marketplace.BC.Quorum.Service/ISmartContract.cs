using System.Net.Http;
using System.Threading.Tasks;

namespace Marketplace.BC.Quorum.Service
{
    public interface ISmartContract
    {
        string Abi { get; }

        Task<HttpResponseMessage> CreateResponse(string parameters, string functionName);
    }
}