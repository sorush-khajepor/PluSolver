# PluSolver

PluSolver is a Compact PLU decomposition (factorization) solver of a system of linear equations, AX=B. 
It is compact as both Lower matrix and upper matrix are stored in A matrix. Moreover, Permutation matrix, P, is stored in a vector. 
In this way, all the memory could have taken by zeros in P, L and U is saved. 
