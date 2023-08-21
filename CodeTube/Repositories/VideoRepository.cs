using System.Data;
using CodeTube.Interfaces;
using CodeTube.Models;
using MySql.Data.MySqlClient;

namespace CodeTube.Repositories;

public class VideoRepository : IVideoRepository
{
    readonly string connectionString = "server=localhost;port=3306;database=CodeTubedb;uid=root;pwd=''";
    readonly IVideoTagRepository _VideoTagRepository;

    public VideoRepository(IVideoTagRepository VideoTagRepository)
    {
        _VideoTagRepository = VideoTagRepository;
    }


    public void Create(Video model)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "insert into Video(Name, Description, UploadDate, Duration, AgeRating, Thumbnail, VideoFile) "
              + "values (@Name, @Description, @UploadDate, @Duration, @AgeRating, @Thumbnail, @VideoFile)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@Name", model.Name);
        command.Parameters.AddWithValue("@Description", model.Description);
        command.Parameters.AddWithValue("@UploadDate", model.UploadDate);
        command.Parameters.AddWithValue("@Duration", model.Duration);
        command.Parameters.AddWithValue("@AgeRating", model.AgeRating);
        command.Parameters.AddWithValue("@Thumbnail", model.Thumbnail);
        command.Parameters.AddWithValue("@VideoFile", model.VideoFile);

        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int? id)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "delete from Video where Id = @Id";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@Id", id);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public List<Video> ReadAll()
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from Video";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        
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

    public Video ReadById(int? id)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from Video where Id = @Id";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@Id", id);
        
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        reader.Read();
        if (reader.HasRows)
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
            connection.Close();
            return Video;
        }
        connection.Close();
        return null;
    }

    public void Update(Video model)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "update Video set "
                        + "Name = @Name, "
                        + "Description = @Description, "
                        + "UploadDate = @UploadDate, "
                        + "Duration = @Duration, "
                        + "AgeRating = @AgeRating, "
                        + "Thumbnail = @Thumbnail "
                        + "VideoFile = @VideoFile "
                    + "where Id = @Id";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@Id", model.Id);
        command.Parameters.AddWithValue("@Name", model.Name);
        command.Parameters.AddWithValue("@Description", model.Description);
        command.Parameters.AddWithValue("@UploadDate", model.UploadDate);
        command.Parameters.AddWithValue("@Duration", model.Duration);
        command.Parameters.AddWithValue("@AgeRating", model.AgeRating);
        command.Parameters.AddWithValue("@Thumbnail", model.Thumbnail);
        command.Parameters.AddWithValue("@VideoFile", model.VideoFile);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public List<Video> ReadAllDetailed()
    {
        List<Video> Videos = ReadAll();
        foreach (Video Video in Videos)
        {
            Video.Tags = _VideoTagRepository.ReadTagsByVideo(Video.Id);
        }
        return Videos;
    }

    public Video ReadByIdDetailed(int id)
    {
        Video Video = ReadById(id);
        Video.Tags = _VideoTagRepository.ReadTagsByVideo(Video.Id);
        return Video;
    }
}