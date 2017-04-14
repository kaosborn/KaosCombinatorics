using System;
using System.Linq;
using Kaos.Combinatorics;

namespace ExampleApp
{
    static class CnExample05
    {
        static void Main()
        {
            // Create a k-combination:

            var cn = new Combination (choices:10, picks:7, rank:110);
            Console.WriteLine ("n={0}, k={1}, rank={2}:\n", cn.Choices, cn.Picks, cn.Rank);

            // Access elements using the default enumerator:

            var text = String.Concat (cn.Select (ei => (char) ('A' + ei)));
            Console.WriteLine (text + "\n");

            // Access elements using the indexer:

            for (int i = 0; i < cn.Picks; ++i)
                Console.WriteLine ("Element at {0} is {1}", i, cn[i]);
        }

        /* Output:

        n=10, k=7, rank=110:

        BDFGHIJ

        Element at 0 is 1
        Element at 1 is 3
        Element at 2 is 5
        Element at 3 is 6
        Element at 4 is 7
        Element at 5 is 8
        Element at 6 is 9

        */
    }
}
