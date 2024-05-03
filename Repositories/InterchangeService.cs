using TollCalculation.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace TollCalculation.Repositories
{
    public class InterchangeService : IInterchangeService
    {
        private readonly TollContext _dbContext;

        public InterchangeService(TollContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetInterchangeId(string InterchnageName)
        {
            var interchange = await _dbContext.Interchanges
                .Where(i => i.InterchnageName == InterchnageName)
                .FirstOrDefaultAsync();

            if (interchange != null)
            {
                return interchange.InterchangeId;
            }
            else
            {
                return 0;
            }
            

        }
    }
}
