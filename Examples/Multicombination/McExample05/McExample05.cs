using System;
using System.Linq;
using Kaos.Combinatorics;

namespace ExampleApp
{
    class McExample05
    {
        static void Main()
        {
            // Create a k-multicombination:

            var mc = new Multicombination (choices:9, picks:7, rank:5973);
            Console.WriteLine ("n={0}, k={1}, rank={2}:\n", mc.Choices, mc.Picks, mc.Rank);

            // Access elements using the default enumerator:

            var text = String.Concat (mc.Select (ei => (char) ('A' + ei)));
            Console.WriteLine (text + "\n");

            // Access elements using the indexer:

            for (int i = 0; i < mc.Picks; ++i)
                Console.WriteLine ("Element at {0} is {1}", i, mc[i]);
        }

        /* Output:

        n=9, k=7, rank=5973:

        DEFFFHI

        Element at 0 is 3
        Element at 1 is 4
        Element at 2 is 5
        Element at 3 is 5
        Element at 4 is 5
        Element at 5 is 7
        Element at 6 is 8

        */
    }
}
