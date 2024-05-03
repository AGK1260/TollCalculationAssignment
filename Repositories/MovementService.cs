using TollCalculation.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace TollCalculation.Repositories
{
    public class MovementService : IMovementService
    {

        private readonly TollContext _dbContext;

        public MovementService(TollContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddCarEnteryAsync(int EnteryInterchangeId, DateTime EnteryTime, string CarRegNumber)
        {
            try
            {
                var log = new MovementLogs
                {
                    EnteryInterchangeId = EnteryInterchangeId,
                    EnteryTime = EnteryTime,
                    CarRegNumber = CarRegNumber
                };

                _dbContext.MovementLogs.Add(log);
                return await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return -1;
            }
            
        }

        public async Task<bool> ValidateCarEnteryAsync(string CarRegNumber)
        {
            var logs = await _dbContext.MovementLogs
                .Where(i => i.CarRegNumber == CarRegNumber && i.ExitInterchangeId == 0)
                .FirstOrDefaultAsync();

            if (logs != null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }


        public async Task<List<TollCalculationRespomse>> GetCalculatedTollAsync(string ExitInterchangeName, DateTime ExitTime, string CarRegNumber)
        {
            try
            {
                var parameters = new List<SqlParameter>
                {
                    //new SqlParameter("@InterchangeName", ExitInterchangeName,SqlDb),
                    //new SqlParameter("@CarRegNumber", CarRegNumber),
                    //new SqlParameter("@ExitTime", ExitTime)
                    new SqlParameter
                    {
                        ParameterName = "@InterchangeName",
                        SqlDbType = SqlDbType.VarChar, // Define the SqlDbType for string
                        Value = ExitInterchangeName
                    },
                    new SqlParameter
                    {
                        ParameterName = "@CarRegNumber",
                        SqlDbType = SqlDbType.VarChar, // Define the SqlDbType for string
                        Value = CarRegNumber
                    },
                    new SqlParameter
                    {
                        ParameterName = "@ExitTime",
                        SqlDbType = SqlDbType.DateTime, // Define the SqlDbType for DateTime
                        Value = ExitTime
                    }

                };

                var result = await _dbContext.tollCalculationRespomses
                    .FromSqlRaw("EXEC sp_CalculateToll @InterchangeName, @CarRegNumber,@ExitTime", parameters.ToArray())
                    .ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                throw;
            }



        }
    }
}
