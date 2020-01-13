using Gol.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gol.Domains.Repository
{
    public interface IPassengerRepository
    {
        Task<Passenger> InsertPassenger(Passenger request);
        Task<Passenger> InsertPassengerToAirplane(Passenger request);
        Task<Passenger> ChangePassenger(int id, int IdAirplane);
        Task<IEnumerable<Passenger>> ListAllPassengerByAirplane(int idAirplane);
        Task<IEnumerable<Passenger>> GetAllPassenger();
    }
}
