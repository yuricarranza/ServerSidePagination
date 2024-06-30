using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PaginationDemo.Models;

namespace PaginationDemo.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IConfiguration _configuration;
        public PersonRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<Person>> GetPaginatedPeople(int start, int length)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultDb"));
            string sql = $"select FirstName, LastName from Person.Person ORDER BY BusinessEntityID OFFSET {start} ROWS FETCH NEXT {length} ROWS ONLY;";
            return await connection.QueryAsync<Person>(sql);
        }

        public async Task<int> GetPeopleCount()
        {
            string sqlCount = $"select count(1) from Person.Person;";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultDb"));
            return await connection.ExecuteScalarAsync<int>(sqlCount);
        }
    }
}
