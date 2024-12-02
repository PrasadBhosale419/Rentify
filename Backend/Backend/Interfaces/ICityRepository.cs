using Backend.Models;

namespace Backend.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetAllCitiesAsync();

        void AddCity(City city);

        void RemoveCity(int id);

        Task<City> FindCity(int id); 
    }
}
