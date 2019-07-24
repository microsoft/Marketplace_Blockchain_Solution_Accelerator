using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.ABI.Model;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.Transactions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Marketplace.BC.Quorum.Service
{

    public class SmartContract : ISmartContract
    {
        // TODO: Move to Appsettings
        public string Abi => "[{\"constant\":true,\"inputs\":[],\"name\":\"contoso\",\"outputs\":[{\"name\":\"partyId\",\"type\":\"string\"},{\"name\":\"accountAddress\",\"type\":\"address\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"bindingID\",\"type\":\"string\"},{\"name\":\"_milestone\",\"type\":\"uint8\"}],\"name\":\"setMilestone\",\"outputs\":[{\"name\":\"success\",\"type\":\"bool\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"_bindingID\",\"type\":\"string\"},{\"name\":\"_orderCode\",\"type\":\"string\"},{\"name\":\"_orderStatus\",\"type\":\"uint8\"},{\"name\":\"_bookingDate\",\"type\":\"string\"},{\"name\":\"_serviceType\",\"type\":\"uint8\"},{\"name\":\"_serviceDate\",\"type\":\"string\"},{\"name\":\"_partyID\",\"type\":\"string\"},{\"name\":\"_quantity\",\"type\":\"string\"},{\"name\":\"_price\",\"type\":\"string\"},{\"name\":\"_currency\",\"type\":\"string\"}],\"name\":\"NeoPlaceOrder\",\"outputs\":[{\"name\":\"success\",\"type\":\"bool\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"_bindingID\",\"type\":\"string\"}],\"name\":\"getConsignment\",\"outputs\":[{\"name\":\"value\",\"type\":\"uint8\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"}]";
        public string ContractAddress = "";

        // Using local development environment
        public string BlockchainRpcEndpoint = "";

        public async Task<HttpResponseMessage> CreateResponse(string parameters, string functionName)
        {
            HttpResponseMessage response = new HttpResponseMessage();

            var body = JsonConvert.DeserializeObject<JObject>(parameters);

            // Get parameters
            var inputParameters = body.Values();
            var arguments = new object[inputParameters.Count()];
            var i = 0;
            foreach (var p in inputParameters.Values())
                arguments[i++] = p.Value<string>();

            Nethereum.Web3.Web3 web3 = new Nethereum.Web3.Web3(BlockchainRpcEndpoint);
            Contract contract = web3.Eth.GetContract(Abi, ContractAddress);

            var functionABI = contract.ContractBuilder.ContractABI.Functions
                .FirstOrDefault(f => f.Name == functionName);


            if (functionABI == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            var functionParameters = functionABI.InputParameters;
            if (functionParameters?.Count() != inputParameters.Count())
            {
                var fp = functionParameters.Count();
                var ip = inputParameters.Count();
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            Function function = contract.GetFunction(functionName);
            Type returnType = GetFunctionReturnType(functionABI);
            EthCall ethCall = contract.Eth.Transactions.Call;
            var result = await ethCall.SendRequestAsync(function.CreateCallInput(arguments), contract.Eth.DefaultBlock).ConfigureAwait(false);

            FunctionBase functionBase = function;
            PropertyInfo builderBaseProperty = functionBase.GetType()
                .GetProperty("FunctionBuilderBase", BindingFlags.Instance | BindingFlags.NonPublic);
            if (builderBaseProperty != null)
            {
                FunctionBuilderBase builderBase = (FunctionBuilderBase)builderBaseProperty.GetValue(functionBase);
                PropertyInfo funcCallDecoderProperty = builderBase.GetType()
                    .GetProperty("FunctionCallDecoder", BindingFlags.Instance | BindingFlags.NonPublic);
                if (funcCallDecoderProperty != null)
                {
                    ParameterDecoder decoder = (ParameterDecoder)funcCallDecoderProperty.GetValue(builderBase);
                    var results = decoder.DecodeDefaultData(result, functionABI.OutputParameters[0]);

                    if (results.Count == 1)
                    {
                        var resultValue = JsonConvert.SerializeObject(results[0].Result);
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }

                    var resultMultiValue = Activator.CreateInstance(returnType, results.Select(r => r.Result).ToArray());
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private static Type GetFunctionReturnType(FunctionABI functionABI)
        {
            if (functionABI == null)
                return typeof(object);

            Parameter[] parameters = functionABI.OutputParameters;

            if (parameters == null || parameters.Length == 0)
                return typeof(object);

            if (parameters.Length == 1)
                return parameters[0].ABIType.GetDefaultDecodingType();

            Type taskType = Type.GetType("System.Tuple`" + parameters.Length);
            List<Type> typeArgs = new List<Type>();

            foreach (var param in parameters)
            {
                typeArgs.Add(param.ABIType.GetDefaultDecodingType());
            }

            if (taskType != null)
            {
                Type genericType = taskType.MakeGenericType(typeArgs.ToArray());

                return genericType;
            }

            return null;
        }
    }
}