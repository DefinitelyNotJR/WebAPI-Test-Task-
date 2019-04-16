using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIData.Entities;
using APITypes;
using Microsoft.Data.Sqlite;

namespace APIData.Repos.RssItemRepos
{
    public class RssItemRepos : IRssItemRepos
    {
        
        private const string SelectById = "SELECT \"id\", \"source\", \"title\", \"link\", \"date\" " +
                                                  "FROM \"rss_item\" " +
                                                  "WHERE \"id\" = @id";

        private const string SelectAll = "SELECT \"id\", \"source\", \"title\", \"link\", \"date\" " +
                                              "FROM \"rss_item\" " +
                                              "ORDER BY \"id\"";
        private string v;

        public RssItemRepos(string v)
        {
            this.v = v;
        }

        public async Task<RssItem> Get(long id)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = v;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
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

        public async Task<RssItem[]> GetAll()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = v;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    var result = new List<RssItem>();
                    command.CommandText = SelectAll;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                result.Add(new RssItem
                                {
                                    Id = reader.GetInt64(0),
                                    Source = (RssSource)reader.GetInt32(1),
                                    Title = reader.GetString(2),
                                    Link = reader.GetString(3),
                                    Date = Convert.ToDateTime(reader.GetString(4))
                                });
                            }
                        }
                        return result.ToArray();
                    }
                }
            }
        }
    }
}