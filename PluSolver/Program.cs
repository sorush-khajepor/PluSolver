using System;

namespace PluSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] A = new double[,] {
                { 1,  2,  3,  4,  5},
                { 11, 9, 13, 14, 15},
                { 21, 22, 16, 24, 25},
                { 31, 32, 33, 25, 35},
                { 41, 42, 43, 44, 36}
            };
            var B = new double[] { 10, 20, 30, 40, 50 };

            var s = new Solver(A, B);
            var X = s.SolveX();

            var analytical = new double[] { -4970.0 / 2383.0 ,1890.0 / 2383, 1620.0 / 2383, 1890.0 / 2383, 2520.0 / 2383};
        }
    }

    


    

    
}
