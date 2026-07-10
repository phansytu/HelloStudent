# HelloStudent - Hệ Thống Quản Lý Sinh Viên & Điểm Tín Chỉ

## Mục Tiêu Dự Án
Ứng dụng Console giúp quản lý thông tin sinh viên, danh mục môn học của trường và thực hiện tính toán điểm tổng kết môn, điểm trung bình (ĐTB) tích lũy dựa trên số tín chỉ. Dự án được tích hợp các cơ chế bẫy lỗi nhập liệu dữ liệu (chống crash tuyệt đối) và kiểm tra trùng lặp dữ liệu thông minh.

---

## Cấu Trúc Dự Án & Kiến Trúc Code
src
│
├── Student.cs              // Model sinh viên
├── Subject.cs              // Model môn học
│
├── IStudentService.cs      // Interface quản lý sinh viên
├── StudentService.cs       // Cài đặt nghiệp vụ quản lý sinh viên
│
├── ISubjectService.cs      // Interface quản lý môn học
├── SubjectService.cs       // Cài đặt nghiệp vụ quản lý môn học
│
└── Program.cs              // Điểm khởi chạy chương trình

---

## Công Nghệ & Công Cụ Sử Dụng

* **Ngôn ngữ:** C# (.NET Core 6.0 trở lên)
* **Loại ứng dụng:** Windows/Linux Console Application
* **Tính năng nâng cao:** Sử dụng `double.TryParse` / `int.TryParse` chống crash, Generic `List<T>`, LINQ (`Exists`, `Find`).
* **IDE Khuyên dùng:** Visual Studio 2022 / VS Code

---

## Hướng Dẫn Cài Đặt và Chạy Dự Án

### 1. Sao chép dự án về máy (Clone)
Mở Git Bash hoặc Terminal tại thư mục bạn muốn lưu và gõ:
```bash
git clone [https://github.com/phansytu/HelloStudent.git](https://github.com/phansytu/HelloStudent.git)
cd HelloStudent
2. Biên dịch và Chạy ứng dụng
Cách 1: Sử dụng Visual Studio 2022 (Khuyên dùng)
Mở Visual Studio, chọn Open a project or solution.

Điều hướng đến thư mục dự án và mở file HelloStudent.sln.

Nhấn phím F5 (Debug) hoặc Ctrl + F5 để khởi chạy chương trình.

Cách 2: Sử dụng Command Line (Terminal / VS Code)
Chạy lệnh sau tại thư mục gốc của dự án:

Bash
dotnet run --project src
### Các Chức Năng Chính Trong Hệ Thống
Chương trình cung cấp menu tương tác trực quan:
1 Thêm môn học mới vào danh mục chung của trường (nhập số tín chỉ).
2 Thêm sinh viên mới (tự động kiểm tra trùng Mã SV).
3 Đăng ký môn và nhập điểm cho sinh viên (Hệ thống tự động yêu cầu số lượng điểm HS1, HS2 tương ứng với môn 2 hay 3 tín chỉ).
4 Hiển thị bảng danh sách toàn bộ sinh viên kèm ĐTB Tích lũy.
5 Tìm kiếm sinh viên theo Mã SV & hiển thị chi tiết bảng điểm các môn đã học.
6 Sửa thông tin Họ tên, Tuổi của sinh viên.
7 Xóa sinh viên khỏi hệ thống.
0 Thoát ứng dụng.
---
### Gợi ý các bước cập nhật file này lên GitHub:
Sau khi bạn dán nội dung trên vào file `README.md` và lưu lại, hãy chạy cụm lệnh sau trong Git Bash để đồng bộ lên GitHub:

```bash
git add README.md
git commit -m "docs: update README.md to reflect new credit-based system structure"
git push origin main
