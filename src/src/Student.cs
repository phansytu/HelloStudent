using System;
using System.Collections.Generic;
using System.Text;

namespace src
{
    public class Student
    {
        public string maSV { get; set; }
        public string hoTen { get; set; }
        public int tuoi { get; set; }
        public Student(string maSV, string hoTen, int tuoi)
        {
            this.maSV = maSV;
            this.hoTen = hoTen;
            this.tuoi = tuoi;
        }

        
    }
}
