using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {

        Task<List<Region>> GetAllRegionsAsync();
        Task<Region?> GetRegionAsync(Guid regionId);

        Task<bool> CreateRegionAsync(Region region);
        Task<bool> UpdateRegionAsync(Region region);
        Task<Region> DeleteRegionAsync(Guid regionId);


    }
}
