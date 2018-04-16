using System;
using System.Linq;
using Kaos.Combinatorics;

namespace ExampleApp
{
    static class PtExample05
    {
        static void Main()
        {
            int[] sizes = { 2, 3, 4, 5, 6, 7 };
            long rank = 925;

            // Create a cartesian product row:
            var pt = new Product (sizes, rank);

            // Access elements using the default enumerator:
            var text = String.Concat (pt.Select (ei => (char) ('A' + ei)));
            Console.WriteLine ($"{text}\n");

            // Access elements using the indexer:
            for (int i = 0; i < pt.Width; ++i)
                Console.WriteLine ($"Element at {i} is {pt[i]}");
        }

        /* Output:

        ABACAB

        Element at 0 is 0
        Element at 1 is 1
        Element at 2 is 0
        Element at 3 is 2
        Element at 4 is 0
        Element at 5 is 1

        */
    }
}
