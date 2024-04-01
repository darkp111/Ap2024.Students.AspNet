using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Students.Common.Data;
using Students.Common.Models;
using Students.Interfaces;
using Students.Services;

namespace Students.Web.Controllers;

public class SubjectsController : Controller
{
    private readonly StudentsContext _context;
    private readonly IDatabaseService _databaseService;

    public SubjectsController(StudentsContext context, IDatabaseService databaseService)
    {
        _context = context;
        _databaseService = databaseService;
    }

    // GET: Subjects
    public async Task<IActionResult> Index()
    {
        return View(await _context.Subject.ToListAsync());
    }

    // GET: Subjects/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        var subject = await _databaseService.SubjectDetails(id);
        return View(subject);
    }

    // GET: Subjects/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Subjects/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Credits")] Subject subject)
    {
        if (ModelState.IsValid)
        {
            var result = await _databaseService.CreateSubject(subject);
            return RedirectToAction(nameof(Index));
        }
        return View(subject);
    }

    // GET: Subjects/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subject = await _databaseService.EditSubject(id);
        return View(subject);
    }

    // POST: Subjects/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Credits")] Subject subject)
    {
        if (ModelState.IsValid)
        {
            await _databaseService.EditSubject(id, subject);
            return RedirectToAction(nameof(Index));
        }
        return View(subject);
    }

    // GET: Subjects/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var subject = await _databaseService.DeleteSubject(id);

        return View(subject);
    }

    // POST: Subjects/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var subject = await _databaseService.SubjectDeleteConfirmed(id);
        return RedirectToAction(nameof(Index));
    }

    private bool SubjectExists(int id)
    {
        var result = _databaseService.CheckSubjectExist(id);
        return result;
    }
}
