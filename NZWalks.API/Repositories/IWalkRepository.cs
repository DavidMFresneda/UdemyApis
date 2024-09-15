using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {

        Task<List<Walk>> GetAllWalksAsync();
        Task<List<Walk>> GetAllWalksAsync(string? campo, string? valor, int pageNumber = 1, int pageSize = 4);
        Task<Walk?> GetWalkAsync(Guid walkId);

        Task<bool> CreateWalkAsync(Walk walk);
        Task<Walk?> DeleteWalkAsync(Guid walkId);
        Task<Walk?> UpdateWalkAsync(Guid id, Walk walkUpdateData);
    }
}
