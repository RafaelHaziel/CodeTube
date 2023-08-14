using CodeTube.Models;

namespace CodeTube.Interfaces;

public interface IVideoRepository : IRepository<Video>
{
    List<Video> ReadAllDetailed();

    Video ReadByIdDetailed(int id);
}