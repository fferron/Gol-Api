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
    public class AirplanesController : ControllerBase
    {
        private readonly IAirplaneRepository _repository;

        public AirplanesController([FromServices]IAirplaneRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("InsertAirplane")]
        public async Task<IActionResult> InsertAirplane([FromBody]Airplane airplane)
        {
            try
            {
                airplane.RegistryCreationDate = DateTime.Now;

                var result = await _repository.InsertAirplane(airplane);

                if (result.ID != 0)
                {
                    return Ok(airplane);
                }
                else {
                    return NotFound();
                }
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

        [HttpGet()]
        [Route("GetAllAirplane")]
        public async Task<IActionResult> GetAllAirplane()
        {
            try
            {
                var result = await _repository.GetAllAirplane();
                return Ok(result);
                //return StatusCode(200, result);
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
        [Route("FindAirplane/{id}")]
        public async Task<IActionResult> FindAirplane([FromRoute]int id)
        {
            try
            {
                var result = await _repository.FindAirplane(id);
                return Ok(result);
            }
            catch (AirplaneException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest("Ocorreu um erro interno no servidor.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var airplane = await _repository.FindAirplane(id);
                await _repository.DeleteAirplane(airplane);

                return NoContent();
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