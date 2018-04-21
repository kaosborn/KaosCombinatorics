using System;
using Kaos.Combinatorics;

namespace ExamleApp
{
    // Subclassing is one way to get user-friendly output:
    public class FullName : Product
    {
        static string[] first = { "Alice", "Bob", "Carol", "David" };
        static string[] last = { "Smith", "Jones" };
        static string[][] nameRows = { first, last };
        static int[] sizes = { first.Length, last.Length };

        public FullName() : base (sizes)
        { }

        public override string ToString()
        {
            string result = "";
            for (int column = 0; column < Width; ++column)
            {
                if (column > 0)
                    result += " ";
                result += nameRows[column][this[column]];
            }
            return result;
        }
    }

    class PtExample02
    {
        static void Main()
        {
            foreach (var full in new FullName().GetRows())
                Console.WriteLine (full);
        }

        /* Output:

        Alice Smith
        Alice Jones
        Bob Smith
        Bob Jones
        Carol Smith
        Carol Jones
        David Smith
        David Jones

        */
    }
}
