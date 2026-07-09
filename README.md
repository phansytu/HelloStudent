# HelloStudent - Hệ Thống Quản Lý Sinh Viên & Điểm Tín Chỉ

## Mục Tiêu Dự Án
Ứng dụng Console giúp quản lý thông tin sinh viên, danh mục môn học của trường và thực hiện tính toán điểm tổng kết môn, điểm trung bình (ĐTB) tích lũy dựa trên số tín chỉ. Dự án được tích hợp các cơ chế bẫy lỗi nhập liệu dữ liệu (chống crash tuyệt đối) và kiểm tra trùng lặp dữ liệu thông minh.

---

## Cấu Trúc Dự Án & Kiến Trúc Code

Dự án tuân thủ nghiêm ngặt tư duy lập trình hướng đối tượng (OOP) và kiến trúc phân tách trách nhiệm (Separation of Concerns), bao gồm các lớp chính trong namespace `src`:

1. **`MonHoc.cs` (Model):** Định nghĩa cấu trúc môn học gồm tên môn, số tín chỉ, danh sách điểm thành phần (Hệ số 1, Hệ số 2), điểm chuyên cần và điểm thi (Hệ số 3). Tích hợp logic tự động tính điểm tổng kết môn học.
2. **`Student.cs` (Model):** Định nghĩa thông tin sinh viên (`maSV`, `hoTen`, `tuoi`) và danh sách các môn học sinh viên đó đã đăng ký. Tích hợp hàm tính Điểm trung bình tích lũy hệ số 10 có trọng số theo số tín chỉ.
3. **`MonHocManager.cs` (Business Logic):** Quản lý danh mục môn học gốc của toàn trường. Xử lý logic thêm môn học mới (chống trùng tên) và hiển thị danh sách môn học.
4. **`StudentManager.cs` (Business Logic):** Quản lý danh sách sinh viên. Chứa các nghiệp vụ cốt lõi: Thêm sinh viên (chống trùng mã), đăng ký môn & nhập điểm thành phần cho sinh viên (tự động cấu hình số lượng đầu điểm theo số tín chỉ), tìm kiếm, sửa đổi và xóa sinh viên.
5. **`Program.cs` (Entry Point):** Điểm vào của ứng dụng, thiết lập bảng mã UTF-8 hiển thị tiếng Việt và điều khiển luồng thực thi thông qua hệ thống Menu CLI tương tác (0-7).

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
