using System;
using System.Linq;
using Kaos.Combinatorics;

namespace ExampleApp
{
    // Subclassing is one way to get user-friendly output:
    public class Fruit : Combination
    {
        string[] choices;

        public Fruit (string[] list) : base (list.Length)
        { this.choices = list; }

        public override string ToString()
        { return String.Join (" ", from ei in this select choices[ei]); }
    }

    class CnExample02
    {
        static void Main()
        {
            string[] names = { "apple", "banana", "cherry", "durian" };

            foreach (var basket in new Fruit (names).GetRowsForAllPicks())
                Console.WriteLine (basket);
        }

        /* Output:

        apple
        banana
        cherry
        durian
        apple banana
        apple cherry
        apple durian
        banana cherry
        banana durian
        cherry durian
        apple banana cherry
        apple banana durian
        apple cherry durian
        banana cherry durian
        apple banana cherry durian

        */
    }
}
