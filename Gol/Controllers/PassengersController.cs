using Gol.Domains;
using Gol.Domains.Repository;
using Gol.Entities;
using Gol.Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gol.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PassengersController : ControllerBase
    {
        private readonly IPassengerRepository _repository;

        public PassengersController([FromServices]IPassengerRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("InsertPassenger")]
        public async Task<IActionResult> InsertPassenger([FromBody]Passenger Passenger)
        {
            try
            {
                Passenger.RegistryCreationDate = DateTime.Now;

                var result = await _repository.InsertPassenger(Passenger);
                return Ok(Passenger);
            }
            catch(AirplaneException e)
            {
                return BadRequest(e.Message);
            }
            catch(Exception e)
            {
                return BadRequest("Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost]
        [Route("InsertPassengerToAirplane")]
        public async Task<IActionResult> InsertPassengerToAirplane([FromBody]Passenger Passenger)
        {
            try
            {
                var result = await _repository.InsertPassengerToAirplane(Passenger);
                return Ok(result);
            }
            catch (AirplaneException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest("Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet]
        [Route("ChangePassenger/{id}/{idAirplane}")]
        public async Task<IActionResult> ChangePassenger(int id, int idAirplane)
        {
            try
            {
                var result = await _repository.ChangePassenger(id, idAirplane);
                return Ok(result);
            }
            catch (AirplaneException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest("Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet()]
        [Route("ListAllPassengerByAirplane/{id}")]
        public async Task<IActionResult> ListAllPassengerByAirplane(int id)
        {
            try
            {
                var result = await _repository.ListAllPassengerByAirplane(id);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (AirplaneException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest("Ocorreu um erro interno no servidor.");
            }
        }

        [HttpGet()]
        [Route("GetAllPassenger")]
        public async Task<IActionResult> GetAllPassenger()
        {
            try
            {
                var result = await _repository.GetAllPassenger();
                return Ok(result);
            }
            catch (AirplaneException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest("Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost]
        [Route("UpdatePassenger")]
        public async Task<IActionResult> UpdatePassenger([FromBody]Passenger passenger)
        {
            try
            {
                passenger.RegistryCreationDate = DateTime.Now;

                var result = await _repository.UpdatePassenger(passenger);

                if (result.ID != 0)
                {
                    return Ok(passenger);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (AirplaneException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest("Ocorreu um erro interno no servidor.");
            }
        }

    }
}