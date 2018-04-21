using System;
using Kaos.Combinatorics;

namespace ExampleApp
{
    public class Pastry
    {
        private string name;
        public Pastry (string newName) { name = newName; }
        public override string ToString() { return name; }
    }

    class McExample03
    {
        static void Main()
        {
            Pastry[] Pastries =
            {
                new Pastry ("danish"),
                new Pastry ("eclair"),
                new Pastry ("strudel"),
                new Pastry ("donut"),
                new Pastry ("croissant")
            };

            // Use k-multicombinations to get rearrangements of other objects:

            foreach (var row in new Multicombination (Pastries.Length, 3).GetRows())
            {
                foreach (var treat in Multicombination.Permute (row, Pastries))
                    Console.Write (treat + " ");
                Console.WriteLine();
            }
        }

        /* Output:

        danish danish danish
        danish danish eclair
        danish danish strudel
        danish danish donut
        danish danish croissant
        danish eclair eclair
        danish eclair strudel
        danish eclair donut
        danish eclair croissant
        danish strudel strudel
        danish strudel donut
        danish strudel croissant
        danish donut donut
        danish donut croissant
        danish croissant croissant
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
