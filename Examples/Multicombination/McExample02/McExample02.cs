using System;
using System.Linq;
using Kaos.Combinatorics;

namespace ExampleApp
{
    // Subclassing is one way to get user-friendly output:
    public class LettersXYZ : Multicombination
    {
        static string[] letters = { "X", "Y", "Z" };

        public LettersXYZ() : base (letters.Length)
        { }

        public override string ToString()
        { return String.Concat (from ei in this select letters[ei]); }
    }

    class McExample02
    {
        static void Main()
        {
            foreach (var row in new LettersXYZ().GetRowsForPicks (1, 4))
                Console.WriteLine ($"Rank {row.Rank,2}:  {row}");
        }

        /* Output:

        Rank  0:  X
        Rank  1:  Y
        Rank  2:  Z
        Rank  0:  XX
        Rank  1:  XY
        Rank  2:  XZ
        Rank  3:  YY
        Rank  4:  YZ
        Rank  5:  ZZ
        Rank  0:  XXX
        Rank  1:  XXY
        Rank  2:  XXZ
        Rank  3:  XYY
        Rank  4:  XYZ
        Rank  5:  XZZ
        Rank  6:  YYY
        Rank  7:  YYZ
        Rank  8:  YZZ
        Rank  9:  ZZZ
        Rank  0:  XXXX
        Rank  1:  XXXY
        Rank  2:  XXXZ
        Rank  3:  XXYY
        Rank  4:  XXYZ
        Rank  5:  XXZZ
        Rank  6:  XYYY
        Rank  7:  XYYZ
        Rank  8:  XYZZ
        Rank  9:  XZZZ
        Rank 10:  YYYY
        Rank 11:  YYYZ
        Rank 12:  YYZZ
        Rank 13:  YZZZ
        Rank 14:  ZZZZ

        */
    }
}
