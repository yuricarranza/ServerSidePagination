using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PaginationDemo.Models;
using PaginationDemo.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PaginationDemo.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;
        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public IActionResult Index()
        {                                    
            return View();
        }

        public async Task<JsonResult> GetPaginatedPeople(int draw = 1, int start = 0, int length = 10) 
        {
            var peopleQuantity = await _personRepository.GetPeopleCount();
            var returnData = new
            {
                data = await _personRepository.GetPaginatedPeople(start, length),
                recordsTotal = peopleQuantity,
                recordsFiltered = peopleQuantity
            };
            return Json(returnData);
        }
    }
}
