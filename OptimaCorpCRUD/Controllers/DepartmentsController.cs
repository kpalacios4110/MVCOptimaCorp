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
    public class DepartmentsController : Controller
    {
        private readonly OptimaCorpBdContext _context;
        public DepartmentsController(OptimaCorpBdContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Department> lista = _context.Departments.ToList();
            return View(lista);
        }
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create([Bind("Id,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var department = _context.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,Name")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department =  _context.Departments
                .FirstOrDefault(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var department =  _context.Departments.Find(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }
    }
}
