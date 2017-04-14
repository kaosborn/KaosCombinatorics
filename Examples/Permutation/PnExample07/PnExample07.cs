using System;
using Kaos.Combinatorics;

namespace ExampleApp
{
    static class PnExample07
    {
        static void Main()
        {
            var pn = new Permutation (4);

            Console.WriteLine ("Plain changes, n={0}:\n", pn.Choices);

            foreach (var row in pn.GetRowsOfPlainChanges())
                Console.WriteLine ("{0,2}: {1}  Swaps={2}, Rank={3}", row.PlainRank, row, row.Swaps, row.Rank);
        }

        /* Output:

        Plain changes, n=4:

         0: { 0, 1, 2, 3 }  Swaps=0, Rank=0
         1: { 0, 1, 3, 2 }  Swaps=1, Rank=1
         2: { 0, 3, 1, 2 }  Swaps=2, Rank=4
         3: { 3, 0, 1, 2 }  Swaps=3, Rank=18
         4: { 3, 0, 2, 1 }  Swaps=2, Rank=19
         5: { 0, 3, 2, 1 }  Swaps=1, Rank=5
         6: { 0, 2, 3, 1 }  Swaps=2, Rank=3
         7: { 0, 2, 1, 3 }  Swaps=1, Rank=2
         8: { 2, 0, 1, 3 }  Swaps=2, Rank=12
         9: { 2, 0, 3, 1 }  Swaps=3, Rank=13
        10: { 2, 3, 0, 1 }  Swaps=2, Rank=16
        11: { 3, 2, 0, 1 }  Swaps=3, Rank=22
        12: { 3, 2, 1, 0 }  Swaps=2, Rank=23
        13: { 2, 3, 1, 0 }  Swaps=3, Rank=17
        14: { 2, 1, 3, 0 }  Swaps=2, Rank=15
        15: { 2, 1, 0, 3 }  Swaps=1, Rank=14
        16: { 1, 2, 0, 3 }  Swaps=2, Rank=8
        17: { 1, 2, 3, 0 }  Swaps=3, Rank=9
        18: { 1, 3, 2, 0 }  Swaps=2, Rank=11
        19: { 3, 1, 2, 0 }  Swaps=1, Rank=21
        20: { 3, 1, 0, 2 }  Swaps=2, Rank=20
        21: { 1, 3, 0, 2 }  Swaps=3, Rank=10
        22: { 1, 0, 3, 2 }  Swaps=2, Rank=7
        23: { 1, 0, 2, 3 }  Swaps=1, Rank=6

        */
    }
}
