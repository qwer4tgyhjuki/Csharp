using System.Data.Common;
using System.Data.SqlClient;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using HtmlAgilityPack;
class Program
{
    static void ThreadFun1(List<int> numbers, string threadName)
    {
        foreach (int number in numbers)
        {
            if(number < 10)
            {
                Console.WriteLine($"{threadName}: {number}");
            }
        }
    }
    static void ThreadFun2()
    {
        string connectionString = "Data Source=(local);Initial Catalog=GameDatabase;Integrated Security=True";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT g.title, dc.name " +
                                      "FROM Games g " +
                                      "JOIN DevelopersCompanies dc ON g.developer_company_id = dc.developer_company_id where dc.name = 'Valve Corporation'";
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string gameTitle = reader.GetString(0);
                        string companyName = reader.GetString(1);
                        Console.WriteLine($"{companyName}: {gameTitle}");
                    }
                }
            }
        }
    }
    static void ThreadFun3()
    {
        string connectionString = "Data Source=(local);Initial Catalog=GameDatabase;Integrated Security=True";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT title, release_year, rating_id FROM Games g ";
                                    
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    using (StreamWriter file = new StreamWriter("data.json"))
                    {
                        while (reader.Read())
                        {
                            string gameTitle = reader.GetString(0);
                            DateTime releaseYear = reader.GetDateTime(1);
                            int ratingId = reader.GetInt32(2);

                            string jsonData = $"{{\"title\": \"{gameTitle}\", \"release_year\": \"{releaseYear}\", \"rating_id\": {ratingId}}},";

                            file.WriteLine(jsonData);
                        }
                        Console.WriteLine("Succes wrote the file");
                    }
                }
            }
        }
    }


    static async Task AsyncFun1()
    {
        try
        {
            using(HttpClient client = new HttpClient())
            {
                string url = "https://google.com";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(responseBody);

                HtmlNode companyNameNode = htmlDocument.DocumentNode.SelectSingleNode("//title");
                string companyName = companyNameNode.InnerText;
                await Console.Out.WriteLineAsync(companyName);

            }
        } 
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
        }
    }

    static async Task AsyncFun2()
    {
        string dataJson = "data.json";
        try
        {
            using(StreamReader reader = File.OpenText(dataJson))
            {
                string line;
                int lineCount = 0;

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    lineCount++;
                }

                Console.WriteLine($"Line count: {lineCount}");
            }
        }
        catch(Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
        }
    }

    static async Task<int> AsyncFun3(int a, int b)
    {
        await Task.Delay(1000);

        return a + b;
    }

    static void Main()
    {
        List<int> numbers = new List<int> { 555, 511, 8, 3, 152, 120, 2576 };
        Thread thread1 = new Thread(() => ThreadFun1(numbers, "Thread 1"));
        Thread thread2 = new Thread(new ThreadStart(ThreadFun2));
        Thread thread3 = new Thread(new ThreadStart(ThreadFun3));

        
        thread2.Start();
        thread1.Start();
        thread3.Start();

        thread1.Join();
        thread2.Join();
        thread3.Join();

        AsyncFun1().GetAwaiter().GetResult();
        AsyncFun2().GetAwaiter().GetResult();
        Console.WriteLine(AsyncFun3(2, 56).GetAwaiter().GetResult());


    }
}