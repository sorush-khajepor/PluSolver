using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PluSolver;


namespace PluSolverTests
{
    [TestClass]
    public class SolverTests
    {
        private bool IsEqual(double a, double b)
        {
            return (Math.Abs(a - b) < 1e-10) ;
        }

        [TestMethod]
        public void Test_2Unknows()
        {
            double[,] A = new double[,] {
                { 1,  2},
                { 3,  1}
            };
            var B = new double[] { 5, 5 };

            var solver = new Solver(A, B);
            double[] X=null;

            try
            {
                X = solver.SolveX();
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
            

            Assert.IsTrue(IsEqual(X[0], 1) && IsEqual(X[1], 2));

        }
    }
}
