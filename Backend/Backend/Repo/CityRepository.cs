using Backend.Data;
using Backend.Interfaces;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repo
{
    public class CityRepository : ICityRepository
    {
        public CityRepository(DataContext Db)
        {
            this.Db = Db;
        }

        public DataContext Db { get; }

        public void AddCity(City city)
        {
            Db.Cities.AddAsync(city);
        }

        public async Task<City> FindCity(int id)
        {
            return await Db.Cities.FindAsync(id);
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
            return await Db.Cities.ToListAsync();
        }

        public void RemoveCity(int id)
        {
            City city = Db.Cities.Find(id);
            if (city != null)
            {
                Db.Cities.Remove(city);
            }
        }

    }
}
