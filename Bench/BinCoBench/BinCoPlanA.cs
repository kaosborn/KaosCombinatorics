//using System;
//using System.Collections.Generic;

namespace BenchApp
{
    partial class BinCoBench
    {
        //
        // Binomial coefficient plan A: Factorial formula with common factors cancelled
        //
        // Advantage:    Faster than plan B
        // Disadvantage: Not as many full rows as plan B
        //
        static public long BinomialCoefficientPlanA (int n, int k)
        {
            if (k < 0 || k > n)
                return 0;

            if (k < n - k)
                k = n - k;

            // Formula is n!/(k!(n-k)!)
            // When n>20, n! overflows so use this technique:

            int k2 = n - k;
            int den = k2;

            long v1 = 1;
            for (int ki = k + 1; ki <= n; ++ki)
            {
                if (ki > k2 + k2 || (ki & 1) != 0)
                    v1 = checked (v1 * ki);
                else
                {
                    v1 = checked (v1 + v1);
                    --den;
                }
            }

            return v1 / factorial[den];
        }

        static public readonly long[] factorial = new long[]
        {
            1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800, 39916800, 
            479001600, 6227020800, 87178291200, 1307674368000, 20922789888000,
            355687428096000, 6402373705728000, 121645100408832000, 2432902008176640000
        };
    }
}
