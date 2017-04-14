using System;
using System.Collections.Generic;
using Kaos.Combinatorics;

namespace ExampleApp
{
    public class Furniture
    {
        private string name;
        public Furniture (string newName) { name = newName; }
        public override string ToString() { return name; }
    }

    public class Fruit
    {
        private string name;
        public Fruit (string newName) { name = newName; }
        public override string ToString() { return name; }
    }

    static class PnExample03
    {
        static void Main()
        {
            var things = new List<object>
            {
                new Fruit ("apple"),
                new Furniture ("bench"),
                new Furniture ("chair")
            };

            // Use permutations to get rearrangements of other objects:

            foreach (var row in new Permutation (things.Count).GetRows())
            {
                foreach (var mix in Permutation.Permute (row, things))
                    Console.Write ("{0} ", mix);
                Console.WriteLine();
            }
        }

        /* Output:

        apple bench chair
        apple chair bench
        bench apple chair
        bench chair apple
        chair apple bench
        chair bench apple

        */
    }
}
