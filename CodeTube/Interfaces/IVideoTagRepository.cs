using CodeTube.Models;

namespace CodeTube.Interfaces;

public interface IVideoTagRepository
{
    void Create(int VideoId, byte TagId);

    void Delete(int VideoId, byte TagId);

    void Delete(int VideoId);

    List<VideoTag> ReadVideoTag();

    List<Video> ReadVideosByTag(byte TagId);

    List<Tag> ReadTagsByVideo(int VideoId);
}