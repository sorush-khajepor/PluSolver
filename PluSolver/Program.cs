using System;

namespace PluSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] data = new double[,] {
                { 1,  2,  3,  4,  5},
                { 11, 9, 13, 14, 15},
                { 21, 22, 16, 24, 25},
                { 31, 32, 33, 25, 35},
                { 41, 42, 43, 44, 36}
            };
            var decomposer = new PLUDecomposer(data);
            (var p, var lu) = decomposer.FindPAndCombinedLU();

            var B = new double[] { 10, 20, 30, 40, 50 };
            var s = new Solver(lu, p, B);
            var X = s.Solve();

            var analytical = new double[] { -4970.0 / 2383.0 ,1890.0 / 2383, 1620.0 / 2383, 1890.0 / 2383, 2520.0 / 2383};

            

        }
    }

    public class SubMatrixBoundaries
    {
        public int StartColumn;
        public int EndColumn;
        public int StartRow;
        public int EndRow;
    }


    public class Solver
    {
        private double[,] lu;
        private double[] b;
        private double[] y;
        private double[] x;
        private readonly int[] p;


        private readonly int numberOfRows;
        private readonly int numberOfColumns;

        public Solver(double[,] lu, int[] p, double[] b)
        {
            this.lu = lu;
            this.p = p;
            this.b = b;

            numberOfRows = lu.GetLength(0);
            numberOfColumns = lu.GetLength(1);

            y = new double[numberOfRows];
            x = new double[numberOfRows];


        }

        private void ReorderB()
        {
            double[] reorderedB = new double[b.Length];

            for (int row = 0; row < numberOfRows; row++)
            {
                reorderedB[row] = b[p[row]];
            }

            for (int row = 0; row < numberOfRows; row++)
            {
                b[row] = reorderedB[row];
            }
        }

        void SolveYfromLYequalB()
        {
            
            for (int row = 0; row < b.Length; row++)
            {
                double sum = 0;
                var columnOfDiagonal = row;
                for (int column = 0; column < columnOfDiagonal; column++)
                {
                    sum += y[column] * lu[row,column];
                }
                y[row] = b[row] - sum;
            } 
        }

        void SolveXfromUXequalY()
        {
            for (int row = y.Length-1; row > -1; row--)
            {
                double sum = 0;
                var columnOfDiagonal = row;

                for (int column = columnOfDiagonal + 1; column < x.Length; column++)
                {
                    sum += x[column] * lu[row, column];
                }
                x[row] = (y[row] - sum)/lu[row,row];
            }
        }

        public double[] Solve()
        {
            ReorderB();
            SolveYfromLYequalB();
            SolveXfromUXequalY();
            return x;
        }
        

    }

    public class PLUDecomposer{

        private double[,] A;

        private SubMatrixBoundaries LowerTriangleBounds;
        private int numberOfColumns;
        private int numberOfRows;

        public int[] P { get; private set; }

        public double[,] GetData() { return A; }

        public PLUDecomposer(double[,] A)
        {
            this.A = A;

            numberOfRows = A.GetLength(0);
            numberOfColumns = A.GetLength(1);

            InitializePWithRowNumbers();

            LowerTriangleBounds = new SubMatrixBoundaries()
            {
                StartColumn = 0,
                EndColumn = A.GetLength(1) - 1,
                StartRow = 1,
                EndRow = A.GetLength(0)

            };
        }

        private void InitializePWithRowNumbers()
        {
            P = new int[numberOfRows];
            for (int row = 0; row < numberOfRows; row++)
            {
                P[row] = row;
            }
        }

        private double[,] CreateIdentityMatrix(int size)
        {
            var identity = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                identity[i, i] = 1;
            }
            return identity;
        }

        public (int[],double[,]) FindPAndCombinedLU()
        {
            ConvertAtoLU();
            return (P,A);
        }

        private void ConvertAtoLU()
        {
            MakeLowerTriangleZeroAndFillWithL();
        }

        private void MakeLowerTriangleZeroAndFillWithL()
        {
            for (int column = LowerTriangleBounds.StartColumn; column < LowerTriangleBounds.EndColumn; column++)
            {
                SwapMaxRowWithDiagonal(column);
                MakeColumnZero(column);
            }
        }

        private void SwapMaxRowWithDiagonal(int focusedColumn)
        {

            var rowOfDiagonal = focusedColumn;
            var rowOfMaxElement = GetRowOfMaxElementUnderDiagonal(focusedColumn);

            if (rowOfMaxElement!= rowOfDiagonal)
            {
                SwapRows(rowOfMaxElement, rowOfDiagonal);
                
            };

        }

        private int GetRowOfMaxElementUnderDiagonal(int focusedColumn)
        {
            var rowOfDiagonal = focusedColumn;
            var columnOfDiagonal = focusedColumn;
            var rowAfterDiagonal = rowOfDiagonal + 1;


            double maxElement = A[rowOfDiagonal, columnOfDiagonal];
            int rowOfMaxElement = rowOfDiagonal;
            

            for (int row = rowAfterDiagonal; row < numberOfRows; row++)
            {
                if (A[row, focusedColumn] > maxElement)
                {
                    maxElement = A[row, focusedColumn];
                    rowOfMaxElement = row;
                }
            }
            return rowOfMaxElement;
        }

        private void SwapRows(int row1, int row2)
        {
            for (int column = 0; column < numberOfColumns; column++)
            {
                var tmp = A[row1, column];
                A[row1, column] = A[row2, column];
                A[row2, column] = tmp;
            }
            RecordSwap(row1, row2);
        }

        private void RecordSwap(int row1, int row2)
        {
            var tmp = P[row1];
            P[row1] = P[row2];
            P[row2] = tmp;
        }

        private void MakeColumnZero(int column)
        {
            int rowUnderDiagonalElement = column + 1;
            for (int row = rowUnderDiagonalElement; row < LowerTriangleBounds.EndRow; row++)
            {
                MakeElementZeroAndFillWithLowerMatrixElement(row, column);
            }
        }

        private void MakeElementZeroAndFillWithLowerMatrixElement(int elementRow, int elementColumn)
        {
            var element = A[elementRow, elementColumn];
            var sameColumnDiagonalElement = A[elementColumn, elementColumn]; 

            var rowMultiplier = - element / sameColumnDiagonalElement;

            for (int col = elementColumn; col < numberOfColumns; col++)
            {
                A[elementRow, col] += rowMultiplier * A[elementColumn, col];
            }

            A[elementRow, elementColumn] = -rowMultiplier;
        }

        
    }
}
