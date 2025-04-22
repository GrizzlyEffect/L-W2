using System;

namespace L2
{
    class Program
    {
        static double f(double x) => x + Math.Sin(x);
        static double df(double x) => 1 + Math.Cos(x);  //Похідна від х

        // Метод Ньютона
        static double Newton(double x0, double eps = 1e-6, int maxIter = 1000)
        {
            double x = x0, x_prev = x0;
            int iter;
            Console.WriteLine("Метод Ньютона:");
            for (iter = 0; iter < maxIter; iter++)
            {
                x = x_prev - f(x_prev) / df(x_prev);
                Console.WriteLine($"  Ітерація {iter + 1}: x = {x:F15}");
                if (Math.Abs(x - x_prev) <= eps)
                    break;
                x_prev = x;
            }
            Console.WriteLine($"  Завершено за {iter + 1} ітерацій. Корінь = {x:F30}\n");
            return x;
        }

        // Метод хорд
        static double Chord(double x0, double x1, double eps = 1e-6, int maxIter = 1000)
        {
            if (f(x0) * f(x1) >= 0)
                throw new ArgumentException("Функція не має різних знаків на інтервалі.");

            double x = x1, x_prev = x1;
            int iter;
            Console.WriteLine("Метод хорд:");
            for (iter = 0; iter < maxIter; iter++)
            {
                x = (x0 * f(x1) - x1 * f(x0)) / (f(x1) - f(x0));
                Console.WriteLine($"  Ітерація {iter + 1}: x = {x:F15}");
                if (Math.Abs(x - x_prev) <= eps)
                    break;

                if (f(x) * f(x0) < 0)
                    x1 = x;
                else
                    x0 = x;

                x_prev = x;
            }

            Console.WriteLine($"  Завершено за {iter + 1} ітерацій. Корінь = {x:F30}\n");
            return x;
        }

        static void Main()
        {
            double x0 = 1;    // початкове наближення
            double x1 = -2;   // друга точка для методу хорд

            double rootNewton = Newton(x0);
            double rootChord = Chord(x0, x1);

            Console.WriteLine($"Різниця між методами: {Math.Abs(rootChord - rootNewton):E}");
        }
    }
}
