using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace NethereumSample
{
    [Function("getApples", "uint")]
    public class GetApplesFunction : FunctionMessage
    {
        [Parameter("address", "addr", 1)]
        public string Address { get; set; }
    }
}
