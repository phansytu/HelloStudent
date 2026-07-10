using System;
using System.Collections.Generic;

namespace src
{
    public class SubjectManager
    {
        // Danh mục quản lý các môn học gốc của nhà trường
        private ISubjectService _subjectService;
        
        public SubjectManager(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        public void NhapMonHoc()
        {
            Console.WriteLine("\n--- NHẬP THÔNG TIN MÔN HỌC MỚI CỦA TRƯỜNG ---");
            string tenMon = "";
            

            // 1. Nhập và kiểm tra trùng lặp tên môn học
            while (true)
            {
                Console.Write("Tên môn học: ");
                tenMon = (Console.ReadLine() ?? "").Trim();
                if (string.IsNullOrEmpty(tenMon))
                {
                    Console.WriteLine("Tên môn học không được để trống!");
                    continue;
                }

                // Kiểm tra trùng tên môn học (Không phân biệt hoa thường)
                if (_subjectService.IsSubjectExists(tenMon))
                {
                    Console.WriteLine("Môn học này đã tồn tại trong hệ thống trường. Vui lòng nhập tên khác!");
                }
                else
                {
                    break;
                }
            }

            // 2. Nhập và kiểm tra số tín chỉ (hệ số môn học)
            int soTinChi;
            while (true)
            {
                Console.Write("Số tín chỉ của môn học (2, 3...): ");
                // Đã sửa lỗi khai báo trùng biến (inline variable) và ép điều kiện tín chỉ phải từ 2 trở lên theo logic tính điểm thành phần
                if (int.TryParse(Console.ReadLine(), out soTinChi) && (soTinChi == 2 || soTinChi == 3))
                {
                    break;
                }
                Console.WriteLine("Hệ thống hiện tại chỉ hỗ trợ cấu hình môn 2 hoặc 3 tín chỉ. Vui lòng nhập lại!");
            }

            // 3. Khởi tạo đối tượng môn học gốc dựa trên Constructor phù hợp
            // Lưu ý: Đối với danh mục gốc, ta gán số tín chỉ vào thuộc tính HeSo để StudentManager lấy ra xử lý
            bool success = _subjectService.AddSubject(tenMon, soTinChi);
            if (success)
            {
                var monVuaThem = _subjectService.FindByMaMon(_subjectService.GetAllSubjects().Last().MaMonHoc);
                Console.WriteLine($"Thêm thành công! Môn học đã được cấp mã tự động: {monVuaThem?.MaMonHoc}");
            }
            else{
                Console.WriteLine("Thêm môn học thất bại! Có thể môn học đã tồn tại hoặc có lỗi hệ thống.");
            }
            Console.WriteLine($"Thêm môn học '{tenMon}' ({soTinChi} tín chỉ) vào hệ thống trường thành công!");
        }

        // Chức năng hiển thị danh mục môn giảng dạy của nhà trường
        public void HienThiDanhSachMon()
        {
            if (_subjectService.GetAllSubjects().Count == 0)
            {
                Console.WriteLine("Chưa có môn học nào trong hệ thống trường.");
                return;
            }
            Console.WriteLine("\n--- DANH SÁCH MÔN HỌC HIỆN CÓ CỦA TRƯỜNG ---");
            Console.WriteLine($"| {"STT",-4} | {"Tên Môn Học",-25} | {"Số Tín Chỉ",-12} |");
            Console.WriteLine(new string('-', 49));

            for (int i = 0; i < _subjectService.GetAllSubjects().Count; i++)
            {
                var m = _subjectService.GetAllSubjects()[i];
                // Sử dụng m.HeSo vì lớp MonHoc gốc đang dùng thuộc tính này lưu số tín chỉ
                Console.WriteLine($"| {i + 1,-4} | {m.TenMon,-25} | {m.SoTinChi,-12} |");
            }
        }
    }
}