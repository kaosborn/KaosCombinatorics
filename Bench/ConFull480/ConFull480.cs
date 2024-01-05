using System;
using Kaos.Combinatorics;

namespace BenchApp
{
    class ConFull480
    {
        static void Main()
        {
            Console.WriteLine ("This program targets .NET Framework 4.8 with Shared Library reference.");

            foreach (var row in new Combination (4, 2).GetRows())
                Console.WriteLine ($"{row.Rank,2}: {row}");
            foreach (var row in new Multicombination (4, 2).GetRows())
                Console.WriteLine ($"{row.Rank,2}: {row}");
            foreach (var row in new Permutation (4, 2).GetRows())
                Console.WriteLine ($"{row.Rank,2}: {row}");
            foreach (var row in new Product (new int[] { 2, 3 }).GetRows())
                Console.WriteLine ($"{row.Rank,2}: {row}");
        }
    }
}
