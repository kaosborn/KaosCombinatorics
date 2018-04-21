using System;
using Kaos.Combinatorics;

namespace ExampleApp
{
    public class Pastry
    {
        public string Name { get; private set; }
        public Pastry (string name) { Name = name; }
        public override string ToString() => Name;
    }

    class McExample03
    {
        static void Main()
        {
            Pastry[] Pastries =
            {
                new Pastry ("eclair"),
                new Pastry ("strudel"),
                new Pastry ("donut"),
                new Pastry ("croissant")
            };

            // Use k-multicombinations to get rearrangements of other objects:
            foreach (var row in new Multicombination (Pastries.Length, 3).GetRows())
                Console.WriteLine (String.Join (" ", Multicombination.Permute (row, Pastries)));
        }

        /* Output:

        eclair eclair eclair
        eclair eclair strudel
        eclair eclair donut
        eclair eclair croissant
        eclair strudel strudel
        eclair strudel donut
        eclair strudel croissant
        eclair donut donut
        eclair donut croissant
        eclair croissant croissant
        strudel strudel strudel
        strudel strudel donut
        strudel strudel croissant
        strudel donut donut
        strudel donut croissant
        strudel croissant croissant
        donut donut donut
        donut donut croissant
        donut croissant croissant
        croissant croissant croissant

        */
    }
}
