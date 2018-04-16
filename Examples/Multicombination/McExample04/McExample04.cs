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
            Console.WriteLine ($"{mc}  n={mc.Choices}, k={mc.Picks}, rank={mc.Rank}\n");

            // Assign -1 to get the last rank:
            mc.Rank = -1;
            Console.WriteLine ($"{mc}  n={mc.Choices}, k={mc.Picks}, last={mc.Rank}\n");

            // Rank will always stay in bounds:
            mc.Rank = mc.Rank + 1;
            Console.WriteLine ($"{mc}  n={mc.Choices}, k={mc.Picks}, rank={mc.Rank}\n");

            // Create a k-multicombination of n=9 from the supplied picks:
            mc = new Multicombination (9, new int[] { 1, 1, 3, 8 });
            Console.WriteLine ($"{mc}  n={mc.Choices}, k={mc.Picks}, rank={mc.Rank}");
        }

        /* Output:

        { 0, 1, 1, 5 }  n=6, k=4, rank=25

        { 5, 5, 5, 5 }  n=6, k=4, last=125

        { 0, 0, 0, 0 }  n=6, k=4, rank=0

        { 1, 1, 3, 8 }  n=9, k=4, rank=185

        */
    }
}
