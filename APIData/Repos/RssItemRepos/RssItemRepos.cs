using System;
using System.Threading.Tasks;
using APIData.Entities;
using APITypes;
using Microsoft.Data.Sqlite;

namespace APIData.Repos.RssItemRepos
{
    public class RssItemRepos : IRssItemRepos
    {
        private readonly string _connectionString;

        public RssItemRepos(string connectionString)
        {
            _connectionString = connectionString;
        }
        private const string SelectById = "SELECT \"id\", \"source\", \"title\", \"link\", \"date\" " +
                                                  "FROM \"rss_item\" " +
                                                  "WHERE \"id\" = @id";


        public async Task<RssItem> Get(long id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = SelectById;
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync()) return null;
                        return new RssItem
                        {
                            Id = reader.GetInt64(0),
                            Source = (RssSource)reader.GetInt32(1),
                            Title = reader.GetString(2),
                            Link = reader.GetString(3),
                            Date = Convert.ToDateTime(reader.GetString(4))
                        };
                    }
                }
            }
        }
    }
}