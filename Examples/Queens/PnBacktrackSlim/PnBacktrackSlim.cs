using System;
using System.Linq;
using Kaos.Combinatorics;

namespace ExampleApp
{
    class PnBacktrackSlim
    {
        static void Main()
        {
            int nq = 8;

            if (nq > 1)
                for (var layout = new Permutation (nq); ; ++layout.Rank)
                {
                TOP:for (int x1 = 1; x1 < nq; ++x1)
                        for (int x2 = 0; x2 < x1; ++x2)
                            if (x1 - x2 - Math.Abs (layout[x2]-layout[x1]) == 0)
                                if (layout.Backtrack (x1) >= 0)
                                    goto TOP;
                                else
                                    return;

                    Console.WriteLine (String.Join (" ", Enumerable.Range (0,nq).Select (x => ((char) ('a'+x)).ToString()+(layout[x]+1))));
                }
        }
    }
}
