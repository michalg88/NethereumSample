using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace NethereumSample
{
    [Function("addApples", "bool")]
    public class AddApplesFunction : FunctionMessage
    {
        [Parameter("uint", "toAdd", 1)]
        public uint ToAdd { get; set; }
    }
}
