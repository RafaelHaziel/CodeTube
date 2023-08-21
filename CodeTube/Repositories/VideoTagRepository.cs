using System.Data;
using CodeTube.Interfaces;
using CodeTube.Models;
using MySql.Data.MySqlClient;

namespace CodeTube.Repositories;

public class VideoTagRepository : IVideoTagRepository
{
    readonly string connectionString = "server=localhost;port=3306;database=CodeTubedb;uid=root;pwd=''";

    public void Create(int VideoId, byte TagId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "insert into Video[Tag(VideoId, TagId) values (@VideoId, @TagId)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", VideoId);
        command.Parameters.AddWithValue("@TagId", TagId);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int VideoId, byte TagId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "delete from VideoTag where VideoId = @VideoId and TagId = @TagId";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", VideoId);
        command.Parameters.AddWithValue("@TagId", TagId);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int VideoId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "delete from VideoTag where VideoId = @VideoId";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", VideoId);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public List<Tag> ReadTagsByVideo(int VideoId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from Tag where id in "
                   + "(select TagId from VideoTag where VideoId = @VideoId)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@VideoId", VideoId);
        
        List<Tag> Tags = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Tag Tag = new()
            {
                Id = reader.GetByte("id"),
                Name = reader.GetString("name")
            };
            Tags.Add(Tag);
        }
        connection.Close();
        return Tags;
    }

    public List<VideoTag> ReadVideoTag()
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from VideoTag";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        
        List<VideoTag> VideoTags = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            VideoTag VideoTag = new()
            {
                VideoId = reader.GetInt32("VideoId"),
                TagId = reader.GetByte("TagId")
            };
            VideoTags.Add(VideoTag);
        }
        connection.Close();
        return VideoTags;
    }

    public List<Video> ReadVideosByTag(byte TagId)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from Video where id in "
                   + "(select VideoId from VideoTag where TagId = @TagId)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@TagId", TagId);
        
        List<Video> Videos = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Video Video = new()
            {
                Id = reader.GetInt32("id"),
                Name = reader.GetString("name"),
                Description = reader.GetString("description"),
                UploadDate = reader.GetDateTime("uploaddate"),
                Duration = reader.GetInt16("duration"),
                AgeRating = reader.GetByte("ageRating"),
                Thumbnail= reader.GetString("thumbnail"),
                VideoFile= reader.GetString("videofile")
            };
            Videos.Add(Video);
        }
        connection.Close();
        return Videos;
    }
}