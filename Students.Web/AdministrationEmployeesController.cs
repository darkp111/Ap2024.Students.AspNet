using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Students.Common.Data;
using Students.Common.Models;

namespace Students.Web
{
    public class AdministrationEmployeesController : Controller
    {
        private readonly StudentsContext _context;

        public AdministrationEmployeesController(StudentsContext context)
        {
            _context = context;
        }

        // GET: AdministrationEmployees
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdministrationEmployees.ToListAsync());
        }

        // GET: AdministrationEmployees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrationEmployee = await _context.AdministrationEmployees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrationEmployee == null)
            {
                return NotFound();
            }

            return View(administrationEmployee);
        }

        // GET: AdministrationEmployees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdministrationEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Position,Name,Email")] AdministrationEmployee administrationEmployee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(administrationEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(administrationEmployee);
        }

        // GET: AdministrationEmployees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrationEmployee = await _context.AdministrationEmployees.FindAsync(id);
            if (administrationEmployee == null)
            {
                return NotFound();
            }
            return View(administrationEmployee);
        }

        // POST: AdministrationEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Position,Name,Email")] AdministrationEmployee administrationEmployee)
        {
            if (id != administrationEmployee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(administrationEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministrationEmployeeExists(administrationEmployee.Id))
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
            return View(administrationEmployee);
        }

        // GET: AdministrationEmployees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var administrationEmployee = await _context.AdministrationEmployees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrationEmployee == null)
            {
                return NotFound();
            }

            return View(administrationEmployee);
        }

        // POST: AdministrationEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var administrationEmployee = await _context.AdministrationEmployees.FindAsync(id);
            if (administrationEmployee != null)
            {
                _context.AdministrationEmployees.Remove(administrationEmployee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdministrationEmployeeExists(int id)
        {
            return _context.AdministrationEmployees.Any(e => e.Id == id);
        }
    }
}
