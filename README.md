Mini Project: HelloStudent 

Mục tiêu: Quản lý thông tin sinh viên (Mã SV, Tên, Tuổi) nhập từ bàn phím, hiển thị danh sách sinh viên.
##  Cấu Trúc Dự Án & Kiến Trúc Code

Dự án tuân thủ tư duy lập trình hướng đối tượng (OOP) và được chia làm 3 file chính:

1. **`Student.cs` (Data Model):** Định nghĩa cấu trúc đối tượng Sinh viên gồm: `maSV`, `hoTen`, `tuoi` và hàm khởi tạo (Constructor).
2. **`StudentManager.cs` (Business Logic Layer):** Quản lý bộ nhớ tạm `List<Student>`. Chứa logic kiểm tra trùng mã, vòng lặp `while(true)` để bắt nhập đúng mã và hàm định dạng hiển thị bảng.
3. **`Program.cs` (Entry Point):** Điểm vào của ứng dụng, chứa logic điều khiển luồng thực thi chính.

##  Công Nghệ & Công Cụ Sử Dụng

* **Ngôn ngữ: C# (.NET Core / .NET 6 hoặc mới hơn)
* **Môi trường chạy:** Windows Console Application
* **IDE Khuyên dùng:** Visual Studio 2022 / VS Code

## Hướng Dẫn Cài Đặt và Chạy Dự Án

### 1. Sao chép dự án về máy (Clone)
Mở Git Bash hoặc Terminal tại thư mục bạn muốn lưu và gõ:
```bash
git clone [https://github.com/phansytu/HelloStudent.git](https://github.com/phansytu/HelloStudent.git)
cd HelloStudent
```
### 2. Mở dự án trong Visual Studio

 Mở Visual Studio, chọn "Open a project or solution", điều hướng đến thư mục dự án và mở file `HelloStudent.sln`.
### 3. Biên dịch và Chạy ứng dụng
Dùng Visual Studio: Mở file .sln, nhấn phím F5 để chạy ở chế độ Debug (giúp bắt lỗi crash nếu có).

Dùng Terminal/VS Code: Chạy lệnh sau:
```Bash
dotnet run
```