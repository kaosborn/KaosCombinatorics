//
// Project: KaosCombinatorics
// File: Combinatoric.cs
//
// Copyright © 2009-2021 Kasey Osborn (github.com/kaosborn)
// MIT License - Use and redistribute freely
//

using System;
using System.Collections.Generic;

[assembly: CLSCompliant (true)]
namespace Kaos.Combinatorics
{
    /// <summary>Provides static methods for combinatorics.</summary>
    /// <remarks>This class cannot be instantiated.</remarks>
    public static class Combinatoric
    {
        #region Private static fields

        static private readonly long[] factorial = new long[]
        {
            1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800, 39916800,
            479001600, 6227020800, 87178291200, 1307674368000, 20922789888000,
            355687428096000, 6402373705728000, 121645100408832000, 2432902008176640000
        };

        /// <exclude />
        static public int FactorialLength => factorial.Length;

        static private List<long[]> pascalsTriangle = null;
        static private int pascalsTriangleMaxN = -1;

        #endregion

        #region Private static methods

        // Returns as many complete rows of Pascal's triangle as possible.
        static private List<long[]> BuildPascalsTriangle()
        {
            var result = new List<long[]> { new long[] { 1 } };

            try
            {
                for (int n = 1; ; ++n)
                {
                    var row = new long[n+1];
                    row[0] = 1;
                    for (int k = 1; k <= n-1; ++k)
                        row[k] = checked (result[n-1][k-1] + result[n-1][k]);
                    row[n] = 1;
                    result.Add (row);
                }
            }
            catch (OverflowException)
            { /* expected once */ }

            return result;
        }

        #endregion

        #region Public static methods

        /// <summary>Returns the binomial coefficient of the supplied values.</summary>
        /// <param name="n">Number of choices.</param>
        /// <param name="k">Number of picks.</param>
        /// <returns>The binomial coefficient of <em>n</em> choose <em>k</em>.</returns>
        /// <remarks>
        /// The result is equal to row <em>n</em>, column <em>k</em> of Pascal's triangle
        /// with counting starting at 0.
        /// </remarks>
        /// <example>
        /// <para>
        /// The number of rows in a <see cref="Combination"/> table of <em>k</em> picks
        /// from <em>n</em> choices is:<br />
        /// <br />
        /// <c>Combinatoric.BinomialCoefficient (n, k)</c>
        /// </para>
        /// <para>
        /// The number of rows in a <see cref="Multicombination"/> table of <em>k</em> picks
        /// from <em>n</em> choices is:<br />
        /// <br />
        /// <c>Combinatoric.BinomialCoefficient (k+n-1, k)</c>
        /// </para>
        /// <para>
        /// An exception to the above formulas is the special case where the number of elements
        /// is 0. While mathematics treats this result as 1 row containing the empty product,
        /// this library returns 0 rows.
        /// </para>
        /// </example>
        /// <exception cref="OverflowException">When the numbers are just too big.</exception>
        static public long BinomialCoefficient (int n, int k)
        {
            if (k < 0 || k > n)
                return 0;

            for (;;)
            {
                if (n <= pascalsTriangleMaxN)
                    return pascalsTriangle[n][k];

                if (pascalsTriangleMaxN >= 0)
                    break;

                pascalsTriangle = BuildPascalsTriangle();
                pascalsTriangleMaxN = pascalsTriangle.Count - 1;
            }

            // Fall back to multiplicative formula:

            if (k > n - k)
                k = n - k;

            int factor = n - k;
            long result = 1;

            for (int ki = 1; ki <= k; ++ki)
                result = checked (result * unchecked (++factor)) / ki;

            return result;
        }

        /// <summary>Returns the factorial of the supplied value.</summary>
        /// <param name="value">Non-negative integer.</param>
        /// <returns>
        /// For increasing <em>value</em> starting at 0, returns
        /// 1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800, 39916800, 
        /// 479001600, 6227020800, 87178291200, 1307674368000, 20922789888000,
        /// 355687428096000, 6402373705728000, 121645100408832000, 2432902008176640000.
        /// </returns>
        /// <example>
        /// <para>
        /// The number of rows in a <see cref="Permutation"/> table of <em>n</em> choices is:<br />
        /// <br />
        /// <c>Combinatoric.Factorial (n)</c><br />
        /// <br />
        /// The number of rows in a <see cref="Permutation"/> table of <em>k</em> picks
        /// from <em>n</em> choices is:<br />
        /// <br />
        /// <c>Combinatoric.Factorial (n) / Combinatoric.Factorial (n-k)</c>
        /// </para>
        /// <para>
        /// An exception to the above formulas is the special case where the number of elements
        /// in the permutation is 0. While mathematics treats this result as 1 row containing the
        /// empty product, this library returns 0 rows.
        /// </para>
        /// </example>
        /// <exception cref="IndexOutOfRangeException">When <em>value</em> not in range (0..20).</exception>
        static public long Factorial (int value) => factorial[value];

        #endregion
    }
}
