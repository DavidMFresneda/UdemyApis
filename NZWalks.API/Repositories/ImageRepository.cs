using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly NZWalksDbContext _nZWalks;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string _localPath = "C:\\Users\\josep\\source\\repos\\NZWalks\\NZWalks.API\\wwwroot\\images";

        public LocalImageRepository(NZWalksDbContext nZWalks,
                                    IWebHostEnvironment webHostEnvironment,
                                    IHttpContextAccessor httpContextAccessor)
        {
            this._nZWalks = nZWalks;
            this._webHostEnvironment = webHostEnvironment;
            this._httpContextAccessor = httpContextAccessor;
            this._localPath = Path.Combine(_webHostEnvironment.ContentRootPath, "images");
        }

        async public Task<Image> Upload(Image image)
        {

            var fileNamePath = Path.Combine(_localPath, Path.GetFileName(image.FileName)) + image.FileExtension;

            //Upload image to local path
            using var stream = new FileStream(fileNamePath, FileMode.OpenOrCreate);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            //Saving changes to the database
            await _nZWalks.Images.AddAsync(image);
            await _nZWalks.SaveChangesAsync();

            return image;
        }
    }
}
