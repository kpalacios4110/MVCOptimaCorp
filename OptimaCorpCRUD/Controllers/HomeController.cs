using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimaCorpCRUD.Models;

namespace OptimaCorpCRUD.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly OptimaCorpBdContext _context;
        public HomeController(OptimaCorpBdContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string search)
        {
         
            IQueryable<Employee> employees = _context.Employees.Include(e => e.IdDeparmentFkNavigation);

            if (!string.IsNullOrEmpty(search))
            {
                employees = employees.Where(e => e.Name.Contains(search));
            }
            return View(await employees.ToListAsync());
        }

    }
}
