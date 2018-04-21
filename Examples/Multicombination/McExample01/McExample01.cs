using System;
using Kaos.Combinatorics;

namespace ExampleApp
{
    class McExample01
    {
        static void Main()
        {
            var mc = new Multicombination (choices:4, picks:3);

            Console.WriteLine ($"n={mc.Choices}, k={mc.Picks}:\n");

            foreach (var row in mc.GetRows())
                Console.WriteLine ($"{row.Rank,2}:  {row}");
        }

        /* Output:

        n=4, k=3:

         0:  { 0, 0, 0 }
         1:  { 0, 0, 1 }
         2:  { 0, 0, 2 }
         3:  { 0, 0, 3 }
         4:  { 0, 1, 1 }
         5:  { 0, 1, 2 }
         6:  { 0, 1, 3 }
         7:  { 0, 2, 2 }
         8:  { 0, 2, 3 }
         9:  { 0, 3, 3 }
        10:  { 1, 1, 1 }
        11:  { 1, 1, 2 }
        12:  { 1, 1, 3 }
        13:  { 1, 2, 2 }
        14:  { 1, 2, 3 }
        15:  { 1, 3, 3 }
        16:  { 2, 2, 2 }
        17:  { 2, 2, 3 }
        18:  { 2, 3, 3 }
        19:  { 3, 3, 3 }

        */
    }
}
