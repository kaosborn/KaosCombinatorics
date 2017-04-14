using System;
using Kaos.Combinatorics;

namespace ExampleApp
{
    static class PtExample04
    {
        static void Main()
        {
            int[] sizes = { 7, 6, 5 };
            long rank = 43;

            // Create a cartesian product row of the supplied rank:

            var pt = new Product (sizes, rank);
            Console.WriteLine ("{0}  w={1}, rank={2}\n", pt, pt.Width, pt.Rank);

            // Assign -1 to get the last rank & treat row like a string:

            pt.Rank = -1;
            string text = pt.ToString() + "  w=" + pt.Width + ", last=" + pt.Rank;
            Console.WriteLine (text);

            // Rank will always stay in bounds:

            pt.Rank = pt.Rank + 1;
            Console.WriteLine ("\n{0}  w={1}, rank={2}", pt, pt.Width, pt.Rank);

            // Create a cartesian product row from the supplied values:

            pt = new Product (new int[] { 7, 6, 5 }, new int[] { 5, 4, 3 });
            Console.WriteLine ("\n{0}  w={1}, rank={2}", pt, pt.Width, pt.Rank);
        }

        /* Output:

        { 1, 2, 3 }  w=3, rank=43

        { 6, 5, 4 }  w=3, last=209

        { 0, 0, 0 }  w=3, rank=0

        { 5, 4, 3 }  w=3, rank=173

        */
    }
}
