using Backend.Interfaces;
using Backend.Repo;
using WebAPI.Data.Repo;
using WebAPI.Interfaces;

namespace Backend.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext db;

        public UnitOfWork(DataContext Db)
        {
            db = Db;
        }
        public ICityRepository cityRepository => new CityRepository(db);

        public IUserRepository userRepository => new UserRepository(db);

        public IPropertyRepository propertyRepository => new PropertyRepository(db);

        public IPropertyTypeRepository propertyTypeRepository => new PropertyTypeRepository(db);

        public IFurnishingTypeRepository furnishingTypeRepository => new FurnishingTypeRepository(db);

        public async Task<bool> SaveAsync()
        {
            return await db.SaveChangesAsync()>0;
        }
    }
}
