using System;
using System.Linq;
using Kaos.Combinatorics;

namespace ExampleApp
{
    class PtBruteForceSlim
    {
        static void Main()
        {
            int nq = 8;

            foreach (var layout in new Product (Enumerable.Repeat (nq,nq).ToArray()).GetRows())
            {
                for (int qx1 = 0; qx1 < layout.Width; ++qx1)
                    for (int qx2 = qx1+1; qx2 < layout.Width; ++qx2)
                        if (layout[qx1] == layout[qx2] || qx2 - qx1 - Math.Abs (layout[qx2]-layout[qx1]) == 0)
                            goto NEXT;

                    Console.WriteLine (String.Join (" ", Enumerable.Range (0, layout.Width)
                                                        .Select (qx => ((char) ('a'+qx)).ToString() + (layout[qx]+1))));
                NEXT:;
            }
        }
    }
}
