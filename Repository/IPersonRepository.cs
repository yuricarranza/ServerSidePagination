using PaginationDemo.Models;

namespace PaginationDemo.Repository
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetPaginatedPeople(int start, int length);
        Task<int> GetPeopleCount();
    }
}
