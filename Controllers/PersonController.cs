using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PaginationDemo.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PaginationDemo.Controllers
{
    public class PersonController : Controller
    {
        private readonly IConfiguration _configuration;
        public PersonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {                                    
            return View();
        }

        public async Task<JsonResult> GetPaginatedPeople(int draw = 1, int start = 0, int length = 10) {
            string sql = $"select FirstName, LastName from Person.Person ORDER BY BusinessEntityID OFFSET {start} ROWS FETCH NEXT {length} ROWS ONLY;";
            string sqlCount = $"select count(1) from Person.Person;";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultDb"));
            var people = await connection.QueryAsync<Person>(sql);
            var peopleCount = await connection.ExecuteScalarAsync<int>(sqlCount);
            var returnData = new
            {
                data = people,
                recordsTotal = peopleCount,
                recordsFiltered = peopleCount
            };
            return Json(returnData);
        }
    }
}
