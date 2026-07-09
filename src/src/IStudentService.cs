
namespace src
{
    public interface IStudentService
    {
        List<Student> GetAllStudents();
        Student FindByMaSV(string maSV);
        bool IsStudentExists(string maSV);
        bool AddStudent(Student student);
        bool UpdateStudent(Student student);
        bool DeleteStudent(string maSV);
        bool HasStudentTakenCourse(string maSV, string maMonHoc);
        double TinhDiemTongKetMon(string maSV, string tenMon);
    }
}