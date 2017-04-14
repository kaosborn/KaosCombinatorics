using System;
using Kaos.Combinatorics;

namespace ExampleApp
{
    static class PnExample06
    {
        static void Main()
        {
            var pn = new Permutation (choices: 4, picks: 3);

            Console.WriteLine ("n={0}, k={1}, count={2}:\n", pn.Choices, pn.Picks, pn.RowCount);

            foreach (var row in pn.GetRows())
                Console.WriteLine ("{0,2}:  {1}", row.Rank, row);
        }

        /* Output:

        n=4, k=3, count=24:

         0:  { 0, 1, 2 }
         1:  { 0, 1, 3 }
         2:  { 0, 2, 1 }
         3:  { 0, 2, 3 }
         4:  { 0, 3, 1 }
         5:  { 0, 3, 2 }
         6:  { 1, 0, 2 }
         7:  { 1, 0, 3 }
         8:  { 1, 2, 0 }
         9:  { 1, 2, 3 }
        10:  { 1, 3, 0 }
        11:  { 1, 3, 2 }
        12:  { 2, 0, 1 }
        13:  { 2, 0, 3 }
        14:  { 2, 1, 0 }
        15:  { 2, 1, 3 }
        16:  { 2, 3, 0 }
        17:  { 2, 3, 1 }
        18:  { 3, 0, 1 }
        19:  { 3, 0, 2 }
        20:  { 3, 1, 0 }
        21:  { 3, 1, 2 }
        22:  { 3, 2, 0 }
        23:  { 3, 2, 1 }

        */
    }
}
