using Gol.Domains.Repository;
using Gol.Entities;
using Gol.Entities.Exceptions;
using Gol.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gol.Domains
{
    public class PassengersDomain : IPassengerRepository
    {
        private GolContext _context;
        private readonly IConfiguration _configuration;

        public PassengersDomain(IConfiguration configuration)
        {
            _configuration = configuration;

            using (_context = new GolContext(_configuration))
            {
                _context.Database.Migrate();
            }
        }


        /// <summary>
        /// Saves a new entity to database.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <exception cref="PassengerException">Throws when something goes worng. The InnerException can be used to have more details.</exception>
        /// <returns></returns>
        public async Task<Passenger> InsertPassenger(Passenger entity)
        {
            try
            {
                var Passenger = default(Passenger);
                using (_context = new GolContext(_configuration))
                {
                    var entry = await _context.AddAsync(entity);
                    var rowsAffected = await _context.SaveChangesAsync();

                    if (rowsAffected > 0)
                    {
                        Passenger = entry.Entity;
                    }
                }

                if (Passenger == null)
                {
                    throw new PassengerException("O passageiro não foi salvo na base de dados.");
                }

                return Passenger;
            }
            catch (PassengerException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new PassengerException("Não foi possível salvar o passageiro informado.", e);
            }
        }

        /// <summary>
        /// Saves a new entity to database.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <exception cref="PassengerException">Throws when something goes worng. The InnerException can be used to have more details.</exception>
        /// <returns></returns>
        public async Task<Passenger> InsertPassengerToAirplane(Passenger entity)
        {
            try
            {
                var Airplane = default(Airplane);
                var Passenger = default(Passenger);

                using (_context = new GolContext(_configuration))
                {
                    Airplane = await _context.Airplanes.FirstOrDefaultAsync(a => a.ID.Equals(entity.AirplaneID));

                    if (Airplane == null)
                    {
                        throw new PassengerException("O avião não existe na base de dados.");
                    }

                    Passenger = await _context.Passengers.FirstOrDefaultAsync(a => a.ID.Equals(entity.ID));

                    var entry = _context.Passengers.Update(Passenger);
                    var rowsAffected = await _context.SaveChangesAsync();

                    if (rowsAffected > 0)
                    {
                        Passenger = entry.Entity;
                    }
                }

                if (Passenger == null)
                {
                    throw new PassengerException("O passageiro não foi salvo na base de dados.");
                }

                return Passenger;
            }
            catch (PassengerException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new PassengerException("Não foi possível salvar o passageiro informado.", e);
            }
        }


        /// <summary>
        /// Saves a new entity to database.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <exception cref="PassengerException">Throws when something goes worng. The InnerException can be used to have more details.</exception>
        /// <returns></returns>
        public async Task<Passenger> ChangePassenger(int id, int idAirplane)
        {
            try
            {
                var Airplane = default(Airplane);
                var Passenger = default(Passenger);

                using (_context = new GolContext(_configuration))
                {
                    Passenger = await _context.Passengers.FindAsync(id);

                    if (Passenger != null)
                    {
                        Airplane = await _context.Airplanes.FindAsync(idAirplane);

                        if (Airplane != null)
                        {
                            Passenger.AirplaneID = idAirplane;

                            var entry = _context.Passengers.Update(Passenger);
                            var rowsAffected = await _context.SaveChangesAsync();

                            if (rowsAffected > 0)
                            {
                                Passenger = entry.Entity;
                            }
                        }
                        else
                        {
                            throw new PassengerException("O avião não existe na base de dados.");
                        }
                    }
                    else
                    {
                        throw new PassengerException("O passageiro não existe na base de dados.");
                    }
                }

                if (Passenger == null)
                {
                    throw new PassengerException("O passageiro não foi atualizado na base de dados.");
                }

                return Passenger;
            }
            catch (PassengerException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new PassengerException("Não foi possível salvar o passageiro informado.", e);
            }
        }


        /// <summary>
        /// Gets a specific entity based on its unique identifier.
        /// </summary>
        /// <exception cref="PassengerException">Throws when something goes worng. The InnerException can be used to have more details.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<Passenger>> ListAllPassengerByAirplane(int idAirplane)
        {
            try
            {
                IEnumerable<Passenger> Passengers = default(IEnumerable<Passenger>);

                using (_context = new GolContext(_configuration))
                {
                    Passengers = await _context.Passengers.Include("Airplane").Where(a => a.AirplaneID == idAirplane).ToListAsync();
                }

                return Passengers;
            }
            catch (Exception e)
            {
                throw new PassengerException("Não foi possível buscar os passageiros deste avião solicitado.", e);
            }
        }

        /// <summary>
        /// Gets all entities available to database.
        /// </summary>
        /// <exception cref="PassengerException">Throws when something goes worng. The InnerException can be used to have more details.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<Passenger>> GetAllPassenger()
        {
            try
            {
                var Passengers = default(IEnumerable<Passenger>);
                using (_context = new GolContext(_configuration))
                {
                    Passengers = await _context.Passengers.ToListAsync();
                }

                return Passengers;
            }
            catch (Exception e)
            {
                throw new PassengerException("Não foi possível buscar os passageiros disponíveis.", e);
            }
        }

        /// <summary>
        /// Saves a new entity to database.
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <exception cref="PassengerException">Throws when something goes worng. The InnerException can be used to have more details.</exception>
        /// <returns></returns>
        public async Task<Passenger> UpdatePassenger(Passenger entity)
        {
            try
            {
                var passenger = default(Passenger);
                using (_context = new GolContext(_configuration))
                {
                    var entry = _context.Update(entity);
                    var rowsAffected = await _context.SaveChangesAsync();

                    if (rowsAffected > 0)
                    {
                        passenger = entry.Entity;
                    }
                }

                if (passenger == null)
                {
                    throw new PassengerException("O passageiro não foi atualizado na base de dados.");
                }

                return passenger;
            }
            catch (PassengerException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new PassengerException("Não foi possível atualizar o passageiro informado.", e);
            }
        }

    }
}
