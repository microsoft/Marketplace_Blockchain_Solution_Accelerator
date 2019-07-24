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

namespace AzureFunction
{
	public class SmartContract
	{
		public static string Abi => "[{\"constant\":true,\"inputs\":[],\"name\":\"contoso\",\"outputs\":[{\"name\":\"partyId\",\"type\":\"string\"},{\"name\":\"accountAddress\",\"type\":\"address\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"bindingID\",\"type\":\"string\"},{\"name\":\"_milestone\",\"type\":\"uint8\"}],\"name\":\"setMilestone\",\"outputs\":[{\"name\":\"success\",\"type\":\"bool\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"name\":\"_bindingID\",\"type\":\"string\"},{\"name\":\"_orderCode\",\"type\":\"string\"},{\"name\":\"_orderStatus\",\"type\":\"uint8\"},{\"name\":\"_bookingDate\",\"type\":\"string\"},{\"name\":\"_serviceType\",\"type\":\"uint8\"},{\"name\":\"_serviceDate\",\"type\":\"string\"},{\"name\":\"_partyID\",\"type\":\"string\"},{\"name\":\"_quantity\",\"type\":\"string\"},{\"name\":\"_price\",\"type\":\"string\"},{\"name\":\"_currency\",\"type\":\"string\"}],\"name\":\"NeoPlaceOrder\",\"outputs\":[{\"name\":\"success\",\"type\":\"bool\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"name\":\"_bindingID\",\"type\":\"string\"}],\"name\":\"getConsignment\",\"outputs\":[{\"name\":\"value\",\"type\":\"uint8\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"}]";
		public static string ContractAddress = "0xE4a7f4FfA7c2A4c2a3595D29E240162F2F8Cf400";
		public static string BlockchainRpcEndpoint = "";
	}

	public static class Blockchain
	{
		public static async Task<HttpResponseMessage> CreateRespone(HttpRequestMessage req, string functionName)
		{
			// Get request body
			var jsonBody = await req.Content.ReadAsStringAsync();
			if (string.IsNullOrWhiteSpace(jsonBody)) jsonBody = @"{}";
			var body = JsonConvert.DeserializeObject<JObject>(jsonBody);

			// Get parameters
			var inputParameters = body.Values();
			var arguments = new object[inputParameters.Count()];
			var i = 0;
			foreach (var p in inputParameters.Values())
				arguments[i++] = p.Value<string>();

			var web3 = new Nethereum.Web3.Web3(SmartContract.BlockchainRpcEndpoint);
			var contract = web3.Eth.GetContract(SmartContract.Abi, SmartContract.ContractAddress);

			var functionABI = contract.ContractBuilder.ContractABI.Functions
				.FirstOrDefault(f => f.Name == functionName);

			if (functionABI == null)
				return req.CreateResponse(HttpStatusCode.BadRequest, "Function not found!");

			var functionParameters = functionABI.InputParameters;
			if (functionParameters?.Count() != inputParameters.Count())
				return req.CreateResponse(HttpStatusCode.BadRequest, "Parameters do not match!");

			Function function = contract.GetFunction(functionName);
			Type returnType = GetFunctionReturnType(functionABI);
			IEthCall ethCall = contract.Eth.Transactions.Call;
			var result = await ethCall.SendRequestAsync(function.CreateCallInput(arguments), contract.Eth.DefaultBlock)
				.ConfigureAwait(false);

			FunctionBase functionBase = function;
			PropertyInfo builderBaseProperty = functionBase.GetType()
				.GetProperty("FunctionBuilderBase", BindingFlags.Instance | BindingFlags.NonPublic);
			if (builderBaseProperty != null)
			{
				FunctionBuilderBase builderBase = (FunctionBuilderBase) builderBaseProperty.GetValue(functionBase);
				PropertyInfo funcCallDecoderProperty = builderBase.GetType()
					.GetProperty("FunctionCallDecoder", BindingFlags.Instance | BindingFlags.NonPublic);
				if (funcCallDecoderProperty != null)
				{
					ParameterDecoder decoder = (ParameterDecoder) funcCallDecoderProperty.GetValue(builderBase);
					var results = decoder.DecodeDefaultData(result, functionABI.OutputParameters);

					if (results.Count == 1)
					{
						var resultValue = JsonConvert.SerializeObject(results[0].Result);
						return req.CreateResponse(HttpStatusCode.OK, resultValue);
					}

					var resultMultiValue = Activator.CreateInstance(returnType, results.Select(r => r.Result).ToArray());
					return req.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(resultMultiValue));
				}
			}

			return req.CreateResponse(HttpStatusCode.InternalServerError);
		}

		private static Type GetFunctionReturnType( FunctionABI functionABI )
		{
			if ( functionABI == null )
				return typeof( object );

			Parameter[] parameters = functionABI.OutputParameters;

			if ( parameters == null || parameters.Length == 0 )
				return typeof( object );

			if ( parameters.Length == 1 )
				return parameters[0].ABIType.GetDefaultDecodingType();

			Type taskType = Type.GetType( "System.Tuple`" + parameters.Length );
			List<Type> typeArgs = new List<Type>();

			foreach ( var param in parameters )
			{
				typeArgs.Add( param.ABIType.GetDefaultDecodingType() );
			}

			if (taskType != null)
			{
				Type genericType = taskType.MakeGenericType( typeArgs.ToArray() );

				return genericType;
			}

			return null;
		}
	}
}
                       