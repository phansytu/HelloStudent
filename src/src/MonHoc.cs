using System;
using System.Collections.Generic;
using System.Linq;

namespace src
{
    public class MonHoc
    {
        public string TenMon { get; set; }
        public int SoTinChi { get; set; } // Thay thế cho HeSo cũ

        // Lưu trữ các điểm thành phần
        public List<double> DiemsHeSo1 { get; set; } = new List<double>();
        public List<double> DiemsHeSo2 { get; set; } = new List<double>();
        public double DiemChuyenCan { get; set; }
        public double DiemThi { get; set; }

        // Trọng số cấu hình theo đề bài của bạn
        public const double TrongSoChuyenCan = 3; // Hệ 3 tương đương trọng số/hệ số 3
        public const double TrongSoThi = 3;       // Hệ 3 tương đương trọng số/hệ số 3

        public MonHoc(string tenMon, int soTinChi)
        {
            TenMon = tenMon;
            SoTinChi = soTinChi;
        }

        // Hàm tính điểm tổng kết của riêng môn học này dựa trên các hệ số thành phần
        public double TinhDiemTongKetMon()
        {
            double tongDiemThanhPhan = 0;
            double tongHeSoThanhPhan = 0;

            // 1. Cộng các điểm hệ số 1
            foreach (var d in DiemsHeSo1)
            {
                tongDiemThanhPhan += d * 1;
                tongHeSoThanhPhan += 1;
            }

            // 2. Cộng các điểm hệ số 2
            foreach (var d in DiemsHeSo2)
            {
                tongDiemThanhPhan += d * 2;
                tongHeSoThanhPhan += 2;
            }

            // 3. Cộng điểm chuyên cần (Hệ số 3)
            tongDiemThanhPhan += DiemChuyenCan * TrongSoChuyenCan;
            tongHeSoThanhPhan += TrongSoChuyenCan;

            // 4. Cộng điểm thi (Hệ số 3)
            tongDiemThanhPhan += DiemThi * TrongSoThi;
            tongHeSoThanhPhan += TrongSoThi;

            // Tránh chia cho 0 nếu chưa nhập điểm nào
            return tongHeSoThanhPhan == 0 ? 0 : tongDiemThanhPhan / tongHeSoThanhPhan;
        }
    }
}