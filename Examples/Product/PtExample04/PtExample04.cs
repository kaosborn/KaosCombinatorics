using System;
using Kaos.Combinatorics;

namespace ExampleApp
{
    static class PtExample04
    {
        static void Main()
        {
            // Create a cartesian product row of the supplied sizes and rank:
            var pt = new Product (new int[] { 7, 6, 5 }, 43);
            Console.WriteLine ($"{pt}  width={pt.Width}, rank={pt.Rank}\n");

            // Assign -1 to get the last rank:
            pt.Rank = -1;
            Console.WriteLine ($"{pt}  width={pt.Width}, last={pt.Rank}\n");

            // Rank will always stay in bounds:
            pt.Rank = pt.Rank + 1;
            Console.WriteLine ($"{pt}  width={pt.Width}, rank={pt.Rank}\n");

            // Create a cartesian product row from the supplied values:
            pt = new Product (new int[] { 7, 6, 5 }, new int[] { 5, 4, 3 });
            Console.WriteLine ($"{pt}  width={pt.Width}, rank={pt.Rank}");
        }

        /* Output:

        { 1, 2, 3 }  width=3, rank=43

        { 6, 5, 4 }  width=3, last=209

        { 0, 0, 0 }  width=3, rank=0

        { 5, 4, 3 }  width=3, rank=173

        */
    }
}
