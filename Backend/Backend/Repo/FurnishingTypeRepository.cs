using Backend.Data;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace Backend.Repo
{
    public class FurnishingTypeRepository : IFurnishingTypeRepository
    {
        private readonly DataContext db;

        public FurnishingTypeRepository(DataContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<FurnishingType>> GetFurnishingTypesAsync()
        {
            return await db.FurnishingTypes.ToListAsync();
        }
    }
}
