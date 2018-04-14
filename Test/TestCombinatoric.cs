using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kaos.Combinatorics;

namespace CombinatoricsTest
{
    [TestClass]
    public class TestCombinatoric
    {
        #region Support methods

        // This reflection cheat is necessary for complete code coverage.
        private void ResetCombinatoric()
        {
            Type ct = typeof (Combinatoric);

            FieldInfo fiN = ct.GetField ("pascalsTriangleMaxN", BindingFlags.Static|BindingFlags.NonPublic);
            fiN.SetValue (null, -1);

            FieldInfo fiT = ct.GetField ("pascalsTriangle", BindingFlags.Static|BindingFlags.NonPublic);
            fiT.SetValue (null, null);
        }

        // Returns as many complete rows of Pascal's triangle as possible.
        private static List<long[]> BuildPascalsTriangle()
        {
            var pascals = new List<long[]> { new long[] { 1 } };

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
            catch (OverflowException) { /* expected once */ }

            return pascals;
        }

        #endregion

        #region Test static methods

        [TestMethod]
        [ExpectedException (typeof (OverflowException))]
        public void Crash_BinomialCoefficient_Overflow()
        {
            long zz = Combinatoric.BinomialCoefficient (67, 34);
        }


        [TestMethod]
        public void Unit_BinomialCoefficient1()
        {
            ResetCombinatoric();

            long bc1 = Combinatoric.BinomialCoefficient (1, -1);
            Assert.AreEqual (0, bc1);

            long bc2 = Combinatoric.BinomialCoefficient (1, 2);
            Assert.AreEqual (0, bc2);

            long bc3 = Combinatoric.BinomialCoefficient (65, 4);
            Assert.AreEqual (677040, bc3);

            long bc4 = Combinatoric.BinomialCoefficient (66, 33);
            Assert.AreEqual (7219428434016265740, bc4);
        }

        [TestMethod]
        public void Unit_BinomialCoefficient2()
        {
            ResetCombinatoric();

            long bc5 = Combinatoric.BinomialCoefficient (80, 3);
            Assert.AreEqual (82160, bc5);
        }


        [TestMethod]
        public void Unit_BinomialCoefficient3()
        {
            var bcTable = BuildPascalsTriangle();

            for (int n = 0; n < bcTable.Count; ++n)
                for (int k = 0; k < bcTable[n].Length; ++k)
                {
                    long bc = Combinatoric.BinomialCoefficient (n, k);
                    Assert.AreEqual (bcTable[n][k], bc, "n=" + n + ", k=" + k);
                }
        }

        [TestMethod]
        public void Unit_BinomialCoefficient4()
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
        public void Crash_Factorial_IndexOutOfRange()
        {
            long f = Combinatoric.Factorial (21);
        }


        [TestMethod]
        public void Unit_Factorial()
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
