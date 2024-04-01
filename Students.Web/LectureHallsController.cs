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
    public class LectureHallsController : Controller
    {
        private readonly StudentsContext _context;

        public LectureHallsController(StudentsContext context)
        {
            _context = context;
        }

        // GET: LectureHalls
        public async Task<IActionResult> Index()
        {
            return View(await _context.LectureHall.ToListAsync());
        }

        // GET: LectureHalls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lectureHall = await _context.LectureHall
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lectureHall == null)
            {
                return NotFound();
            }

            return View(lectureHall);
        }

        // GET: LectureHalls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LectureHalls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlacesCount,Number,Floor")] LectureHall lectureHall)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lectureHall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lectureHall);
        }

        // GET: LectureHalls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lectureHall = await _context.LectureHall.FindAsync(id);
            if (lectureHall == null)
            {
                return NotFound();
            }
            return View(lectureHall);
        }

        // POST: LectureHalls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlacesCount,Number,Floor")] LectureHall lectureHall)
        {
            if (id != lectureHall.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lectureHall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LectureHallExists(lectureHall.Id))
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
            return View(lectureHall);
        }

        // GET: LectureHalls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lectureHall = await _context.LectureHall
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lectureHall == null)
            {
                return NotFound();
            }

            return View(lectureHall);
        }

        // POST: LectureHalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lectureHall = await _context.LectureHall.FindAsync(id);
            if (lectureHall != null)
            {
                _context.LectureHall.Remove(lectureHall);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LectureHallExists(int id)
        {
            return _context.LectureHall.Any(e => e.Id == id);
        }
    }
}
