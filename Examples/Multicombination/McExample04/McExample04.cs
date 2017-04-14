using System;
using Kaos.Combinatorics;

namespace ExampleApp
{
    static class McExample04
    {
        static void Main()
        {
            // Create a k-multicombination of the supplied rank:

            var mc = new Multicombination (choices:6, picks:4, rank:25);
            Console.WriteLine ("{0}  n={1}, k={2}, rank={3}\n", mc, mc.Choices, mc.Picks, mc.Rank);

            // Assign -1 to get the last rank, treat row like a string:

            mc.Rank = -1;
            string text = mc.ToString() + "  n=" + mc.Choices + ", k=" + mc.Picks + ", last=" + mc.Rank;
            Console.WriteLine (text);

            // Rank will always stay in bounds:

            mc.Rank = mc.Rank + 1;
            Console.WriteLine ("\n{0}  n={1}, k={2}, rank={3}", mc, mc.Choices, mc.Picks, mc.Rank);

            // Create a k-multicombination of n=9 from the supplied picks:

            mc = new Multicombination (9, new int[] { 1, 1, 3, 8 });
            Console.WriteLine ("\n{0}  n={1}, k={2}, rank={3}", mc, mc.Choices, mc.Picks, mc.Rank);
        }

        /* Output:

        { 0, 1, 1, 5 }  n=6, k=4, rank=25

        { 5, 5, 5, 5 }  n=6, k=4, last=125

        { 0, 0, 0, 0 }  n=6, k=4, rank=0

        { 1, 1, 3, 8 }  n=9, k=4, rank=185

        */
    }
}
