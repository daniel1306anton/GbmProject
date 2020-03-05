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
