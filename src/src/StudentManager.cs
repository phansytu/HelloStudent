using System;
using System.Collections.Generic;

namespace src
{
    public class StudentManager
    {
        //Sử dụng interface IStudentService để quản lý danh sách sinh viên
        private IStudentService _studentService;
        private ISubjectService _subjectService;

        public StudentManager(IStudentService studentService, ISubjectService subjectService)
        {
            _studentService = studentService;
            _subjectService = subjectService;
        }

        // 1. THÊM SINH VIÊN
        public void NhapSinhVien()
        {
            Console.WriteLine("\n--- NHẬP THÔNG TIN SINH VIÊN ---");
            string maSV = "";
            while (true)
            {
                Console.Write("Mã sinh viên: ");
                maSV = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrEmpty(maSV))
                {
                    Console.WriteLine("Mã sinh viên không được để trống!");
                    continue;
                }

        
                if (_studentService.IsStudentExists(maSV))
                {
                    Console.WriteLine("Mã sinh viên đã tồn tại. Vui lòng nhập lại.");
                }
                else
                {
                    break;
                }
            }

            Console.Write("Họ tên: ");
            string hoTen = Console.ReadLine()?.Trim() ?? "";

            int tuoi;
            while (true)
            {
                Console.Write("Tuổi: ");
                if (int.TryParse(Console.ReadLine(), out tuoi) && tuoi > 0) break;
                Console.WriteLine("Tuổi phải là số nguyên dương hợp lệ!");
            }

            _studentService.AddStudent(new Student(maSV, hoTen, tuoi));
            Console.WriteLine("Thêm sinh viên thành công!");
        }

