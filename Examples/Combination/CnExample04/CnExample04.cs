using System;
using Kaos.Combinatorics;

namespace ExampleApp
{
    static class CnExample04
    {
        static void Main()
        {
            // Create a k-combination of the supplied rank:

            var cn = new Combination (choices:6, picks:4, rank:2);
            Console.WriteLine ("{0}  n={1}, k={2}, rank={3}\n", cn, cn.Choices, cn.Picks, cn.Rank);

            // Assign -1 to get the last rank, treat row like a string:

            cn.Rank = -1;
            string text = cn.ToString() + "  n=" + cn.Choices + ", k=" + cn.Picks + ", last=" + cn.Rank;
            Console.WriteLine (text);

            // Rank will always stay in bounds:

            cn.Rank = cn.Rank + 1;
            Console.WriteLine ("\n{0}  n={1}, k={2}, rank={3}", cn, cn.Choices, cn.Picks, cn.Rank);

            // Create a k-combination of n=9 from the supplied picks:

            cn = new Combination (9, new int[] { 2, 4, 6, 8 });
            Console.WriteLine ("\n{0}  n={1}, k={2}, rank={3}", cn, cn.Choices, cn.Picks, cn.Rank);
        }

        /* Output:

        { 0, 1, 2, 5 }  n=6, k=4, rank=2

        { 2, 3, 4, 5 }  n=6, k=4, last=14

        { 0, 1, 2, 3 }  n=6, k=4, rank=0

        { 2, 4, 6, 8 }  n=9, k=4, rank=105

        */
    }
}
