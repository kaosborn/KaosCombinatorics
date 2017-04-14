using System;
using System.Linq;
using Kaos.Combinatorics;

namespace ExampleApp
{
    class PnRooksFirstSlim
    {
        static void Main()
        {
            foreach (var layout in new Permutation (8).GetRows())
            {
                for (int qx1 = 0; qx1 < layout.Choices; ++qx1)
                    for (int qx2 = qx1+1; qx2 < layout.Choices; ++qx2)
                        if (qx2 - qx1 - Math.Abs (layout[qx2]-layout[qx1]) == 0)
                            goto NEXT;
                Console.WriteLine (String.Join (" ", Enumerable.Range(0,layout.Choices).Select (x => ((char)('a'+x)).ToString()+(layout[x]+1))));
                NEXT:;
            }
        }
    }
}
