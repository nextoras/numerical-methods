
using System;

namespace PUIN
{
    class Program
    {
        static int n = 5;
        static double[] f = new double[70]; // массив х нашей табулируемой функции
        static double[] Xif = new double[70]; // все точки для f
        static double[] Xil = new double[12]; // значения точек x с чертой
        static double[] Ln = new double[11]; // значения полинома
        static double[] fl = new double[11]; // значение функции в точках полинома
        static double Fxi(double xi)
        {
            double epsilon = Math.Pow(10, -6);
            int i = 0;
            double ai = (2 / Math.Sqrt(Math.PI)) * xi; // а0
            double q = ((-1) * xi * xi) / 3; // q0
            double Fx = ai;
            double Si = 0;
            while (Math.Abs(Fx - Si) > epsilon)
            {
                i++;
                ai = ai * q; // поиск нового слагаемого
                q = ((-1) * xi * xi * (2 * i + 1)) / ((i + 1) * (2 * i + 3)); // нахождение qi
                Si = Fx;
                Fx += ai;
            }
            return Fx;
        }
        static void Polinom(int n)
        {
            double a = 0;
            double b = 2;
            double h = ((b - a) / 10);
            double z = 0, p1, p2, x;
            int y = 0;
            for (x = a; x <= b; x += h)
            {
                z = 0;
                for (int i = 0; i <= n; i++)
                {
                    p1 = 1; p2 = 1;
                    for (int j = 0; j <= n; j++)
                    {
                        if (i != j)
                        {
                            p1 = p1 * (x - Xif[j]);
                            p2 = p2 * (Xif[i] - Xif[j]);
                        }
                    }
                    {
                        z = z + f[i] * ((p1 * 0.1) / (p2 * 0.1));
                    }
                }
                Xil[y] = x;
                Ln[y] = z;
                fl[y] = Fxi(x);
                y++;
            }
        }
        static void Calculation(int n)
        {
            double epsilon = Math.Pow(10, -6);
            double a = 0;
            double b = 2;
            double h = (b - a) / n;
            h = h - h % 0.00000001;
            int j = 0;
            for (double x = a; x <= b; x += h)
            {
                int i = 0;
                double ai = (2 / Math.Sqrt(Math.PI)) * x; // а0
                double q = ((-1) * x * x) / 3; // q0
                double Fx = ai;
                double Si = 0;
                while (Math.Abs(Fx - Si) > epsilon)
                {
                    i++;
                    ai = ai * q; // поиск след слагаемого
                    q = ((-1) * x * x * (2 * i + 1)) / ((i + 1) * (2 * i + 3)); // нахождение qi
                    Si = Fx;
                    Fx += ai;
                }
                Xif[j] = x;
                f[j] = Fx;
                j++;
            }
        }
        static double Max()
        {
            double m = Math.Round(Math.Abs(Math.Round(fl[0], 13) - Math.Round(Ln[0], 13)), 13);
            for (int i = 1; i <= 10; i++)
            {
                double k = Math.Round(Math.Abs(Math.Round(fl[i], 13) - Math.Round(Ln[i], 13)), 13);
                if (k > m) m = k;
            }
            return m;
        }
        static void Main1(string[] args)
        {
            for (int i = 0; i < 70; i++)    //предварительно заполняем нулями
            {
                f[i] = 0;   
                Xif[i] = 0;
            }
            Calculation(n);
            Polinom(n);
            Console.WriteLine($" X F(x) Ln(x) Rn(x)");
            for (int i = 0; i <= 10; i++)
                Console.WriteLine($"{Xil[i],4} {Math.Round(fl[i], 13),18} {Math.Round(Ln[i], 13),18} {Math.Round(Math.Abs(Math.Round(fl[i], 13) - Math.Round(Ln[i], 13)), 13),18}");
            for (int i = 0; i <= n; i++)
                Console.WriteLine($"x= {Xif[i]}");
            Calculation(n + 4);
            Polinom(n + 4);
            Console.WriteLine($" X F(x) Ln(x) Rn(x)");
            for (int i = 0; i <= 10; i++)
                Console.WriteLine($"{Xil[i],4} {Math.Round(fl[i], 13),18} {Math.Round(Ln[i], 13),18} {Math.Round(Math.Abs(Math.Round(fl[i], 13) - Math.Round(Ln[i], 13)), 13),18}");
            for (int i = 0; i <= 9; i++)
                Console.WriteLine($"x= {Xif[i]}");
            Console.WriteLine($"Нахождение максимальных погрешностей");
            for (int i = 0; i <= 40; i++)
            {
                if ((n + i) % 10 != 0)
                {
                    Calculation(n + i);
                    Polinom(n + i);
                    double m = Max();
                    Console.WriteLine($"При n: {n + i} Максимальная Rn(x): {m}");
                }
            }
            Console.ReadKey();
        }
    }
}
