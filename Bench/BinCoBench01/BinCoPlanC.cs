using System;
using System.Collections.Generic;

namespace BenchApp
{
    partial class BinCoBench01
    {
        //
        // Binomial coefficient plan C: Stored table with fallback to plan B
        //
        // Advantage:    Faster than either plan A or B (for precalculated rows)
        // Disadvantage: Allocates 18K on first use
        //
        static public long BinomialCoefficientPlanC (int n, int k)
        {
            if (k < 0 || k > n)
                return 0;

            if (pastri == null)
                pastri = BuildPascalsTriangle();

            if (n < pastri.Count)
                return pastri[n][k];

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

        static private List<long[]> pastri = null;

        // Returns as many complete rows of Pascal's triangle as possible.
        static private List<long[]> BuildPascalsTriangle()
        {
            var result = new List<long[]>();

            result.Add (new long[] { 1 });
            try
            {
                for (int n = 1; ; ++n)
                {
                    var row = new long[n+1];
                    row[0] = 1;
                    for (int k = 1; k <= n-1; ++k)
                        row[k] = checked (result[n-1][k-1] + result[n-1][k]);
                    row[n] = 1;
                    result.Add (row);
                }
            }
            catch (OverflowException)
            { }

            return result;
        }
    }
}
