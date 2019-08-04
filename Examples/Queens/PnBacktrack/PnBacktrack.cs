//
// This program solves the N-Queens problem using permutations with backtracking.
// This is the most efficient method with the fewest board layouts tried.
//
//   Queens  Solutions    Tries
//   ------  ---------  ---------
//      2           0           2
//      3           0           6
//      4           2          18
//      5          10          58
//      6           4         208
//      7          40         834
//      8          92        3544
//      9         352       15970
//     10         724       75190
//     15     2279184   467414974
//

using System;
using System.Collections.Generic;
using System.Linq;
using Kaos.Combinatorics;

namespace ExampleApp
{
    class NQueens : Permutation
    {
        public static long Tries;

        public NQueens (int count) : base (count)
        { }

        // Returns -1 if no threat; else index of threatened.
        private int GetThreat()
        {
            for (int qx1 = 1; qx1 < Choices; ++qx1)
                for (int qx2 = 0; qx2 < qx1; ++qx2)
                    if (qx1 - qx2 - Math.Abs (this[qx2]-this[qx1]) == 0)
                        return qx1;
            return -1;
        }

        public IEnumerable<NQueens> GetSolutions()
        {
            Tries = 0;
            if (RowCount == 0)
                yield break;

            for (NQueens current = new NQueens (Choices);;)
            {
                ++Tries;
                int qx = current.GetThreat();
                if (qx < 0)
                {
                    yield return current;
                    if (++current.Rank == 0)
                        yield break;
                }
                else
                    // Prune all layouts that include this attack.
                    if (current.Backtrack (qx) < 0)
                        yield break;
            }
        }

        public override string ToString()
         => String.Join (" ", Enumerable.Range (0, Choices).Select (x => ((char) ('a'+x)).ToString()+(this[x]+1)));
    }

    class PnBacktrack
    {
        static void Main()
        {
            // Number of queens:
            int nq = 8;

            Console.WriteLine ($"Solve {nq}-Queens with backtracking permutations:\n");

            long solutions = 0;
            foreach (var layout in new NQueens (nq).GetSolutions())
            {
                Console.Write (layout);
                if (++solutions % 100 == 0)
                    Console.Write ($"  -  {solutions}");
                Console.WriteLine();
            }

            Console.WriteLine ($"\nmethod=Backtrack, solutions={solutions}, tries={NQueens.Tries}");
        }

