using System;
using Kaos.Combinatorics;

namespace ExampleApp
{
    static class CnExample01
    {
        static void Main()
        {
            var cn = new Combination (choices:6, picks:3);

            Console.WriteLine ("n={0}, k={1}:\n", cn.Choices, cn.Picks);

            foreach (var row in cn.GetRows())
                Console.WriteLine ("{0,2}:  {1}", row.Rank, row);
        }

        /* Output:

        n=6, k=3:

         0:  { 0, 1, 2 }
         1:  { 0, 1, 3 }
         2:  { 0, 1, 4 }
         3:  { 0, 1, 5 }
         4:  { 0, 2, 3 }
         5:  { 0, 2, 4 }
         6:  { 0, 2, 5 }
         7:  { 0, 3, 4 }
         8:  { 0, 3, 5 }
         9:  { 0, 4, 5 }
        10:  { 1, 2, 3 }
        11:  { 1, 2, 4 }
        12:  { 1, 2, 5 }
        13:  { 1, 3, 4 }
        14:  { 1, 3, 5 }
        15:  { 1, 4, 5 }
        16:  { 2, 3, 4 }
        17:  { 2, 3, 5 }
        18:  { 2, 4, 5 }
        19:  { 3, 4, 5 }

        */
    }
}
