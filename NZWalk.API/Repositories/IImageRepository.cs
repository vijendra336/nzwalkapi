using NZWalk.API.Models.Domain;

namespace NZWalk.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
