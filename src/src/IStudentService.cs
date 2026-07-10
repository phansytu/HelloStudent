using System;
using System.Collections.Generic;
using System.Linq;
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
        bool RankStudent(string maSV, string xepLoai);
        IEnumerable<IGrouping<string, Student>> ThongKeSinhVienTheoXepLoai();
        IEnumerable<IGrouping<string, Student>> ThongKeSinhVienTheoMonHoc();
        IEnumerable<IGrouping<string, Student>> ThongKeSinhVienTheoLop();
        List<Student> SapXepSinhVienTheoDiemTrungBinh();
        List<Student> SapXepSinhVienTheoTen();
        IEnumerable<(string TenMonHoc, double DiemTrungBinh)> ThongKeDiemTrungBinhTheoMonHoc();

    }
}