using System;
using System.Collections.Generic;

namespace src
{
    public class Student
    {
        public string maSV { get; set; }
        public string hoTen { get; set; }
        public int tuoi { get; set; }
        public List<MonHoc> danhSachMonHoc { get; set; }

        public Student(string maSV, string hoTen, int tuoi)
        {
            this.maSV = maSV;
            this.hoTen = hoTen;
            this.tuoi = tuoi;
            this.danhSachMonHoc = new List<MonHoc>();
        }

        public double TinhDiemTrungBinh()
        {
            if (danhSachMonHoc.Count == 0) return 0;

            double tongDiemNhanTinChi = 0;
            int tongSoTinChi = 0;

            foreach (var monHoc in danhSachMonHoc)
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