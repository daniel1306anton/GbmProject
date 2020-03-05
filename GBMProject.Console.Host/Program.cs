using GBMProject.Factory;
using System.Linq;

namespace GBMProject.Console.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new SellOrderFactory();
            var response = factory.Execute();
            if(response.ErrorList != null && response.ErrorList.Any())
            {
                foreach (var item in response.ErrorList)
                {
                    System.Console.WriteLine(item.Code + " " + item.Message);
                }
            }
            else
            {
                System.Console.WriteLine("Finish Execution Robot");
            }
        }
    }
}