        // 2. ĐĂNG KÝ MÔN HỌC & NHẬP ĐIỂM
        public void NhapDiemChoSinhVien()
        {
            Console.WriteLine("\n--- NHẬP ĐIỂM MÔN HỌC CHO SINH VIÊN ---");
            if (_studentService.GetAllStudents().Count == 0)
            {
                Console.WriteLine("Chưa có sinh viên nào trong hệ thống.");
                return;
            }
            if (_subjectService?.GetAllSubjects() == null || _subjectService.GetAllSubjects().Count == 0)
            {
                Console.WriteLine("Trường chưa cấu hình môn học nào. Hãy thêm môn học trước!");
                return;
            }

            Console.Write("Nhập mã sinh viên cần nhập điểm: ");
            string maSV = Console.ReadLine()?.Trim() ?? "";

            // Dùng kiểm tra an toàn trong lambda (Fix Warning dòng 41)
            Student? sv = _studentService.FindByMaSV(maSV);

            if (sv == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên có mã vừa nhập!");
                return;
            }

            _subjectService.GetAllSubjects();
            Console.Write("Nhập tên môn học muốn đăng ký điểm: ");
            string tenMon = Console.ReadLine()?.Trim() ?? "";

            Subject? monGoc = _subjectService.FindByTenMon(tenMon);
            if (monGoc == null)
            {
                Console.WriteLine("Môn học này không tồn tại trong hệ thống của trường!");
                return;
            }

            // Đảm bảo danh sách môn học của sinh viên đã được khởi tạo
            if (sv.DanhSachMonHoc == null)
            {
                sv.DanhSachMonHoc = new List<Subject>();
            }

            if (sv.DanhSachMonHoc.Exists(m => m != null && m.TenMon != null && m.TenMon.Equals(monGoc.TenMon, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Sinh viên này đã được nhập điểm môn này rồi! Hãy dùng tính năng Sửa nếu muốn thay đổi.");
                return;
            }

            int soTinChi = monGoc.SoTinChi;
            Subject monCuaSV = new Subject(monGoc.MaMonHoc, monGoc.TenMon, soTinChi);

            int soLuongHeSo1 = 1;
            int soLuongHeSo2 = 1;
            if (soTinChi == 3)
            {
                soLuongHeSo1 = 1;
                soLuongHeSo2 = 3;
            }
            else if (soTinChi == 2)
            {
                soLuongHeSo1 = 1;
                soLuongHeSo2 = 2;
            }

            Console.WriteLine($"\n[Cấu hình môn {monCuaSV.TenMon} - {soTinChi} Tín chỉ]: Yêu cầu nhập {soLuongHeSo1} điểm HS1, {soLuongHeSo2} điểm HS2, 1 điểm Chuyên cần (HS3), 1 điểm Thi (HS3).");

            for (int i = 1; i <= soLuongHeSo1; i++)
            {
                double diemHS1 = NhapDiemHopLe($"Nhập điểm hệ số 1 (lần {i})");
                monCuaSV.DiemsHeSo1.Add(diemHS1);
            }

            for (int i = 1; i <= soLuongHeSo2; i++)
            {
                double diemHS2 = NhapDiemHopLe($"Nhập điểm hệ số 2 (lần {i})");
                monCuaSV.DiemsHeSo2.Add(diemHS2);
            }

            monCuaSV.DiemChuyenCan = NhapDiemHopLe("Nhập điểm Chuyên Cần (Hệ số 3)");
            monCuaSV.DiemThi = NhapDiemHopLe("Nhập điểm Thi kết thúc học phần (Hệ số 3)");

            sv.DanhSachMonHoc.Add(monCuaSV);

            Console.WriteLine($"\nNhập điểm môn {monCuaSV.TenMon} cho sinh viên {sv.HoTen} thành công!");
            Console.WriteLine($"-> Điểm tổng kết môn đạt: {Math.Round(monCuaSV.TinhDiemTongKetMon(), 2)}");
        }

        private double NhapDiemHopLe(string thongBao)
        {
            double diem;
            while (true)
            {
                Console.Write($"{thongBao}: ");
                if (double.TryParse(Console.ReadLine(), out diem) && diem >= 0 && diem <= 10)
                {
                    return diem;
                }
                Console.WriteLine("Điểm số không hợp lệ! Vui lòng nhập số thực trong khoảng từ 0.0 đến 10.0.");
            }
        }

        // 3. HIỂN THỊ THÔNG TIN
        public void HienThiThongTin()
        {
            Console.WriteLine("\n--- DANH SÁCH SINH VIÊN VÀ ĐIỂM TRUNG BÌNH ---");
            var danhSach = _studentService.GetAllStudents();
            if (danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách trống.");
                return;
            }
            Console.WriteLine($"| {"Mã SV",-10} | {"Họ và Tên",-25} | {"Tuổi",-5} | {"ĐTB Hệ Số",-10} |");
            Console.WriteLine(new string('-', 62));
            foreach (var student in danhSach)
            {
                if (student == null) continue;
                double dtb = student.TinhDiemTrungBinh();
                Console.WriteLine($"| {student.MaSV,-10} | {student.HoTen,-25} | {student.Tuoi,-5} | {Math.Round(dtb, 2),-10} |");
            }
        }

        // 4. TÌM KIẾM THEO MÃ
        public void TimKiemSinhVien()
        {
            Console.Write("\nNhập mã sinh viên cần tìm: ");
            string maSV = Console.ReadLine()?.Trim() ?? "";
            Student? sv = _studentService.FindByMaSV(maSV);

            if (sv == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên!");
                return;
            }

            Console.WriteLine($"[KẾT QUẢ] SV: {sv.HoTen} - Tuổi: {sv.Tuoi} - ĐTB: {Math.Round(sv.TinhDiemTrungBinh(), 2)}");
            if (sv.DanhSachMonHoc == null) return;

            Console.WriteLine("Chi tiết các môn đã học:");
            foreach (var m in sv.DanhSachMonHoc)
            {
                if (m == null) continue;
                Console.WriteLine($"  + Môn: {m.TenMon} (Số tín chỉ: {m.SoTinChi}) => Tổng kết môn: {Math.Round(m.TinhDiemTongKetMon(), 2)} điểm");
            }
        }

        // 5. SỬA THÔNG TIN SINH VIÊN
        public void SuaSinhVien()
        {
            Console.Write("\nNhập mã sinh viên cần sửa: ");
            string maSV = Console.ReadLine()?.Trim() ?? "";
            Student? sv = _studentService.FindByMaSV(maSV);

            if (sv == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên để sửa!");
                return;
            }

            Console.Write($"Nhập họ tên mới (Để trống nếu giữ nguyên '{sv.HoTen}'): ");
            string tenMoi = Console.ReadLine()?.Trim() ?? "";
            if (!string.IsNullOrEmpty(tenMoi)) sv.HoTen = tenMoi;

            Console.Write($"Nhập tuổi mới (Để trống nếu giữ nguyên '{sv.Tuoi}'): ");
            string tuoiStr = Console.ReadLine()?.Trim() ?? "";
            if (!string.IsNullOrEmpty(tuoiStr))
            {
                if (int.TryParse(tuoiStr, out int tuoiMoi) && tuoiMoi > 0) sv.Tuoi = tuoiMoi;
                else Console.WriteLine("Tuổi mới không hợp lệ, giữ nguyên tuổi cũ.");
            }

            Console.WriteLine("Cập nhật thông tin sinh viên thành công!");
        }

        // 6. XÓA SINH VIÊN
        public void XoaSinhVien()
        {
            Console.Write("\nNhập mã sinh viên cần xóa: ");
            string maSV = Console.ReadLine()?.Trim() ?? "";
            Student? sv = _studentService.FindByMaSV(maSV);

            if (sv == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên cần xóa!");
                return;
            }

            _studentService.DeleteStudent(maSV);
            Console.WriteLine($"Đã xóa thành công sinh viên {sv.HoTen} khỏi hệ thống.");
        }
    }
}