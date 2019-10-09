using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;

namespace WebAPI3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoreController : ControllerBase
    {

        private readonly ILogger<CoreController> _logger;
        private IConfiguration _configuration;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public CoreController(ILogger<CoreController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IEnumerable<Weather> Get()
        {
            var rng = new Random();
            _logger.LogDebug("Core is called");
            return Enumerable.Range(1, 10).Select(index => new Weather
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("Book")]
        public async Task<IEnumerable<Book>> GetLibraryBooks()
        {
            
            string conn = _configuration.GetSection("ConnectionStrings").GetSection("library").Value;
            var sqlConnection = new SqlConnection(conn);
            sqlConnection.Open();
            string sql = "select * from books";
            SqlCommand command = new SqlCommand(sql, sqlConnection);
            var reader = command.ExecuteReader();
            var columns = new List<string>();
            var results = new List<Book>();

            for ( var i = 0; i <reader.FieldCount; i++)
            {
                columns.Add(reader.GetName(i));
            }

            while (reader.Read())
            {
                results.Add(new Book
                {   Id = (int)reader["Id"], 
                    Title = reader["Title"].ToString(), 
                    Author = reader["Title"].ToString() 
                });
            }

            return results;

            //var results = ConvertToDictionary(reader);
            //return JsonConvert.SerializeObject(results);
       
        }

        [HttpGet("Table/{tablename}")]
        public async Task<IEnumerable<Dictionary<string, object>>> GetTable(string tablename)
        {

            string conn = _configuration.GetSection("ConnectionStrings").GetSection("library").Value;
            var sqlConnection = new SqlConnection(conn);
            sqlConnection.Open();
            string sql = "select * from " + tablename;
            SqlCommand command = new SqlCommand(sql, sqlConnection);
            var reader = command.ExecuteReader();
            _logger.LogInformation("Table " + tablename + " is retrieved.");
            Log.Information("Table " + tablename + " is retrieved.");
            return ConvertToDictionary(reader);

            //return JsonConvert.SerializeObject(results);

        }

        private IEnumerable<Dictionary<string, object>> ConvertToDictionary(SqlDataReader reader)
        {
            var columns = new List<string>();
            var rows = new List<Dictionary<string, object>>();

            for (var i = 0; i < reader.FieldCount; i++)
            {
                columns.Add(reader.GetName(i));
            }

            while (reader.Read())
            {
                rows.Add(columns.ToDictionary(column => column, column => reader[column]));
            }

            return rows;
        }


    }
}
