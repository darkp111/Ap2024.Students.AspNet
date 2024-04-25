using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Students.Common.Data;
using Students.Common.Models;
using Students.Interfaces;
using Students.Services;

namespace Students.Web.Controllers;

public class StudentsController : Controller
{
    #region Ctor And Properties

    private readonly StudentsContext _context;
    private readonly ILogger _logger;
    private readonly ISharedResourcesService _sharedResourcesService;
    private readonly IDatabaseService _databaseService;

    public StudentsController(
        StudentsContext context, 
        ILogger<StudentsController> logger, 
        ISharedResourcesService sharedResourcesService,
        IDatabaseService databaseService)
    {
        _context = context;
        _logger = logger;
        _sharedResourcesService = sharedResourcesService;
        _databaseService = databaseService;
    }

    #endregion // Ctor And Properties

    #region Public Methods

    // GET: Students
    public async Task<IActionResult> Index()
    {
        IActionResult result = View();
        try
        {
            var model = await _databaseService.Index();
            result = View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }

        return result;
    }

    // GET: Students/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        IActionResult result = NotFound();

        try
        {
            if (id != null)
            {
                var student = await _databaseService.Details(id);
                if (student != null)
                {
                    result = View(student);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }
        return result;
    }

    // GET: Students/Create
    public IActionResult Create()
    {
        IActionResult result = View();
        try
        {
            var newStudent = _databaseService.Create();

             result = View(newStudent);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }

        return result;
    }

    // POST: Students/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Age,Major,PostalCode")] Student student, int[] subjectIdDst)
    {
        if (ModelState.IsValid)
        {
            var newStudent = await _databaseService.Create(student, subjectIdDst);
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    // GET: Students/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        IActionResult result = NotFound();

        try
        {
           var student = await _databaseService.EditStudent(id);
            result = View(student);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }
        return result;
    }

    // POST: Students/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([Bind("Id,Name,Age,Major,PostalCode")] Student student, int[] subjectIdDst)
    {
        if (ModelState.IsValid)
        {
            student = await _databaseService.EditStudent(student, subjectIdDst);
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }


    // GET: Students/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        IActionResult result = View();
        try
        {
            if (id == null)
            {
                result = NotFound();
            }
            else
            {

                var student = await _databaseService.DisplayStudent(id);
                if (student == null)
                {
                    result = NotFound();
                }
                else
                {
                    result = View(student);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception caught: " + ex.Message);
        }

        return result;
    }

    // POST: Students/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        IActionResult result = View();
        try
        {
            var student = await _databaseService.StudentDeleteConfirmedProc(id);
            result = RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception caught: " + ex.Message);
        }

        return result;
    }

    #endregion // Public Methods

    #region Private Methods

    private bool StudentExists(int id)
    {
        var result = _databaseService.CheckStudentExist(id);
        return result;
    }

    #endregion // Private Methods
}
