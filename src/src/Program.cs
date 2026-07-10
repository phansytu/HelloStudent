using System;
using System.Text;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; // Đảm bảo hiển thị tiếng Việt Console
            Console.InputEncoding = Encoding.UTF8;
           //Khởi tạo cụm Môn học trước
            ISubjectService subjectService = new SubjectService();
            SubjectManager subjectManager = new SubjectManager(subjectService);
            // Khởi tạo cụm Sinh viên
            IStudentService studentService = new StudentService(subjectService);
            StudentManager studentManager = new StudentManager(studentService, subjectService);

            // Dữ liệu mẫu nạp sẵn để bạn dễ test đỡ phải nhập lại nhiều lần
            subjectService.AddSubject("Toan Cao Cap", 3);
            subjectService.AddSubject("Triet Hoc", 2);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n================ QUẢN LÝ SINH VIÊN ================");
                Console.WriteLine("1. Thêm môn học mới của trường");
                Console.WriteLine("2. Thêm sinh viên mới");
                Console.WriteLine("3. Đăng ký môn & Nhập điểm cho sinh viên");
                Console.WriteLine("4. Hiển thị danh sách sinh viên & ĐTB");
                Console.WriteLine("5. Tìm kiếm sinh viên theo Mã");
                Console.WriteLine("6. Sửa thông tin sinh viên");
                Console.WriteLine("7. Xóa sinh viên");
                Console.WriteLine("8. Xếp loại sinh viên");
                Console.WriteLine("9. Thống kê số lượng sinh viên theo xếp loại");
                Console.WriteLine("10. Thống kê số lượng sinh viên theo môn học");
                Console.WriteLine("11. Thống kê số lượng sinh viên theo lớp");
                Console.WriteLine("12. Sắp xếp danh sách sinh viên theo điểm trung bình");
                Console.WriteLine("13. Sắp xếp danh sách sinh viên theo tên");
                Console.WriteLine("0. Thoát chương trình");
                Console.Write("Chọn chức năng (0-13): ");

                string chon = Console.ReadLine() ?? "";
                switch (chon)
                {
                    case "1": subjectManager.NhapMonHoc(); break;
                    case "2": studentManager.NhapSinhVien(); break;
                    case "3": studentManager.NhapDiemChoSinhVien(); break;
                    case "4": studentManager.HienThiThongTin(); break;
                    case "5": studentManager.TimKiemSinhVien(); break;
                    case "6": studentManager.SuaSinhVien(); break;
                    case "7": studentManager.XoaSinhVien(); break;
                    case "8": studentManager.XepLoaiSinhVien(); break;
                    case "9": studentManager.ThongKeSinhVienTheoXepLoai(); break;
                    case "10": studentManager.ThongKeSinhVienTheoMonHoc(); break;
                    case "11": studentManager.ThongKeSinhVienTheoLop(); break;
                    case "12": studentManager.SapXepSinhVienTheoDiemTrungBinh(); break;
                    case "13": studentManager.SapXepSinhVienTheoTen(); break;
                    case "0": return;
                    default: Console.WriteLine("Chức năng không hợp lệ, vui lòng chọn lại!"); break;
                }
                Console.ReadKey();
           }
        }
    }
}