using System;
using Kaos.Combinatorics;

namespace ExampleApp
{
    static class PtExample01
    {
        static void Main()
        {
            int[] sizes = { 2, 4, 3 };

            var pt = new Product (sizes);

            Console.WriteLine ($"    ( {String.Join (", ", sizes)} )\n");

            foreach (var row in pt.GetRows())
                Console.WriteLine ($"{row.Rank,2}: {row}");
        }

        /* Output:

            ( 2, 4, 3 )

         0: { 0, 0, 0 }
         1: { 0, 0, 1 }
         2: { 0, 0, 2 }
         3: { 0, 1, 0 }
         4: { 0, 1, 1 }
         5: { 0, 1, 2 }
         6: { 0, 2, 0 }
         7: { 0, 2, 1 }
         8: { 0, 2, 2 }
         9: { 0, 3, 0 }
        10: { 0, 3, 1 }
        11: { 0, 3, 2 }
        12: { 1, 0, 0 }
        13: { 1, 0, 1 }
        14: { 1, 0, 2 }
        15: { 1, 1, 0 }
        16: { 1, 1, 1 }
        17: { 1, 1, 2 }
        18: { 1, 2, 0 }
        19: { 1, 2, 1 }
        20: { 1, 2, 2 }
        21: { 1, 3, 0 }
        22: { 1, 3, 1 }
        23: { 1, 3, 2 }

        */
    }
}
