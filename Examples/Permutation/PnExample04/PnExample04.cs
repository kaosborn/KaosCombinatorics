using System;
using Kaos.Combinatorics;

namespace ExampleApp
{
    class PnExample04
    {
        static void Main()
        {
            // Create a permutation of the supplied rank:
            var pn = new Permutation (choices:8, picks:8, rank:119);
            Console.WriteLine ($"{pn}  rank={pn.Rank}\n");

            // Assign -1 to get the last rank:
            pn.Rank = -1;
            Console.WriteLine ($"{pn}  last={pn.Rank}\n");

            // Rank will always stay in bounds:
            pn.Rank = pn.Rank + 1;
            Console.WriteLine ($"{pn}  rank={pn.Rank}\n");

            // Create a permutation from the supplied sequence:
            pn = new Permutation (new int[] { 6, 4, 2, 0, 7, 5, 3, 1 });
            Console.WriteLine ($"{pn}  rank={pn.Rank}");
        }

        /* Output:

        { 0, 1, 2, 7, 6, 5, 4, 3 }  rank=119

        { 7, 6, 5, 4, 3, 2, 1, 0 }  last=40319

        { 0, 1, 2, 3, 4, 5, 6, 7 }  rank=0

        { 6, 4, 2, 0, 7, 5, 3, 1 }  rank=33383

        */
    }
}
