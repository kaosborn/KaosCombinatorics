﻿//
// This program solves the N-Queens problem using pick-combinations.
// This brute-force method is so inefficient that it's ludicrous.
//
//   Queens  Solutions     Tries
//   ------  ---------  -----------
//      2           0             6
//      3           0            84
//      4           2          1820
//      5          10         53130
//      6           4       1947792
//      7          40      85900584
//      8          92    4426165368 
//      9         352  260887834350 *
//     10                  overflow
//
//     * = untested
//

using System;
using System.Collections.Generic;
using Kaos.Combinatorics;

namespace ExampleApp
{
    class NQueens : Combination
    {
        public static long Tries;

        public NQueens (int count) : base (count*count, count)
        { }

        bool IsNoThreat()
        {
            for (int pi1 = 0; pi1 < Picks; ++pi1)
            {
                int qx1 = Math.DivRem (this[pi1], Picks, out int qy1);

                for (int pi2 = pi1+1; pi2 < Picks; ++pi2)
                {
                    int qx2 = Math.DivRem (this[pi2], Picks, out int qy2);
                    int dx = qx2 - qx1,
                        dy = qy2 - qy1;

                    if (dx == 0 || dy == 0 || Math.Abs (dx) - Math.Abs (dy) == 0)
                        return false;
                }
            }
            return true;
        }

        public IEnumerable<NQueens> GetSolutions()
        {
            foreach (NQueens current in GetRows())
            {
                ++Tries;
                if (IsNoThreat())
                    yield return current;
            }
        }

        public override string ToString()
        {
            string result = "";
            for (int pi = 0; pi < Picks; ++pi)
            {
                int qx = Math.DivRem (this[pi], Picks, out int qy);
                result += ((char) ('a'+qx)).ToString() + (qy+1) + " ";
            }
            return result;
        }
    }


    class CnLudicrous
    {
        static void Main()
        {
            // Number of queens:
            int nq = 8;

            Console.WriteLine ($"Solve {nq}-Queens with pick-combinations:\n");

            long solutions = 0;
            foreach (var layout in new NQueens (nq).GetSolutions())
            {
                Console.Write (layout);
                if (++solutions % 100 == 0)
                    Console.Write ($" -  {solutions}");
                Console.WriteLine();
            }

            Console.WriteLine ($"\nmethod=Ludicrous, solutions={solutions}, tries={NQueens.Tries}");
        }
    }
}
