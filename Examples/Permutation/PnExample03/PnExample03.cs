using System;
using System.Collections.Generic;
using Kaos.Combinatorics;

namespace ExampleApp
{
    public class Furniture
    {
        public string Name { get; private set; }
        public Furniture (string name) { Name = name; }
        public override string ToString() => Name;
    }

    public class Fruit
    {
        public string Name { get; private set; }
        public Fruit (string name) { Name = name; }
        public override string ToString() => Name;
    }

    class PnExample03
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
                Console.WriteLine (String.Join (" ", Permutation.Permute (row, things)));
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
