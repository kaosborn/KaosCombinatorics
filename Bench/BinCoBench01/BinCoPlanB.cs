using System;
using System.Collections.Generic;

namespace BenchApp
{
    partial class BinCoBench01
    {
        //
        // Binomial coefficient plan B: Multiplicative formula
        //
        // Advantages:   Greater range before overflow than plan A, smallest footprint
        // Disadvantage: slower than plan A
        //
        static public long BinomialCoefficientPlanB (int n, int k)
        {
            if (k < 0 || k > n)
                return 0;

            if (k > n - k)
                k = n - k;

            int k2 = n - k;
            long bc = 1;

            for (int ki = 1; ki <= k; ++ki)
            {
                int factor = k2 + ki;
                bc = checked (bc * factor) / ki;
            }

            return bc;
        }
    }
}
