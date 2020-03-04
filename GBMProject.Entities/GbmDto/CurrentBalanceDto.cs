using GBMProject.Entities.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBMProject.Entities.GbmDto
{
    public class CurrentBalanceDto : InitialBalanceDto
    {
        public static CurrentBalanceDto ConvertFromInitialBalance(InitialBalanceDto initialBalance)
        {
            return new CurrentBalanceDto()
            {
                Cash = initialBalance.Cash,
                IssuerList = initialBalance.IssuerList
            };
        }
    }
}
