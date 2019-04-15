using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIData.Entities;
using Microsoft.Data.Sqlite;

namespace Parser.Services.DataService
{
    public class DbDataService : IDataService
    {
        private const string ConnectionString = "C:/Code/1/WebAPI-Test-Task-/WebApiDB.db";
        private const string DeleteQuery = "DELETE FROM \"rss_item\"";
        private const string UpdateQuery = "INSERT INTO \"rss_item\"(\"source\", " +
                                      "\"title\", \"link\", \"date\") " +
                                      "VALUES (@source, @title, @link, @date)";
        public async Task ClearDataAsync()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = ConnectionString;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                await connection.OpenAsync();

                var delTable = connection.CreateCommand();
                delTable.CommandText = DeleteQuery;
                await delTable.ExecuteNonQueryAsync();
            }
            Console.WriteLine("The data has been deleted.");
        }

        public async Task SaveRssItemsAsync(IEnumerable<RssItem> rssItems)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = ConnectionString;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                await connection.OpenAsync();
                foreach (var rssItem in rssItems)
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = UpdateQuery;
                        command.Parameters.Add(new SqliteParameter("@source", rssItem.Source));
                        command.Parameters.Add(new SqliteParameter("@title", rssItem.Title));
                        command.Parameters.Add(new SqliteParameter("@link", rssItem.Link));
                        command.Parameters.Add(new SqliteParameter("@date", rssItem.Date));

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }
    }
}