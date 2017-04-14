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

    static class PtExample03
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
            {
                foreach (object coloredThing in Product.Permute (row, lists))
                    Console.Write ("{0} ", coloredThing);
                Console.WriteLine();
            }
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
