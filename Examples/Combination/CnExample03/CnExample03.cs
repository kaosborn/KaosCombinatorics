using System;
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

    static class CnExample03
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
            {
                foreach (var thing in Combination.Permute (row, things))
                    Console.Write (thing + " ");
                Console.WriteLine();
            }
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
