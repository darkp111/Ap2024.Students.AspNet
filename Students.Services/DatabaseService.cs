using Microsoft.Extensions.Logging;
using Students.Common.Data;
using Students.Common.Models;
using Students.Interfaces;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Students.Services;

public class DatabaseService : IDatabaseService
{
    #region Ctor and Properties

    private readonly StudentsContext _context;
    private readonly ILogger<DatabaseService> _logger;

    public DatabaseService(
        ILogger<DatabaseService> logger,
        StudentsContext context)
    {
        _logger = logger;
        _context = context;
    }

    #endregion // Ctor and Properties

    #region Public Methods

    public bool CheckStudentExist(int id)
    {
        var result = _context.Student.Any(e => e.Id == id);
        return result;
    }

    public async Task<Student> Details(int? id)
    {
        var student = await _context.Student
                    .FirstOrDefaultAsync(m => m.Id == id);

        var studentSubjects = _context.StudentSubject
            .Where(ss => ss.StudentId == id)
            .Include(ss => ss.Subject)
            .ToList();
        if (student is not null)
            student.StudentSubjects = studentSubjects;

        if (student != null)
        {
            return student;
        }
        else
            throw new Exception("Student detailes find failed");
    }

    public async Task<Subject> SubjectDetails(int? id)
    {
        if (id == null)
        {
            throw new Exception("Error");
        }

        var subject = await _context.Subject
            .FirstOrDefaultAsync(m => m.Id == id);
        if (subject == null)
        {
            throw new Exception("Error");
        }
        return subject;
    }

    public async Task<bool> CreateSubject(Subject subject)
    {
        var result = false;
        _context.Add(subject);
        var CheckResult = await _context.SaveChangesAsync();
        result = CheckResult > 0;
        return result;
    }

    public async Task<Subject> EditSubject(int? id)
    {
        var subject = await _context.Subject.FindAsync(id);

        if (subject != null)
            return subject;
        else
            throw new Exception("Error");
    }

    public async Task<Subject> DeleteSubject(int? id)
    {
        var subject = await _context.Subject
            .FirstOrDefaultAsync(m => m.Id == id);
        if (subject != null)
            return subject;
        else
            throw new Exception("Error");
    }

    public async Task<bool> SubjectDeleteConfirmed(int id)
    {
        var result = false;
        var subject = await _context.Subject.FindAsync(id);
        if (subject != null)
        {
            _context.Subject.Remove(subject);
        }

        var checkresult = await _context.SaveChangesAsync();
        result = checkresult > 0;
        return result;
    }

    public bool CheckSubjectExist(int id)
    {
        var result = _context.Subject.Any(e => e.Id == id);
        return result;
    }


    public async Task<Subject> EditSubject(int id, Subject subject)
    {
        if (id != subject.Id)
        {
            throw new Exception("No such id found");
        }

        try
        {
            _context.Update(subject);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CheckSubjectExist(subject.Id))
            {
                throw new Exception("There is no such a subject");
            }
            else
            {
                throw;
            }
        }
        return subject;

    }
    public Student Create()
    {
        var listOfSubjects = _context.Subject
                 .ToList();
        var newStudent = new Student();
        newStudent.AvailableSubjects = listOfSubjects;

        return newStudent;
    }

    public async Task<Student> Create(Student student, int[] subjectIdDst)
    {
        try
        {
            var chosenSubjects = _context.Subject
                .Where(s => subjectIdDst.Contains(s.Id))
                .ToList();
            var availableSubjects = _context.Subject
                .Where(s => !subjectIdDst.Contains(s.Id))
                .ToList();
            foreach (var chosenSubject in chosenSubjects)
            {
                student.AddSubject(chosenSubject);
            }
            await _context.Student.AddAsync(student);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }

        return student;
    }

     public async Task<bool>  StudentDeleteConfirmedProc(int id)
     {
        var result = false;
        var student = await _context.Student.FindAsync(id);
        if (student != null)
        {
            _context.Student.Remove(student);
        }

        var checkresult = await _context.SaveChangesAsync();
        result = checkresult > 0;
        return result;
     }

     public async Task<List<Student>> Index()
     {
        List<Student> model = await _context.Student.ToListAsync();
        return model;
     }

     public async Task<Student> EditStudent(int? id)
     {
        var student = await _context.Student.FindAsync(id);
        try
        {
            if (id != null)
            {
                if (student != null)
                {
                    var chosenSubjects = _context.StudentSubject
                        .Where(ss => ss.StudentId == id)
                        .Select(ss => ss.Subject)
                        .ToList();
                    var availableSubjects = _context.Subject
                        .Where(s => !chosenSubjects.Contains(s))
                        .ToList();
                    student.StudentSubjects = _context.StudentSubject
                        .Where(x => x.StudentId == id)
                        .ToList();
                    student.AvailableSubjects = availableSubjects;

                    return student;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception caught: " + ex.Message);
        }
        if (student != null)
        {
            return student;
        }
        else
        {
            throw new Exception("Studentu ploxo");
        }
     }

    public async Task<Student> EditStudent(Student student, int[] subjectIdDst)
    {
        var result = false;

        // Find the student
        var existingStudent = await _context.Student.FindAsync(student.Id);
        if (existingStudent != null)
        {
            // Update the student's properties
            existingStudent.Name = student.Name;
            existingStudent.Age = student.Age;
            existingStudent.Major = student.Major;

            // Get the chosen subjects
            var chosenSubjects = _context.Subject
                .Where(s => subjectIdDst.Contains(s.Id))
                .ToList();

            // Remove the existing StudentSubject entities for the student
            var studentSubjects = _context.StudentSubject
                .Where(ss => ss.StudentId == student.Id)
                .ToList();
            _context.StudentSubject.RemoveRange(studentSubjects);

            // Add new StudentSubject entities for the chosen subjects
            foreach (var subject in chosenSubjects)
            {
                var studentSubject = new StudentSubject
                {
                    Student = student,
                    Subject = subject
                };
                _context.StudentSubject.Add(studentSubject);
            }

            // Save changes to the database
            var resultInt = _context.SaveChanges();
            result = resultInt > 0;
        }
        if (existingStudent != null)
            return existingStudent;
        else
            throw new Exception("Can't edit student");
    }

    public async Task<Student?> DisplayStudent(int? id)
    {
        Student? student = null;
        try
        {
            student =  _context.Student
                .FirstOrDefault(m => m.Id == id);
            if (student is not null)
            {
                var studentSubjects = await _context.StudentSubject.Where(ss => ss.StudentId == id).Include(ss => ss.Subject).ToListAsync();
                student.StudentSubjects = studentSubjects;
            }
        }
        catch (Exception ex)
        {
           _logger.LogError("Exception caught in DisplayStudent: " + ex);
        }

        return student;
    }

    #endregion // Public Methods
}
