using System.Data;
using CodeTube.Interfaces;
using CodeTube.Models;
using MySql.Data.MySqlClient;

namespace CodeTube.Repositories;

public class TagRepository : ITagRepository
{
    readonly string connectionString = "server=localhost;port=3306;database=CodeTubedb;uid=root;pwd=''";

    public void Create(Tag model)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "insert into Tag(Name) values (@Name)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@Name", model.Name);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int? id)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "delete from Tag where Id = @Id";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@Id", id);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public List<Tag> ReadAll()
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from Tag";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        
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

    public Tag ReadById(int? id)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from Tag where Id = @Id";
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
            Tag Tag = new()
            {
                Id = reader.GetByte("id"),
                Name = reader.GetString("name")
            };
            connection.Close();
            return Tag;
        }
        connection.Close();
        return null;
    }

    public void Update(Tag model)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "update Tag set Name = @Name where Id = @Id";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@Id", model.Id);
        command.Parameters.AddWithValue("@Name", model.Name);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }
}