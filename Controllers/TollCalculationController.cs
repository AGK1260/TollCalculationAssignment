using Microsoft.AspNetCore.Mvc;
using TollCalculation.Models;
using TollCalculation.Repositories;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;



namespace TollCalculation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TollCalculationController : Controller
    {

        private readonly IMovementService _movementService;
        private readonly IInterchangeService _interchangeService;

        public TollCalculationController(IMovementService movementService, IInterchangeService interchangeService)
        {
            _movementService = movementService;
            _interchangeService = interchangeService;
        }
        [HttpPost("AddEntry")]
        public async Task<IActionResult> AddEntryAsync([FromBody] MovementLogsRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.InterchangeName) || string.IsNullOrEmpty(request.CarRegNumber) || request.EntryExitDate == default)
            {
                return BadRequest("Interchange name, car registration number, and entry date are required.");
            }

            try
            {
                // Get the interchange ID based on the interchange name
                var interchangeId = await _interchangeService.GetInterchangeId(request.InterchangeName);
                if(interchangeId > 0)
                {   if(await _movementService.ValidateCarEnteryAsync(request.CarRegNumber))
                    {
                        var response = await _movementService.AddCarEnteryAsync(interchangeId, request.EntryExitDate, request.CarRegNumber);
                        return Ok(response);
                    }
                    else
                    {
                        return BadRequest("This car is already enterd , please exit the car first!");
                    }
                }
                else
                {
                    return BadRequest("Interchange name not found, please enter correct interchnagename");
                }
                
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("CalculateToll")]
        public async Task<IActionResult> GetCalculatedTollAsync([FromBody] MovementLogsRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.InterchangeName) || string.IsNullOrEmpty(request.CarRegNumber) || request.EntryExitDate == default)
            {
                return BadRequest("Interchange name, car registration number, and Exit date are required.");
            }

            try
            {
                //validate if car is entered and exit date is greatr than enterdate
                if (await _movementService.ValidateCarEnteryAsync(request.CarRegNumber) == false)
                {
                    var response = await _movementService.GetCalculatedTollAsync(request.InterchangeName, request.EntryExitDate, request.CarRegNumber);

                    if (response == null)
                    {
                        return null;
                    }

                    return Json(response);
                }
                else
                {
                    return BadRequest("This car is not enterd on Road!");
                }
            }
            
        
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
