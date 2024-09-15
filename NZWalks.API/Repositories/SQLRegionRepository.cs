using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> CreateRegionAsync(Region region)
        {
            await _dbContext.Regions.AddAsync(region);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Region> DeleteRegionAsync(Guid regionId)
        {
            var region = _dbContext.Regions.FirstOrDefault(r => r.Id.Equals(regionId));

            if (region != null)
            {
                _dbContext.Regions.Remove(region);
                await _dbContext.SaveChangesAsync();
            }

            return region;
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            return await _dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionAsync(Guid regionId)
        {
            var region = await _dbContext.Regions.Where(r => r.Id.Equals(regionId)).FirstOrDefaultAsync();

            if (region == null)
            {
                return null;
            }
            else
            {
                return region;
            }
        }

        public async Task<bool> UpdateRegionAsync(Region region)
        {
            //Recuperamos la region de la BD
            var regionDbContext = await _dbContext.Regions.FirstOrDefaultAsync(r => r.Id.Equals(region.Id));

            if (regionDbContext == null)
            {
                return false;
            }

            //Actualizamos los valores:
            regionDbContext.Name = region.Name;
            regionDbContext.Code = region.Code;
            regionDbContext.RegionImageUrl = region.RegionImageUrl;


            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}


