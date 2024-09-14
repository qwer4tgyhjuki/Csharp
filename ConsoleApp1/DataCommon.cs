using System.Data.Common;
using System.Data.SqlClient;
class DataCommon
{
    static void Main()
    {
        string connectionString = "Data Source=(local);Initial Catalog=GameDatabase;Integrated Security=True";
        using (DbConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT game_id, title FROM Games";

                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int gameId = reader.GetInt32(reader.GetOrdinal("game_id"));
                        string title = reader.GetString(reader.GetOrdinal("title"));

                        Console.WriteLine($"Game ID: {gameId}, Title: {title}");
                    }
                }
            }
        }
    }
}