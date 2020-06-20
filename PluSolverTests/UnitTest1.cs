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

        [TestMethod]
        public void Test_3Unknows()
        {
            double[,] A = new double[,] {
                { 1,  1, 1},
                { 1,  2, 3},
                { 2,  1, 2}
            };
            var B = new double[] { 6, 14, 10 };

            var solver = new Solver(A, B);
            double[] X = null;

            try
            {
                X = solver.SolveX();
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }


            Assert.IsTrue(IsEqual(X[0], 1) &&
                          IsEqual(X[1], 2) &&
                          IsEqual(X[2],3));

        }

        [TestMethod]
        public void Test_4Unknows()
        {
            double[,] A = new double[,] {
                { 1,  2, 1, 2},
                { 3,  2, 1, 2},
                { 2,  1, 3, 1},
                { 1,  1, 1, 2}
            };
            var B = new double[] {16, 18, 17, 14};

            var solver = new Solver(A, B);
            double[] X = null;

            try
            {
                X = solver.SolveX();
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }


            Assert.IsTrue(IsEqual(X[0], 1) &&
                          IsEqual(X[1], 2) &&
                          IsEqual(X[2], 3) &&
                          IsEqual(X[3], 4));

        }

        [TestMethod]
        public void Test_5Unknows()
        {
            double[,] A = new double[,] {
                { 1,  2,  3,  4,  5},
                { 11, 9, 13, 14, 15},
                { 21, 22, 16, 24, 25},
                { 31, 32, 33, 25, 35},
                { 41, 42, 43, 44, 36}
            };
            var B = new double[] {55, 199, 334, 469, 610};

            var solver = new Solver(A, B);
            double[] X = null;

            try
            {
                X = solver.SolveX();
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }


            Assert.IsTrue(IsEqual(X[0], 1) &&
                          IsEqual(X[1], 2) &&
                          IsEqual(X[2], 3) &&
                          IsEqual(X[3], 4) &&
                          IsEqual(X[4], 5));

        }

        [TestMethod]
        public void Test_SingularMatrixSimilarRows_NoAnswer()
        {
            double[,] A = new double[,] {
                { 1,  1, 1},
                { 1,  1, 1},
                { 2,  1, 2}
            };
            var B = new double[] { 6, 14, 10 };

            var solver = new Solver(A, B);
            double[] X = null;

            try
            {
                X = solver.SolveX();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(true) ;
            }            

        }

        [TestMethod]
        public void Test_SingularMatrixOneColumnZero_NoAnswer()
        {
            double[,] A = new double[,] {
                { 1,  1, 0},
                { 3,  5, 0},
                { 2,  1, 0}
            };
            var B = new double[] { 6, 14, 10 };

            var solver = new Solver(A, B);
            double[] X = null;

            try
            {
                X = solver.SolveX();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(true);
            }

        }


    }
}
