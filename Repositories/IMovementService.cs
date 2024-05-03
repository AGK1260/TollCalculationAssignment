using TollCalculation.Models;

namespace TollCalculation.Repositories
{
    public interface IMovementService
    {

        public Task<List<TollCalculationRespomse>> GetCalculatedTollAsync(string ExitInterchangeName, DateTime ExitTime, string CarRegNumber);

        public Task<int> AddCarEnteryAsync(int EnteryInterchangeId,DateTime EnteryTime,string CarRegNumber);
        public Task<bool> ValidateCarEnteryAsync(string CarRegNumber);

    }
}
