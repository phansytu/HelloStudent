using System;
using System.Collections.Generic;
using System.Text;

namespace src
{
    public class StudentManager
    {
        List<Student> students = new List<Student>();
        public void NhapSinhVien()
        {

            string maSV = "";
            Console.WriteLine("Nhập thông tin sinh viên");
            while (true)
            {
                Console.WriteLine("Mã sinh viên: ");
                maSV = Console.ReadLine();
                //kiểm tra mã sinh viên có trùng hay không
                if (students.Exists(s => s.maSV == maSV))
                {
                    Console.WriteLine("Mã sinh viên đã tồn tại. Vui lòng nhập lại.");
                    
                }
                else
                {
                    break;
                }
            }
           
            Console.WriteLine("Họ tên: ");
            string hoTen = Console.ReadLine();
            Console.WriteLine("Tuổi: ");    
            int tuoi = int.Parse(Console.ReadLine());
            students.Add(new Student(maSV, hoTen, tuoi));

        }
        public void HienThiThongTin()
        {
            Console.WriteLine("Danh sách sinh viên:");
            Console.WriteLine($"| {"Mã SV",-10} | {"Họ và Tên",-25} | {"Tuổi",-5} |");
            Console.WriteLine(new string('-', 52));
            foreach (var student in students)
            {
                Console.WriteLine($"{student.maSV, -12} | {student.hoTen, -25} | {student.tuoi, -5} |");
            }
        }
    }
}
