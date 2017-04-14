using System;
using System.Linq;
using Kaos.Combinatorics;

namespace ExampleApp
{
    // Subclassing is one way to get user-friendly output:
    public class NumberText : Permutation
    {
        static string[] text = { "one", "two", "three" };

        public NumberText() : base (text.Length)
        { }

        public override string ToString()
        { return String.Join (" ", from ei in this select text[ei]); }
    }


    static class PnExample02
    {
        static void Main()
        {
            Console.WriteLine ("All picks:\n");
            foreach (var row in new NumberText().GetRowsForAllPicks())
                Console.WriteLine (row);

            Console.WriteLine ("\nAll choices:\n");
            foreach (var row in new NumberText().GetRowsForAllChoices())
                Console.WriteLine (row);
        }

        /* Output:

        All picks:

        one
        two
        three
        one two
        one three
        two one
        two three
        three one
        three two
        one two three
        one three two
        two one three
        two three one
        three one two
        three two one

        All choices:

        one
        one two
        two one
        one two three
        one three two
        two one three
        two three one
        three one two
        three two one

        */
    }
}
