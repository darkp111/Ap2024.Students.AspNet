namespace Students.Interfaces;

public interface IDatabaseService
{
    bool EditStudent(int id, string name, int age, string major, int[] subjectIdDst);
}
