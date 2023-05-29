using System;
using System.Security.Cryptography.X509Certificates;

namespace Newt
{
    public class Newton
    {
        public double[] Newts(double leftik, double rightik, double aik, double bik, double cik)
        {
            double a = aik, b = bik, c = cik, d = -70, e = -10, f = 25, Accuracy1 = 0.0005, Accuracy2 = 0.005, left = leftik, right = rightik, M = 10;
            double[] leftk = new double[50];
            double[] rightk = new double[50];

            leftk[0] = left;
            rightk[0] = right;
            double func1;
            double func;
            int k;
            int flag = 0;
            double[] xk = new double[50]; //xk - gradient
            double[] tk = new double[50];
            tk[0] = 1;
            double[] dkleft = new double[50];
            double[] dkright = new double[50];
            double[] gradleft = new double[50];
            double[] gradright = new double[50];
            double[] container = new double[3];


            for (k = 0; k <= M; k++, tk[k] = tk[k - 1])
            {
                Console.WriteLine($"Итерация {k}");
                xk[k] = (Math.Sqrt(Math.Pow((2 * a * leftk[k] + c * rightk[k] + d), 2) + Math.Pow((b * 2 * rightk[k] + c * leftk[k] + e), 2)));
                gradleft[k] = 2 * a * leftk[k] + c * rightk[k] + d;
                gradright[k] = b * 2 * rightk[k] + c * leftk[k] + e;

                if (Math.Abs(xk[k]) <= Accuracy1)
                {
                    Console.WriteLine($"Gradient f(x({k}) = {xk[k]} < {Accuracy1}");
                    func = (a * Math.Pow(leftk[k], 2) + c * rightk[k] * rightk[k] + b * leftk[k] * rightk[k] + d * leftk[k] + e * rightk[k] + f);
                    Console.WriteLine($"Количество итераций - {k + 1}, X* искомое - ({leftk[k]}; {rightk[k]}), f(x) = ({Math.Round(func, 4)})");
                    container[0] = leftk[k]; container[1] = rightk[k]; container[2] = func;
                    return container;
                }
                Console.WriteLine($"Gradient f(x({k}) = {xk[k]} > {Accuracy1}");
                if (k >= M)
                {
                    func = (a * Math.Pow(leftk[k], 2) + c * rightk[k] * rightk[k] + b * leftk[k] * rightk[k] + d * leftk[k] + e * rightk[k] + f);
                    Console.WriteLine($"Количество итераций - {k + 1}, X* искомое - ({leftk[k]}; {rightk[k]}), f(x) = ({Math.Round(func, 4)})");
                    container[0] = leftk[k]; container[1] = rightk[k]; container[2] = func;
                    return container;
                }

                double a2 = 2 * a;
                double c2 = 2 * b;
                double notH = (1 / (a2 * c2 - c * c)) * (a2) * (1 / (a2 * c2 - c * c)) * (c2) - (1 / (a2 * c2 - c * c)) * (-c) * (1 / (a2 * c2 - c * c)) * (-c); //определитель обратной матрицы Гёссе
                Console.WriteLine($"определитель обратной матрицы Гёссе - notH({k}) = {notH}");

                if (notH > 0)
                {
                    dkleft[k] = -gradleft[k] * (((1 / (a2 * c2 - (c * c))) * c2) + ((1 / (a2 * c2 - (c * c))) * (-c)));
                    Console.WriteLine(dkleft[k]);
                    dkright[k] = -gradright[k] * (((1 / (a2 * c2 - (c * c))) * a2) + ((1 / (a2 * c2 - (c * c))) * (-c)));
                    Console.WriteLine(dkright[k]);
                    tk[k] = 1;

                    leftk[k + 1] = leftk[k] + tk[k] * dkleft[k];
                    rightk[k + 1] = rightk[k] + tk[k] * dkright[k];
                    func = (a * Math.Pow(leftk[k], 2) + c * rightk[k] * rightk[k] + b * leftk[k] * rightk[k] + d * leftk[k] + e * rightk[k] + f);
                    func1 = (a * Math.Pow(leftk[k + 1], 2) + c * rightk[k + 1] * rightk[k + 1] + b * leftk[k + 1] * rightk[k + 1] + d * leftk[k + 1] + e * rightk[k + 1] + f);

                    double check = (Math.Abs(Math.Sqrt(Math.Pow((leftk[k + 1] - leftk[k]), 2) + Math.Pow((rightk[k + 1] - rightk[k]), 2))));
                    double check1 = (Math.Abs((func1 - func)));

                    Console.WriteLine($"t({k}) = {tk[k]}");
                    Console.WriteLine($"dk({k}) = [{dkleft[k]};{dkright[k]}]");


                    if ((check < Accuracy2) && (Math.Abs((func1 - func)) < Accuracy2))
                    {
                        Console.WriteLine($"|f({k + 1}) - f({k})| = {Math.Abs(func1 - func)} < {Accuracy2} и |x({k + 1})-x({k})| = {check} < {Accuracy2}");
                        if (flag == 1)
                        {
                            Console.WriteLine($"Количество итераций - {k + 1}, X* искомое - ({Math.Round(leftk[k + 1], 4)}, {Math.Round(rightk[k + 1], 4)}), f(x) = ({Math.Round(func1, 4)})");
                            container[0] = leftk[k + 1]; container[1] = rightk[k + 1]; container[2] = func1;
                            return container;
                        }
                        flag++;
                    }
                    if (flag < 1)
                    {
                        Console.WriteLine($"|f({k + 1}) - f({k})| = {Math.Abs(func1 - func)}; |x({k + 1})-x({k})| = {check}");
                    }
                    Console.WriteLine($"X*({k}) - ({Math.Round(leftk[k + 1], 4)}, {Math.Round(rightk[k + 1], 4)}), f(x) = ({Math.Round(func1, 4)})");
                    Console.WriteLine("\n\n");
                }
                else
                {
                    dkleft[k] = -gradleft[k];
                    dkright[k] = -gradright[k];
                stepp7:

                    leftk[k + 1] = leftk[k] + tk[k] * dkleft[k];
                    rightk[k + 1] = rightk[k] + tk[k] * dkright[k];

                    func = (a * Math.Pow(leftk[k], 2) + c * rightk[k] * rightk[k] + b * leftk[k] * rightk[k] + d * leftk[k] + e * rightk[k] + f);
                    func1 = (a * Math.Pow(leftk[k + 1], 2) + c * rightk[k + 1] * rightk[k + 1] + b * leftk[k + 1] * rightk[k + 1] + d * leftk[k + 1] + e * rightk[k + 1] + f);


                    if (func1 < func)
                    {
                        Console.WriteLine($"f({k + 1}) - f({k}) = {func1 - func} > 0");
                        Console.WriteLine($"t({k}) = {tk[k]}");
                        tk[k] = tk[k] / 2;
                        goto stepp7;
                    }

                    double check = (Math.Abs(Math.Sqrt(Math.Pow((leftk[k + 1] - leftk[k]), 2) + Math.Pow((rightk[k + 1] - rightk[k]), 2))));
                    double check1 = (Math.Abs((func1 - func)));
                    Console.WriteLine($"t({k}) = {tk[k]}");
                    Console.WriteLine($"dk({k}) = [{dkleft[k]};{dkright[k]}]");
                    //Console.WriteLine($"x({k}) = {Math.Pow(leftk[k], 2) - Math.Pow(rightk[k], 2)}");

                    if ((check < Accuracy2) && (Math.Abs((func1 - func)) < Accuracy2))
                    {
                        Console.WriteLine($"|f({k + 1}) - f({k})| = {Math.Abs(func1 - func)} < {Accuracy2} и |x({k + 1})-x({k})| = {check} < {Accuracy2}");
                        if (flag == 1)
                        {
                            Console.WriteLine($"Количество итераций - {k + 1}, X* искомое - ({Math.Round(leftk[k + 1], 4)}, {Math.Round(rightk[k + 1], 4)}), f(x) = ({Math.Round(func1, 4)})");
                            container[0] = leftk[k + 1]; container[1] = rightk[k + 1]; container[2] = func1;
                            return container;
                        }
                        flag++;
                    }
                    if (flag < 1)
                    {
                        Console.WriteLine($"|f({k + 1}) - f({k})| = {Math.Abs(func1 - func)}; |x({k + 1})-x({k})| = {check}");
                    }
                    Console.WriteLine($"X*({k}) - ({Math.Round(leftk[k + 1], 4)}, {Math.Round(rightk[k + 1], 4)}), f(x) = ({Math.Round(func1, 4)})");
                    Console.WriteLine("\n\n");
                }
            }
            throw new Exception();
        }
    }
}