        /* Output:

        Solve 8-Queens with backtracking permutations:

        a1 b5 c8 d6 e3 f7 g2 h4
        a1 b6 c8 d3 e7 f4 g2 h5
        a1 b7 c4 d6 e8 f2 g5 h3
        a1 b7 c5 d8 e2 f4 g6 h3
        a2 b4 c6 d8 e3 f1 g7 h5
        a2 b5 c7 d1 e3 f8 g6 h4
        a2 b5 c7 d4 e1 f8 g6 h3
        a2 b6 c1 d7 e4 f8 g3 h5
        a2 b6 c8 d3 e1 f4 g7 h5
        a2 b7 c3 d6 e8 f5 g1 h4
        a2 b7 c5 d8 e1 f4 g6 h3
        a2 b8 c6 d1 e3 f5 g7 h4
        a3 b1 c7 d5 e8 f2 g4 h6
        a3 b5 c2 d8 e1 f7 g4 h6
        a3 b5 c2 d8 e6 f4 g7 h1
        a3 b5 c7 d1 e4 f2 g8 h6
        a3 b5 c8 d4 e1 f7 g2 h6
        a3 b6 c2 d5 e8 f1 g7 h4
        a3 b6 c2 d7 e1 f4 g8 h5
        a3 b6 c2 d7 e5 f1 g8 h4
        a3 b6 c4 d1 e8 f5 g7 h2
        a3 b6 c4 d2 e8 f5 g7 h1
        a3 b6 c8 d1 e4 f7 g5 h2
        a3 b6 c8 d1 e5 f7 g2 h4
        a3 b6 c8 d2 e4 f1 g7 h5
        a3 b7 c2 d8 e5 f1 g4 h6
        a3 b7 c2 d8 e6 f4 g1 h5
        a3 b8 c4 d7 e1 f6 g2 h5
        a4 b1 c5 d8 e2 f7 g3 h6
        a4 b1 c5 d8 e6 f3 g7 h2
        a4 b2 c5 d8 e6 f1 g3 h7
        a4 b2 c7 d3 e6 f8 g1 h5
        a4 b2 c7 d3 e6 f8 g5 h1
        a4 b2 c7 d5 e1 f8 g6 h3
        a4 b2 c8 d5 e7 f1 g3 h6
        a4 b2 c8 d6 e1 f3 g5 h7
        a4 b6 c1 d5 e2 f8 g3 h7
        a4 b6 c8 d2 e7 f1 g3 h5
        a4 b6 c8 d3 e1 f7 g5 h2
        a4 b7 c1 d8 e5 f2 g6 h3
        a4 b7 c3 d8 e2 f5 g1 h6
        a4 b7 c5 d2 e6 f1 g3 h8
        a4 b7 c5 d3 e1 f6 g8 h2
        a4 b8 c1 d3 e6 f2 g7 h5
        a4 b8 c1 d5 e7 f2 g6 h3
        a4 b8 c5 d3 e1 f7 g2 h6
        a5 b1 c4 d6 e8 f2 g7 h3
        a5 b1 c8 d4 e2 f7 g3 h6
        a5 b1 c8 d6 e3 f7 g2 h4
        a5 b2 c4 d6 e8 f3 g1 h7
        a5 b2 c4 d7 e3 f8 g6 h1
        a5 b2 c6 d1 e7 f4 g8 h3
        a5 b2 c8 d1 e4 f7 g3 h6
        a5 b3 c1 d6 e8 f2 g4 h7
        a5 b3 c1 d7 e2 f8 g6 h4
        a5 b3 c8 d4 e7 f1 g6 h2
        a5 b7 c1 d3 e8 f6 g4 h2
        a5 b7 c1 d4 e2 f8 g6 h3
        a5 b7 c2 d4 e8 f1 g3 h6
        a5 b7 c2 d6 e3 f1 g4 h8
        a5 b7 c2 d6 e3 f1 g8 h4
        a5 b7 c4 d1 e3 f8 g6 h2
        a5 b8 c4 d1 e3 f6 g2 h7
        a5 b8 c4 d1 e7 f2 g6 h3
        a6 b1 c5 d2 e8 f3 g7 h4
        a6 b2 c7 d1 e3 f5 g8 h4
        a6 b2 c7 d1 e4 f8 g5 h3
        a6 b3 c1 d7 e5 f8 g2 h4
        a6 b3 c1 d8 e4 f2 g7 h5
        a6 b3 c1 d8 e5 f2 g4 h7
        a6 b3 c5 d7 e1 f4 g2 h8
        a6 b3 c5 d8 e1 f4 g2 h7
        a6 b3 c7 d2 e4 f8 g1 h5
        a6 b3 c7 d2 e8 f5 g1 h4
        a6 b3 c7 d4 e1 f8 g2 h5
        a6 b4 c1 d5 e8 f2 g7 h3
        a6 b4 c2 d8 e5 f7 g1 h3
        a6 b4 c7 d1 e3 f5 g2 h8
        a6 b4 c7 d1 e8 f2 g5 h3
        a6 b8 c2 d4 e1 f7 g5 h3
        a7 b1 c3 d8 e6 f4 g2 h5
        a7 b2 c4 d1 e8 f5 g3 h6
        a7 b2 c6 d3 e1 f4 g8 h5
        a7 b3 c1 d6 e8 f5 g2 h4
        a7 b3 c8 d2 e5 f1 g6 h4
        a7 b4 c2 d5 e8 f1 g3 h6
        a7 b4 c2 d8 e6 f1 g3 h5
        a7 b5 c3 d1 e6 f8 g2 h4
        a8 b2 c4 d1 e7 f5 g3 h6
        a8 b2 c5 d3 e1 f7 g4 h6
        a8 b3 c1 d6 e2 f5 g7 h4
        a8 b4 c1 d3 e6 f2 g7 h5

        method=Backtrack, solutions=92, tries=3544

        */
    }
}
