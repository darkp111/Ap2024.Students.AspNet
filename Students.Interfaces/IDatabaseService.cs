﻿using Students.Common.Models;

namespace Students.Interfaces;

public interface IDatabaseService
{
    Task<Student> EditStudent(Student student, int[] subjectIdDst);

    bool CheckStudentExist(int id);

    Task<bool> StudentDeleteConfirmedProc(int id);

    Task<Student> EditStudent(int? id);

    Task<Student?> DisplayStudent(int? id);

    Task<Student> Details(int? id);

    Student Create();

    Task<Student> Create(Student student, int[] subjectIdDst);

    Task<Subject> SubjectDetails(int? id);

    Task<bool> CreateSubject(Subject subject);

    Task<Subject> EditSubject(int? id);

    Task<Subject> DeleteSubject(int? id);

    Task<bool> SubjectDeleteConfirmed(int id);

    bool CheckSubjectExist(int id);

    Task<Subject> EditSubject(int id, Subject subject);

    Task<List<Student>> Index();
}
