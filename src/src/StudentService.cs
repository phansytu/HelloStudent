using System;
using System.Collections.Generic;

namespace src 
{
    // Đảm bảo class này đã kế thừa giao diện IStudentService của bạn
    public class StudentService : IStudentService
    {
        private List<Student> _students = new List<Student>();
        
        // CHUẨN: Tiêm Interface Service môn học vào đây, KHÔNG tiêm Manager
        private readonly ISubjectService _subjectService;

        public StudentService(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        public List<Student> GetAllStudents()
        {
            return _students;
        }

        public Student FindByMaSV(string maSV)
        {
            return _students.Find(s => s.MaSV.Equals(maSV, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsStudentExists(string maSV)
        {
            return _students.Exists(s => s.MaSV.Equals(maSV, StringComparison.OrdinalIgnoreCase));
        }

        // Thay vì throw Exception gây crash app Console, hãy chuyển sang dùng bool để báo trạng thái
        public bool AddStudent(Student student)
        {
            if (IsStudentExists(student.MaSV)) return false;

            _students.Add(student);
            return true;
        }

        public bool UpdateStudent(Student student)
        {
            var existingStudent = FindByMaSV(student.MaSV);
            if (existingStudent == null) return false;

            existingStudent.HoTen = student.HoTen;
            existingStudent.Tuoi = student.Tuoi;
            existingStudent.DanhSachMonHoc = student.DanhSachMonHoc;
            return true;
        }

        public bool DeleteStudent(string maSV)
        {
            var student = FindByMaSV(maSV);
            if (student == null) return false;

            _students.Remove(student);
            return true;
        }

        public bool HasStudentTakenCourse(string maSV, string maMonHoc)
        {
            var student = FindByMaSV(maSV);
            if (student == null) return false;

            return student.DanhSachMonHoc.Exists(m => m.MaMonHoc.Equals(maMonHoc, StringComparison.OrdinalIgnoreCase));
        }

        // =================================================================
        // CHUẨN HÓA SỬA ĐỔI: Tính điểm tổng kết môn CỦA MỘT SINH VIÊN CỤ THỂ
        // =================================================================
        public double TinhDiemTongKetMon(string maSV, string tenMon)
        {
            // 1. Tìm xem sinh viên đó có tồn tại không
            var student = FindByMaSV(maSV);
            if (student == null) return -1; // Sinh viên không tồn tại

            // 2. Tìm môn học đó trong danh sách các môn MÀ SINH VIÊN NÀY ĐÃ HỌC
            var subject = student.DanhSachMonHoc.Find(m => m.TenMon.Equals(tenMon, StringComparison.OrdinalIgnoreCase));
            
            // 3. Nếu tìm thấy môn trong người SV, bảo đối tượng môn đó tự tính điểm
            if (subject != null)
            {
                return subject.TinhDiemTongKetMon(); // Gọi hàm gốc từ Subject.cs
            }

            return -2; // Sinh viên tồn tại nhưng chưa học/đăng ký môn này
        }
    }
}