 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OptimaCorpCRUD.Models;

namespace OptimaCorpCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly OptimaCorpBdContext _context;

        public EmployeesController(OptimaCorpBdContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            List<Employee> lista = _context.Employees.ToList();
            return View(lista);
        }
        public IActionResult Create()
        {
            ViewData["IdDeparmentFk"] = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Salary,IdDeparmentFk")] Employee employee)
        {
                _context.Add(employee);
                await _context.SaveChangesAsync();
            
            TempData["Message"] = "Registro guardado correctamente.";
            ViewData["IdDeparmentFk"] = new SelectList(_context.Departments, "Id", "Name", employee.IdDeparmentFk);
              return View(employee);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["IdDeparmentFk"] = new SelectList(_context.Departments, "Id", "Name", employee.IdDeparmentFk);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Salary,IdDeparmentFk")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();

                TempData["Message"] = "Registro Editado correctamente.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               
            
            ViewData["IdDeparmentFk"] = new SelectList(_context.Departments, "Id", "Name", employee.IdDeparmentFk);
            return View(employee);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employee = _context.Employees
                .Include(e => e.IdDeparmentFkNavigation)
                .FirstOrDefault(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
