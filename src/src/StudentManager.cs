using System;
using System.Collections.Generic;

namespace src
{
    public class StudentManager
    {
        // Thêm khởi tạo mặc định để danh sách không bao giờ null
        private List<Student> students { get; set; } = new List<Student>();
        private MonHocManager _monHocManager;

        public StudentManager(MonHocManager monHocManager)
        {
            _monHocManager = monHocManager;
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

                // Dùng dấu ? để đảm bảo s không null khi so sánh (Fix Warning dòng 24)
                if (students.Exists(s => s != null && s.maSV != null && s.maSV.Equals(maSV, StringComparison.OrdinalIgnoreCase)))
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

            students.Add(new Student(maSV, hoTen, tuoi));
            Console.WriteLine("Thêm sinh viên thành công!");
        }

        // 2. ĐĂNG KÝ MÔN HỌC & NHẬP ĐIỂM
        public void NhapDiemChoSinhVien()
        {
            Console.WriteLine("\n--- NHẬP ĐIỂM MÔN HỌC CHO SINH VIÊN ---");
            if (students.Count == 0)
            {
                Console.WriteLine("Chưa có sinh viên nào trong hệ thống.");
                return;
            }
            if (_monHocManager?.DanhSachMonHocGoc == null || _monHocManager.DanhSachMonHocGoc.Count == 0)
            {
                Console.WriteLine("Trường chưa cấu hình môn học nào. Hãy thêm môn học trước!");
                return;
            }

            Console.Write("Nhập mã sinh viên cần nhập điểm: ");
            string maSV = Console.ReadLine()?.Trim() ?? "";

            // Dùng kiểm tra an toàn trong lambda (Fix Warning dòng 41)
            Student? sv = students.Find(s => s != null && s.maSV != null && s.maSV.Equals(maSV, StringComparison.OrdinalIgnoreCase));

            if (sv == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên có mã vừa nhập!");
                return;
            }

            _monHocManager.HienThiDanhSachMon();
            Console.Write("Nhập tên môn học muốn đăng ký điểm: ");
            string tenMon = Console.ReadLine()?.Trim() ?? "";

            MonHoc? monGoc = _monHocManager.DanhSachMonHocGoc.Find(m => m != null && m.TenMon != null && m.TenMon.Equals(tenMon, StringComparison.OrdinalIgnoreCase));
            if (monGoc == null)
            {
                Console.WriteLine("Môn học này không tồn tại trong hệ thống của trường!");
                return;
            }

            // Đảm bảo danh sách môn học của sinh viên đã được khởi tạo
            if (sv.danhSachMonHoc == null)
            {
                sv.danhSachMonHoc = new List<MonHoc>();
            }

            if (sv.danhSachMonHoc.Exists(m => m != null && m.TenMon != null && m.TenMon.Equals(monGoc.TenMon, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Sinh viên này đã được nhập điểm môn này rồi! Hãy dùng tính năng Sửa nếu muốn thay đổi.");
                return;
            }

            int soTinChi = monGoc.SoTinChi;
            MonHoc monCuaSV = new MonHoc(monGoc.TenMon, soTinChi);

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

            sv.danhSachMonHoc.Add(monCuaSV);

            Console.WriteLine($"\nNhập điểm môn {monCuaSV.TenMon} cho sinh viên {sv.hoTen} thành công!");
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
            if (students.Count == 0)
            {
                Console.WriteLine("Danh sách trống.");
                return;
            }
            Console.WriteLine($"| {"Mã SV",-10} | {"Họ và Tên",-25} | {"Tuổi",-5} | {"ĐTB Hệ Số",-10} |");
            Console.WriteLine(new string('-', 62));
            foreach (var student in students)
            {
                if (student == null) continue;
                double dtb = student.TinhDiemTrungBinh();
                Console.WriteLine($"| {student.maSV,-10} | {student.hoTen,-25} | {student.tuoi,-5} | {Math.Round(dtb, 2),-10} |");
            }
        }

        // 4. TÌM KIẾM THEO MÃ
        public void TimKiemSinhVien()
        {
            Console.Write("\nNhập mã sinh viên cần tìm: ");
            string maSV = Console.ReadLine()?.Trim() ?? "";
            Student? sv = students.Find(s => s != null && s.maSV != null && s.maSV.Equals(maSV, StringComparison.OrdinalIgnoreCase));

            if (sv == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên!");
                return;
            }

            Console.WriteLine($"[KẾT QUẢ] SV: {sv.hoTen} - Tuổi: {sv.tuoi} - ĐTB: {Math.Round(sv.TinhDiemTrungBinh(), 2)}");
            if (sv.danhSachMonHoc == null) return;

            Console.WriteLine("Chi tiết các môn đã học:");
            foreach (var m in sv.danhSachMonHoc)
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
            Student? sv = students.Find(s => s != null && s.maSV != null && s.maSV.Equals(maSV, StringComparison.OrdinalIgnoreCase));

            if (sv == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên để sửa!");
                return;
            }

            Console.Write($"Nhập họ tên mới (Để trống nếu giữ nguyên '{sv.hoTen}'): ");
            string tenMoi = Console.ReadLine()?.Trim() ?? "";
            if (!string.IsNullOrEmpty(tenMoi)) sv.hoTen = tenMoi;

            Console.Write($"Nhập tuổi mới (Để trống nếu giữ nguyên '{sv.tuoi}'): ");
            string tuoiStr = Console.ReadLine()?.Trim() ?? "";
            if (!string.IsNullOrEmpty(tuoiStr))
            {
                if (int.TryParse(tuoiStr, out int tuoiMoi) && tuoiMoi > 0) sv.tuoi = tuoiMoi;
                else Console.WriteLine("Tuổi mới không hợp lệ, giữ nguyên tuổi cũ.");
            }

            Console.WriteLine("Cập nhật thông tin sinh viên thành công!");
        }

        // 6. XÓA SINH VIÊN
        public void XoaSinhVien()
        {
            Console.Write("\nNhập mã sinh viên cần xóa: ");
            string maSV = Console.ReadLine()?.Trim() ?? "";
            Student? sv = students.Find(s => s != null && s.maSV != null && s.maSV.Equals(maSV, StringComparison.OrdinalIgnoreCase));

            if (sv == null)
            {
                Console.WriteLine("Không tìm thấy sinh viên cần xóa!");
                return;
            }

            students.Remove(sv);
            Console.WriteLine($"Đã xóa thành công sinh viên {sv.hoTen} khỏi hệ thống.");
        }
    }
}