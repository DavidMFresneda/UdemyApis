using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _dbContext;

        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> CreateWalkAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Walk?> DeleteWalkAsync(Guid walkId)
        {
            var walk = await _dbContext.Walks.FindAsync(walkId);

            if (walk == null)
                return null;

            _dbContext.Walks.Remove(walk);
            return walk;
        }

        public async Task<List<Walk>> GetAllWalksAsync()
        {
            var walkList = await _dbContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .ToListAsync();
            return walkList;
        }

        public async Task<List<Walk>> GetAllWalksAsync(string? campo, string? valor, int pageNumber = 1, int pageSize = 4)
        {

            if (string.IsNullOrEmpty(campo) || string.IsNullOrEmpty(valor))
            {
                if (pageNumber > 0 && pageSize > 0)
                {
                    return await _dbContext.Walks
                   .Include("Difficulty")
                   .Include("Region")
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize)
                   .ToListAsync();
                }

                return await _dbContext.Walks
                   .Include("Difficulty")
                   .Include("Region")
                   .ToListAsync();
            }
            else
            {
                var walks = _dbContext.Walks
                   .Include("Difficulty")
                   .Include("Region").AsQueryable();

                if (campo.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(w => w.Name.Contains(valor));
                }

                return await walks.ToListAsync();
            }

        }

        public async Task<Walk?> GetWalkAsync(Guid walkId)
        {
            var walk = await _dbContext.Walks
                .Include("Difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(w => w.Id == walkId);
            return walk;
        }

        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk walkUpdateData)
        {
            var walk = await _dbContext.Walks.FindAsync(id);

            if (walk == null)
                return walk;


            walk.Name = walkUpdateData.Name;
            walk.Description = walkUpdateData.Description;
            walk.LengthInKm = walkUpdateData.LengthInKm;
            walk.WalkImageUrl = walkUpdateData.WalkImageUrl;
            walk.DifficultyId = walkUpdateData.DifficultyId;
            walk.RegionId = walkUpdateData.RegionId;

            await _dbContext.SaveChangesAsync();
            return walk;
        }



    }
}
