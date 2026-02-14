using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

string connStr = config.GetConnectionString("DefaultConnection");

using var conn = new SqlConnection(connStr);
conn.Open();

Console.WriteLine("Connected! Showing rows:");


var cmd = new SqlCommand("SELECT * FROM dbo.YKumarTable", conn);
using var reader = cmd.ExecuteReader();

while (reader.Read())
{
    Console.WriteLine($"{reader["UserID"]}, {reader["UserName"]}, {reader["UserEmail"]}");
}

reader.Close();  


Console.WriteLine("\nInserting new user...");

SqlCommand insertCommand = new SqlCommand(
"INSERT INTO YKumarTable (UserId,UserName,UserEmail) VALUES (@0,@1,@2)", conn);

insertCommand.Parameters.Add(new SqlParameter("0", 3));
insertCommand.Parameters.Add(new SqlParameter("1", "NewUser"));
insertCommand.Parameters.Add(new SqlParameter("2", "newuser@kean.edu"));

insertCommand.ExecuteNonQuery();

Console.WriteLine("Insert successful!");

