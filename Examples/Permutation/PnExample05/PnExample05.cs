using System;
using System.Linq;
using Kaos.Combinatorics;

namespace ExampleApp
{
    static class PnExample05
    {
        static void Main()
        {
            // Create a k-permutation:

            var pn = new Permutation (choices: 8, picks: 4, rank: 601);
            Console.WriteLine ("n={0}, k={1}, rank={2}:\n", pn.Choices, pn.Picks, pn.Rank);

            // Access elements using the default enumerator:

            var text = String.Concat (pn.Select (ei => (char) ('A' + ei)));
            Console.WriteLine (text + "\n");

            // Access elements using the indexer:

            for (int i = 0; i < pn.Picks; ++i)
                Console.WriteLine ("Element at {0} is {1}", i, pn[i]);
        }

        /* Output:

        n=8, k=4, rank=601:

        CHAD

        Element at 0 is 2
        Element at 1 is 7
        Element at 2 is 0
        Element at 3 is 3

        */
    }
}
