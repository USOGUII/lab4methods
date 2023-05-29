using System;
using System.Numerics;
using Newt;

class Findme
{
    static void Main()
    {
        //Console.WriteLine("Ввод целевой функции:");
        //Console.WriteLine("Введите коэфициент перед x(1)^2:");
        //double a = double.Parse(Console.ReadLine());
        //Console.WriteLine("Введите коэфициент перед x(2)^2:");
        //double b = double.Parse(Console.ReadLine());
        //Console.WriteLine("Введите значение свободного члена:");
        //double c = double.Parse(Console.ReadLine());
        //Console.WriteLine("Введите значение Epsilen:");
        //double Eps = double.Parse(Console.ReadLine());
        //Console.WriteLine("Введите значение r:");
        //double r = double.Parse(Console.ReadLine());
        //Console.WriteLine("Введите значение C:");
        //double C2 = double.Parse(Console.ReadLine());
        //Console.WriteLine("Ввод g1(x):");
        //Console.WriteLine("Введите коэфициент перед x(1):");
        //double a1 = double.Parse(Console.ReadLine());
        //Console.WriteLine("Введите коэфициент перед x(2):");
        //double b1 = double.Parse(Console.ReadLine());
        //Console.WriteLine("Введите значение свободного члена:");
        //double c1 = double.Parse(Console.ReadLine());
        double a = 8; double b = 1; double c = -5; double eps = 0.05; double r = 0.5; double C2 = 8; double a1 = 7; double b1 = 1; double c1 = -11;
        double[] x1 = new double[500];
        double[] x2 = new double[500];
        x1[0] = -3; x2[0] = 4;

        double[] rk = new double[500];
        rk[0] = r;
        double[] P = new double[3];
        int kmember = 0;
        double Func = 0;

        for (int k = 0; P[2] <= eps; k++, rk[k] = C2 * rk[k-1], x1[k] = P[0], x2[k] = P[1], kmember = k, Func = P[2])
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Итерация {k}");
            if (k != 0)
            {
                Console.WriteLine($"r[k] = {rk[kmember]}, x1[k] = {x1[kmember]}, x2[k] = {x2[kmember]}, f(x*) = {Func}");
            }
            else
            {
                Console.WriteLine($"r[k] = {rk[kmember]}, x1[k] = {x1[kmember]}, x2[k] = {x2[kmember]}");
            }
            //double func = a * (x1[k] * x1[k]) + b * (x2[k] * x2[k]) + (rk[k] / 2) * (a1 * x1[k] + b1 * x2[k] - c1);
            Console.BackgroundColor = ConsoleColor.Black;
            double newa = a + (rk[k]/2)*(a1 * a1);
            double newb = b + (rk[k]/2)*(b1 * b1);
            double newc = (rk[k]/2)*(a1 * b1);
            Newton Pk = new();
            P = Pk.Newts(x1[k], x2[k], newa, newc, newb);
        }
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.WriteLine($"r[k] = {rk[kmember]}, Minimum -  (x1[k] = {x1[kmember]}, x2[k] = {x2[kmember]}), f(x*) = {Func}, количество итераций - {kmember}");
    }
}