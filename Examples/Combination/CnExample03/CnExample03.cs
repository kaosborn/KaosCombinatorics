using System;
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

    class CnExample03
    {
        static void Main()
        {
            object[] things =
            {
                new Fruit ("apple"),
                new Furniture ("bench"),
                new Furniture ("chair"),
                new Fruit ("durian"),
                new Fruit ("eggplant")
            };

            // Use k-combinations to get rearrangements of other objects:
            foreach (var row in new Combination (things.Length, 3).GetRows())
                Console.WriteLine (String.Join (" ", Combination.Permute (row, things)));
        }

        /* Output:

        apple bench chair
        apple bench durian
        apple bench eggplant
        apple chair durian
        apple chair eggplant
        apple durian eggplant
        bench chair durian
        bench chair eggplant
        bench durian eggplant
        chair durian eggplant

        */
    }
}
