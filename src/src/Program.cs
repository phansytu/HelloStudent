namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Nhập số lượng sinh viên: ");
            int n = int.Parse(Console.ReadLine());
            StudentManager studentManager = new StudentManager();
            bool run = true;
            while (run)
            {
                if (n == 0)
                {
                    Console.WriteLine("Không có sinh viên nào để nhập thông tin.");
                    run = false;

                }
                else
                {

                    for (int i = 0; i < n; i++)
                    {
                        Console.WriteLine($"Nhập thông tin sinh viên thứ {i + 1}:");
                        studentManager.NhapSinhVien();
                    }
                    
                    studentManager.HienThiThongTin();
                    run = false;
                }
                Console.ReadKey();

            }
        }
    }
}