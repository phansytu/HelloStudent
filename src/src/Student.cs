using System;
using System.Collections.Generic;

namespace src
{
    public class Student
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public int Tuoi { get; set; }
        public List<Subject> DanhSachMonHoc { get; set; }

        public Student(string maSV, string hoTen, int tuoi)
        {
            this.MaSV = maSV;
            this.HoTen = hoTen;
            this.Tuoi = tuoi;
            this.DanhSachMonHoc = new List<Subject>();
        }

        public double TinhDiemTrungBinh()
        {
            if (DanhSachMonHoc.Count == 0) return 0;

            double tongDiemNhanTinChi = 0;
            int tongSoTinChi = 0;

            foreach (var monHoc in DanhSachMonHoc)
            {
                // Lấy điểm tổng kết đã tính theo các hệ số 1, 2, 3 ở trên
                double diemTongKetMon = monHoc.TinhDiemTongKetMon();

                tongDiemNhanTinChi += diemTongKetMon * monHoc.SoTinChi;
                tongSoTinChi += monHoc.SoTinChi;
            }

            return tongSoTinChi == 0 ? 0 : tongDiemNhanTinChi / tongSoTinChi;
        }
    }
}