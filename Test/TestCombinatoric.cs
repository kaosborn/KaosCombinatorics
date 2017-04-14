using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kaos.Combinatorics;

namespace CombinatoricsTest
{
    [TestClass]
    public class TestCombinatoric
    {
        #region Test static methods

        [TestMethod]
        public void Test_BinomialCoefficient0()
        {
            long bc1 = Combinatoric.BinomialCoefficient (1, -1);
            long bc2 = Combinatoric.BinomialCoefficient (1, 2);

            Assert.AreEqual (0, bc1);
            Assert.AreEqual (0, bc2);
        }


        // Returns as many complete rows of Pascal's triangle as possible.
        static List<long[]> BuildPascalsTriangle()
        {
            var pascals = new List<long[]>();

            pascals.Add (new long[] { 1 });
            try
            {
                for (int n = 1; ; ++n)
                {
                    var row = new long[n+1];
                    row[0] = 1;
                    for (int k = 1; k <= n - 1; ++k)
                        row[k] = checked (pascals[n-1][k-1] + pascals[n-1][k]);
                    row[n] = 1;
                    pascals.Add (row);
                }
            }
            catch (OverflowException) { }

            return pascals;
        }


        [TestMethod]
        public void Test_BinomialCoefficient1()
        {
            var bcTable = BuildPascalsTriangle();
            long counter = 0;

            for (int n = 0; n < bcTable.Count; ++n)
                for (int k = 0; k < bcTable[n].Length; ++k)
                {
                    try
                    {
                        long bc = Combinatoric.BinomialCoefficient (n, k);
                        Assert.AreEqual (bcTable[n][k], bc, "n=" + n + ", k=" + k);
                        ++counter;
                    }
                    catch (OverflowException)
                    {
                        //TODO System.Diagnostics.Debug.WriteLine ("Ignoring OverflowException: n={0}, k={1}", n, k);
                    }
                }
        }


        [TestMethod]
        public void Test_BinomialCoefficient2()
        {
            var bcTable = BuildPascalsTriangle();
            int n = bcTable.Count;

            Assert.AreEqual (1, Combinatoric.BinomialCoefficient (n, 0));
            for (int k = 1; k < n/4; ++k)
            {
                long bc = Combinatoric.BinomialCoefficient (n, k);
                long expected = bcTable[n-1][k-1] + bcTable[n-1][k];
                long actual = Combinatoric.BinomialCoefficient (n, k);

                Assert.AreEqual (expected, actual, "k=" + k);
            }

            for (int k = (n*3)/4; k < n; ++k)
            {
                long bc = Combinatoric.BinomialCoefficient (n, k);
                long expected = bcTable[n-1][k-1] + bcTable[n-1][k];
                long actual = Combinatoric.BinomialCoefficient (n, k);

                Assert.AreEqual (expected, actual, "k=" + k);
            }
        }


        [TestMethod]
        [ExpectedException (typeof (IndexOutOfRangeException))]
        public void Test_Factorial_IndexOutOfRangeException()
        {
            long f = Combinatoric.Factorial (21);
        }


        [TestMethod]
        public void Test_Factorial()
        {
            Assert.AreEqual (1, Combinatoric.Factorial (0));

            long f = 1;
            for (int n = 1; n <= 20; ++n)
            {
                f = f * n;
                Assert.AreEqual (f, Combinatoric.Factorial (n));
            }
        }

        #endregion
    }
}
