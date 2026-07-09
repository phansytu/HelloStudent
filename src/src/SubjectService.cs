namespace src
{
    public class SubjectService : ISubjectService
    {
        private List<Subject> _subject = new List<Subject>();

        public SubjectService()
        {
            // Khởi tạo danh sách môn học nếu cần
        }

        // Lấy toàn bộ danh sách môn học
        public List<Subject> GetAllSubjects()
        {
            return _subject;
        }

        // Tìm kiếm môn học theo tên môn học
        public Subject FindByTenMon(string tenMon)
        {
            return _subject.Find(m => m.TenMon.Equals(tenMon, StringComparison.OrdinalIgnoreCase));
        }

        // Tìm kiếm môn học theo mã môn học
        public Subject FindByMaMon(string maMon)
        {
            return _subject.Find(m => m.MaMonHoc.Equals(maMon, StringComparison.OrdinalIgnoreCase));
        }

        // Kiểm tra môn học đã tồn tại hay chưa
        public bool IsSubjectExists(string tenMon)
        {
            return _subject.Exists(m => m.TenMon.Equals(tenMon, StringComparison.OrdinalIgnoreCase));
        }

        // Thêm môn học mới
        public bool AddSubject(string tenMon, int soTinChi)
        {
            if (_subject.Exists(m => m.TenMon.Equals(tenMon, StringComparison.OrdinalIgnoreCase)))
            {
                return false; // Môn học đã tồn tại, không thêm
            }
    
        
            int nextId = _subject.Count +1;
            string maMonHoc = $"MH{nextId:D3}";
            Subject newSubject = new Subject(maMonHoc, tenMon, soTinChi);
            _subject.Add(newSubject);
            return true;

        }

        // Cập nhật thông tin môn học
        public void UpdateSubject(Subject Subject)
        {
            var existingSubject = FindByMaMon(Subject.MaMonHoc);
            if (existingSubject != null)
            {
                existingSubject.SoTinChi = Subject.SoTinChi;
                existingSubject.DiemsHeSo1 = Subject.DiemsHeSo1;
                existingSubject.DiemsHeSo2 = Subject.DiemsHeSo2;
                existingSubject.DiemChuyenCan = Subject.DiemChuyenCan;
                existingSubject.DiemThi = Subject.DiemThi;
            }
            else
            {
                throw new Exception("Môn học không tồn tại.");
            }
        }

        // Xóa môn học theo mã môn học
        public void DeleteSubject(string maMon)
        {
            var subject = FindByMaMon(maMon);
            if (subject != null)
            {
                _subject.Remove(subject);
            }
            else
            {
                throw new Exception("Môn học không tồn tại.");
            }
        }
    }
}