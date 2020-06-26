# PluSolver

[![Build status](https://dev.azure.com/sorushkh/BuildPluSolver/_apis/build/status/BuildPluSolver-CI)](https://dev.azure.com/sorushkh/BuildPluSolver/_build/latest?definitionId=5)


PluSolver is a Compact PLU or LU decomposition (factorization) solver of a system of linear equations, AX=B. 
It is compact as both Lower matrix and upper matrix are stored in A matrix. Moreover, Permutation matrix, P, is stored in a vector. 
In this way, all the memory could have taken by zeros in P, L and U is saved. The code is written in C#, so it's suitable for dot net applications.
 

## Usage

Inject A and B in an instance of `Solver` then get the result from `Solver.SolveX()` method. 

* If there is no answer to the system, an exception is raised which you can catch.
* As the method is compact, A matrix is modified inside SolveX() method. So, if you want to keep A, inject a
clone of it.

```c#
// Inside a method or main program
double[,] A = new double[,] 
{
  { 1,  2},
  { 3,  1}
};

var B = new double[] { 5, 5 };

var solver = new Solver(A, B);
double[] X = null;

try
{
  X = solver.SolveX();
}
catch (Exception ex);
{
  Console.Write(ex.Message());
}

// rest of the code
```



