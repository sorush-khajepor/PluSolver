# PluSolver

[![Build status](https://dev.azure.com/sorushkh/BuildPluSolver/_apis/build/status/BuildPluSolver-CI)](https://dev.azure.com/sorushkh/BuildPluSolver/_build/latest?definitionId=5)


PluSolver is a Compact PLU decomposition (factorization) solver of a system of linear equations, AX=B. 
It is compact as both Lower matrix and upper matrix are stored in A matrix. Moreover, Permutation matrix, P, is stored in a vector. 
In this way, all the memory could have taken by zeros in P, L and U is saved. The code is written in C#, so it's suitable for dot net applications.

## Background

A system of linear equations is defined as 

A X = B 

where A is the coefficient matrix, X is the unknown matrix, and B is the constants matrix. This system can be solved using LU decomposition method. Matrix A can be factorized as 

A = L U 

where L is lower matrix with all elements above diagonal zero and U is upper matrix with all elements under diagoanl zero. This way the sytem can be solved faster because we have

L U X = B

we can first solve

L Y = B

and then solve

U X = Y

to find X. But what is P? in the first step of decomposition A = LU, most of the time we have to joggle lines to make sure diagoanls are not zero. We record the final order of rows in P, permutation, matrix. Then we can apply that to B before solving LY=B.  

## Usage

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



