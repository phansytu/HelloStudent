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
        public bool RankStudent(string maSV,string xepLoai)
        {
            var student = FindByMaSV(maSV);
            if (student == null) {
                xepLoai = "Không tìm thấy sinh viên";
                return false;
            } // Sinh viên không tồn tại
            student.XepLoai = xepLoai; // Gán giá trị xếp loại vào thuộc tính của sinh viên
            return true;

            
        }
        public IEnumerable<IGrouping<string, Student>> ThongKeSinhVienTheoXepLoai()
        {
            // Giả sử bạn có một thuộc tính Rank trong Student
            return _students.GroupBy(s => s.XepLoai ?? "Chưa xếp loại");
        }
        public IEnumerable<IGrouping<string, Student>> ThongKeSinhVienTheoMonHoc()
        {
            return _students.GroupBy(s => s.DanhSachMonHoc.Count > 0 
            ? string.Join(", ", s.DanhSachMonHoc.Select(m => m.TenMon)) : "Chưa đăng ký môn học");
        }
        public IEnumerable<IGrouping<string, Student>> ThongKeSinhVienTheoLop()
        {
            return _students.GroupBy(s => string.IsNullOrEmpty(s.Lop) ? "Chưa có lớp" : s.Lop);
        }
        public List<Student> SapXepSinhVienTheoDiemTrungBinh()
        {
            var sortedList = new List<Student>(_students);
            sortedList.Sort((s1, s2) => s2.TinhDiemTrungBinh().CompareTo(s1.TinhDiemTrungBinh()));
            return sortedList;
        }
        public List<Student> SapXepSinhVienTheoTen()
        {
            var sortedList = new List<Student>(_students);
            sortedList.Sort((s1, s2) => string.Compare(s1.HoTen, s2.HoTen, StringComparison.OrdinalIgnoreCase));
            return sortedList;
        }
        public IEnumerable<(string TenMonHoc, double DiemTrungBinh)> ThongKeDiemTrungBinhTheoMonHoc()
        {
            var monHocDiemTrungBinh = new Dictionary<string, List<double>>();

            foreach (var student in _students)
            {
                foreach (var monHoc in student.DanhSachMonHoc)
                {
                    double diemTongKetMon = monHoc.TinhDiemTongKetMon();
                    if (!monHocDiemTrungBinh.ContainsKey(monHoc.TenMon))
                    {
                        monHocDiemTrungBinh[monHoc.TenMon] = new List<double>();
                    }
                    monHocDiemTrungBinh[monHoc.TenMon].Add(diemTongKetMon);
                }
            }

            return monHocDiemTrungBinh.Select(kvp => (kvp.Key, kvp.Value.Average()));
        }
            
                          
    }
}