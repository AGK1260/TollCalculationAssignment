using TollCalculation.Models;

namespace TollCalculation.Repositories
{
    public interface IInterchangeService
    {
        public Task<int> GetInterchangeId(string InterchnageName);
    }
}

