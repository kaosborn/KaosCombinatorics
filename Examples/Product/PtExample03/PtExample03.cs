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

    class PtExample03
    {
        static void Main()
        {
            var colors = new List<object> { "aqua", "black", "crimson" };

            var things = new List<object>
            {
                new Fruit ("apple"),
                new Furniture ("bench"),
                new Furniture ("chair"),
                new Fruit ("durian"),
                new Fruit ("eggplant")
            };

            var lists = new List<object>[] { colors, things };
            int[] counts = { colors.Count, things.Count };

            // Use a cartesian product to get rearrangements of other objects:
            foreach (var row in new Product (counts).GetRows())
                Console.WriteLine (String.Join (" ", Product.Permute (row, lists)));
        }

        /* Output:

        aqua apple
        aqua bench
        aqua chair
        aqua durian
        aqua eggplant
        black apple
        black bench
        black chair
        black durian
        black eggplant
        crimson apple
        crimson bench
        crimson chair
        crimson durian
        crimson eggplant

        */
    }
}
