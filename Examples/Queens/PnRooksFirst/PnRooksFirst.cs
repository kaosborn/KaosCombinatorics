//
// This program solves the N-Queens problem using permutations without backtracking.
// While better than the brute-force methods, it is not the most efficient.
//
//   Queens  Solutions     Tries
//   ------  ---------  -----------
//      2           0             2
//      3           0             6
//      4           2            24
//      5          10           120
//      6           4           720
//      7          40          5040
//      8          92         40320
//      9         352        362880
//     10         724       3628800
//     15     2279184 1307674368000 *
//     * = untested
//

using System;
using Kaos.Combinatorics;

namespace ExampleApp
{
    class PnRooksFirst
    {
        static bool IsNoThreat (Permutation row)
        {
            for (int qx1 = 0; qx1 < row.Choices; ++qx1)
                for (int qx2 = qx1+1; qx2 < row.Choices; ++qx2)
                    if (qx2 - qx1 - Math.Abs (row[qx2]-row[qx1]) == 0)
                        return false;
            return true;
        }

        static void Main()
        {
            // Number of queens:
            int nq = 8;

            Console.WriteLine ($"Solve {nq}-Queens with non-backtracking permutations:\n");

            long tries = 0;
            long solutions = 0;
            foreach (var layout in new Permutation (nq).GetRows())
            {
                ++tries;
                if (IsNoThreat (layout))
                {
                    for (int i = 0; i < layout.Choices; ++i)
                        Console.Write (((char) ('a'+i)).ToString() + (layout[i]+1) + " ");
                    if (++solutions % 100 == 0)
                        Console.Write ($" -  {solutions}");
                    Console.WriteLine();
                }
            }

            Console.WriteLine ($"\nmethod=RooksFirst, solutions={solutions}, tries={tries}");
        }
    }
}
