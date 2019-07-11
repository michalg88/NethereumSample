using System;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace NethereumSample
{
    public class Program
    {
        static void Main(string[] args)
        {
            TransferEther().GetAwaiter();

         //   DeployContract().GetAwaiter();

         //   RunContract().GetAwaiter();

            Console.ReadLine();
        }

        private static async Task TransferEther()
        {
            // sender account
            var privateKey = "0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7";
            var account = new Account(privateKey);
            var web3 = new Web3(account);

            // receiver address
            var toAddress = "0x14f022d72158410436cbd66f5dd8bf6d2d129924";

            var transaction = await web3.Eth.GetEtherTransferService()
                .TransferEtherAndWaitForReceiptAsync(toAddress, 3.50m);

            // transaction summary
            Console.WriteLine($"Gas used: {transaction.GasUsed.Value}");
            Console.WriteLine($"Transaction status: {transaction.Status.Value}");

            // check balances
            var frombalance = await web3.Eth.GetBalance.SendRequestAsync(account.Address);
            Console.WriteLine($"Sender Balance in Wei: {frombalance.Value}");

            var balance = await web3.Eth.GetBalance.SendRequestAsync(toAddress);
            Console.WriteLine($"Receiver Balance in Wei: {balance.Value}");
        }

        private static async Task DeployContract()
        {
            var privateKey = "0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7";
            var account = new Account(privateKey);

            var deploymentMessage = new ApplesContractDeployment();

            var web3 = new Web3(account);
            var deploymentHandler = web3.Eth.GetContractDeploymentHandler<ApplesContractDeployment>();
            var deployTransactionReceipt = await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage);

            Console.WriteLine("Gas used: " + deployTransactionReceipt.GasUsed.Value);
            Console.WriteLine("Address: " + deployTransactionReceipt.ContractAddress);
        }

        private static async Task RunContract()
        {
            var privateKey = "0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7";
            var account = new Account(privateKey);
            var web3 = new Web3(account);

            // add apples
            var addApplesFunction = new AddApplesFunction { ToAdd = 14 };

            var addApplesHandler = web3.Eth.GetContractTransactionHandler<AddApplesFunction>();

            var transactionReceipt =
                await addApplesHandler.SendRequestAndWaitForReceiptAsync("0xe672061cbf6a53308888bcaf6e06e08ed34c23c6",
                    addApplesFunction);
            Console.WriteLine("Gas used (add apples): " + transactionReceipt.GasUsed.Value);

            // get apples number
            var getFunctionMessage = new GetApplesFunction { Address = account.Address };
            var balanceHandler = web3.Eth.GetContractQueryHandler<GetApplesFunction>();

            var apples =
                await balanceHandler.QueryAsync<uint>("0xe672061cbf6a53308888bcaf6e06e08ed34c23c6", getFunctionMessage);
            Console.WriteLine("Total apples: " + apples);
        }
    }
}