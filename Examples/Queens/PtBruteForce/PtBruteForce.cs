//
// This program solves the N-Queens problem using a cartesian product.
// This method is not the most efficient but is not the worst either.
//
//   Queens  Solutions     Tries
//   ------  ---------  -----------
//      2           0             4
//      3           0            27
//      4           2           256
//      5          10          3125
//      6           4         46656
//      7          40        823543
//      8          92      16777216
//      9         352     387420489
//     10         724   10000000000
//
//

using System;
using System.Linq;
using Kaos.Combinatorics;

namespace ExampleApp
{
    class PtBruteForce
    {
        static bool IsNoThreat (Product row)
        {
            for (int qx1 = 0; qx1 < row.Width; ++qx1)
                for (int qx2 = qx1+1; qx2 < row.Width; ++qx2)
                    if (row[qx1] == row[qx2] || qx2 - qx1 - Math.Abs (row[qx2]-row[qx1]) == 0)
                        return false;
            return true;
        }

        static void Main()
        {
            // Number of queens:
            int nq = 8;

            Console.WriteLine ("Solve {0}-Queens with a cartesian product:\n", nq);

            long tries = 0;
            long solutions = 0;
            foreach (var layout in new Product (Enumerable.Repeat (nq,nq).ToArray()).GetRows())
            {
                ++tries;
                if (IsNoThreat (layout))
                {
                    Console.Write (String.Join (" ",
                        Enumerable.Range (0, layout.Width)
                        .Select (qx => ((char) ('a'+qx)).ToString() + (layout[qx]+1))));

                    if (++solutions % 100 == 0)
                        Console.Write ("  -  {0}", solutions);
                    Console.WriteLine();
                }
            }

            Console.WriteLine ("\nmethod=BruteForce, solutions={0}, tries={1}", solutions, tries);
        }
    }
}
