using Gol.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gol.Domains.Repository
{
    public interface IAirplaneRepository
    {
        Task<List<Airplane>> GetAllAirplane();
        Task<Airplane> FindAirplane(int id);
        Task<Airplane> InsertAirplane(Airplane request);
        Task DeleteAirplane(Airplane request);
        Task<Airplane> UpdateAirplane(Airplane request);
    }
}
