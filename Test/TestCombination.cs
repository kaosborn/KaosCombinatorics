using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kaos.Combinatorics;

namespace CombinatoricsTest
{
    [TestClass]
    public class TestCombination
    {
        #region Test constructors

        [TestMethod]
        public void Test_Interfaces()
        {
            Combination cn = new Combination (4, 2);

            Assert.IsNotNull (cn as IComparable);
            Assert.IsNotNull (cn as System.Collections.IEnumerable);
            Assert.IsNotNull (cn as IComparable<Combination>);
            Assert.IsNotNull (cn as IEquatable<Combination>);
            Assert.IsNotNull (cn as IEnumerable<int>);
        }


        [TestMethod]
        public void Test_Ctor0()
        {
            Combination cn0 = new Combination();
            Assert.AreEqual (0, cn0.Choices);
            Assert.AreEqual (0, cn0.Picks);
            Assert.AreEqual (0, cn0.Rank);
            Assert.AreEqual (0, cn0.RowCount);

            ++cn0.Rank;
            Assert.AreEqual (0, cn0.Rank);
            --cn0.Rank;
            Assert.AreEqual (0, cn0.Rank);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void Test_Ctor1a_ArgumentNullException()
        {
            Combination nullSource = null;
            Combination cn = new Combination (nullSource);
        }


        [TestMethod]
        public void Test_Ctor1a()
        {
            int n = 5, k = 3, r = 2;
            Combination cn1 = new Combination (n, k, r);
            Combination cn2 = new Combination (cn1);

            Assert.AreEqual (cn1.RowCount, cn1.RowCount);
            Assert.AreEqual (cn1.Picks, k);
            Assert.AreEqual (cn1.Rank, r);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor1b_ArgumentOutOfRangeException()
        {
            Combination cn = new Combination (-3);
        }


        [TestMethod]
        public void Test_Ctor1b1()
        {
            int n = 999;

            Combination cn = new Combination (n);

            Assert.AreEqual (n, cn.Choices);
            Assert.AreEqual (n, cn.Picks);
            Assert.AreEqual (0, cn.Rank);
            Assert.AreEqual (1, cn.RowCount);
        }


        [TestMethod]
        public void Test_Ctor1b2()
        {
            int n = 0;
            Combination cn = new Combination (n);

            Assert.AreEqual (n, cn.Choices);
            Assert.AreEqual (n, cn.Picks);
            Assert.AreEqual (0, cn.Rank);
            Assert.AreEqual (0, cn.RowCount);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor2a_ArgumentOutOfRangeException1()
        {
            Combination cn = new Combination (-2, 3);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor2a_ArgumentOutOfRangeException2()
        {
            Combination cn = new Combination (2, -3);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor2a_ArgumentOutOfRangeException3()
        {
            Combination cn = new Combination (2, 3);
        }


        [TestMethod]
        [ExpectedException (typeof (OverflowException))]
        public void Test_Ctor2a_OverflowException()
        {
            Combination cn = new Combination (90, 45);
        }


        [TestMethod]
        public void Test_Ctor2a()
        {
            Combination cn00 = new Combination (0, 0);
            Assert.AreEqual (0, cn00.RowCount);

            Combination cn22 = new Combination (2, 2);
            Assert.AreEqual (1, cn22.RowCount);

            Combination cn30 = new Combination (3, 0);
            Assert.AreEqual (0, cn30.RowCount);

            int actualCount = 0;
            foreach (Combination row in cn30.GetRows())
                ++actualCount;

            Assert.AreEqual (0, actualCount);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void Test_Ctor2b_ArgumentNullException()
        {
            int[] nullus = null;
            Combination cn = new Combination (1, nullus);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor2b_ArgumentOutOfRangeException1()
        {
            int[] array = new int[1];

            Combination cn = new Combination (-1, array);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor2b_ArgumentOutOfRangeException2()
        {
            int[] array = new int[2];
            Combination cn = new Combination (1, array);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor2b_ArgumentOutOfRangeException4()
        {
            int[] array = new int[] { -1, 0 };
            Combination cn = new Combination (2, array);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor2b_ArgumentOutOfRangeException5()
        {
            int[] array = new int[] { 0, 0 };
            Combination cn = new Combination (2, array);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor2b_ArgumentOutOfRangeException6()
        {
            int[] array = new int[] { 1 };
            Combination cn = new Combination (1, array);
        }


        [TestMethod]
        public void Test_Ctor2b_LongCombo()
        {
#if STRESS
            const int maxChoices = 500;
#else
            const int maxChoices = 50;
#endif
            for (int choices = 1; choices < maxChoices; ++choices)
            {
                int[] array = new int[choices];
                for (int ki = 0; ki < choices; ++ki)
                    array[ki] = ki;

                Combination cn = new Combination (choices, array);

                Assert.AreEqual (0, cn.Rank);
                Assert.AreEqual (1, cn.RowCount);
            }
        }


        [TestMethod]
        public void Test_Ctor2b1()
        {
            int n = 6;

            int[] cnVals1 = new int[] { 5, 0, 3 };

            Combination cn = new Combination (n, cnVals1);

            Assert.AreEqual (n, cn.Choices);
            Assert.AreEqual (cnVals1.Length, cn.Picks);
            Assert.AreEqual (8, cn.Rank);
            Assert.AreEqual (20, cn.RowCount);
        }


        [TestMethod]
        public void StressCombination_Ctor2b()
        {
            // Use higher maxChoices values for a longer running test.
#if STRESS
            const int maxChoices = 16;
#else
            const int maxChoices = 5;
#endif
            int counter = 0;

            for (int choices = 0; choices <= maxChoices; ++choices)
                for (int picks = 0; picks <= choices; ++picks)
                {
                    long maxRows = Combinatoric.BinomialCoefficient (choices, picks);
                    for (int rank = 0; rank < maxRows; ++rank)
                    {
                        Combination row1 = new Combination (choices, picks, rank);

                        int[] source = new int[picks];
                        row1.CopyTo (source);

                        Combination row2 = new Combination (choices, source);

                        // verify that rank(unrank(x)) = x
                        Assert.AreEqual (rank, row1.Rank);
                        Assert.AreEqual (rank, row2.Rank);
                        ++counter;
                    }
                }
            }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor3a_ArgumentOutOfRangeException1()
        {
            Combination cn = new Combination (-2, 3, 1);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor3a_ArgumentOutOfRangeException2()
        {
            Combination cn = new Combination (2, 3, 0);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor3a_ArgumentOutOfRangeException3()
        {
            Combination cn = new Combination (2, -1, 0);
        }


        [TestMethod]
        public void Test_Ctor3a0a()
        {
            Combination cn = new Combination (2, 0, 0);

            Assert.AreEqual (0, cn.Rank);
            Assert.AreEqual (0, cn.RowCount);
        }


        [TestMethod]
        public void Test_Ctor3a0b()
        {
            Combination cn = new Combination (0, 0, 1);
            Assert.AreEqual (0, cn.Rank);
            Assert.AreEqual (0, cn.RowCount);
        }


        [TestMethod]
        public void Test_Ctor3a0c()
        {
            Combination cn = new Combination (4, 0, 9);

            Assert.AreEqual (0, cn.Rank);
            Assert.AreEqual (0, cn.RowCount);
        }


        [TestMethod]
        public void Test_Ctor3a()
        {
            Combination cn = new Combination (6, 3, 12);

            Assert.AreEqual (1, cn[0]);
            Assert.AreEqual (2, cn[1]);
            Assert.AreEqual (5, cn[2]);
            Assert.AreEqual (12, cn.Rank);
            Assert.AreEqual (20, cn.RowCount);
        }

        #endregion

        #region Test properties

        [TestMethod]
        public void Test_Properties()
        {
            int n = 8;
            int k = 3;
            long expectedCount = Combinatoric.Factorial (n)
                              / (Combinatoric.Factorial (k) * Combinatoric.Factorial (n - k));

            Combination cn = new Combination (n, k);
            Assert.AreEqual (n, cn.Choices);
            Assert.AreEqual (k, cn.Picks);
            Assert.AreEqual (expectedCount, cn.RowCount);

            cn.Rank = -1;
            Assert.AreEqual (cn.RowCount - 1, cn.Rank);
        }


        [TestMethod]
        public void Test_Rank0()
        {
            Combination cn0 = new Combination();

            cn0.Rank = 1;
            Assert.AreEqual (0, cn0.Rank);

            cn0.Rank = 2;
            Assert.AreEqual (0, cn0.Rank);

            cn0.Rank = -1;
            Assert.AreEqual (0, cn0.Rank);
        }


        [TestMethod]
        public void Test_Rank1()
        {
            Combination cn1 = new Combination (1);

            cn1.Rank = 1;
            Assert.AreEqual (0, cn1.Rank);

            cn1.Rank = 2;
            Assert.AreEqual (0, cn1.Rank);

            cn1.Rank = -1;
            Assert.AreEqual (0, cn1.Rank);
        }


        [TestMethod]
        public void Test_Rank2()
        {
            Combination cn = new Combination (6, 4);

            cn.Rank = 1;
            Assert.AreEqual (1, cn.Rank);

            cn.Rank = 2;
            Assert.AreEqual (2, cn.Rank);

            cn.Rank = -1;
            Assert.AreEqual (14, cn.Rank);
        }


        [TestMethod]
        public void Test_RankIncrement()
        {
            Combination cn = new Combination (4, 3, 1);

            ++cn.Rank;
            Assert.AreEqual (2, cn.Rank);

            cn.Rank++;
            Assert.AreEqual (3, cn.Rank);
        }


        [TestMethod]
        public void Test_RankDecrement()
        {
            Combination cn = new Combination (4, 3, 1);

            --cn.Rank;
            Assert.AreEqual (0, cn.Rank);

            cn.Rank--;
            Assert.AreEqual (3, cn.Rank);
        }

        #endregion

        #region Test instance methods

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void Test_CopyTo_ArgumentNullException()
        {
            Combination cn = new Combination (3, 3, 4);

            int[] nullTarget = null;
            cn.CopyTo (nullTarget);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void Test_CopyTo_ArgumentException()
        {
            Combination cn = new Combination (3, 3, 4);

            int[] nullTarget = new int[2];
            cn.CopyTo (nullTarget);
        }


        [TestMethod]
        public void Test_CopyTo1()
        {
            Combination cn3 = new Combination (3, 3, 4);

            int[] target = new int[3];
            cn3.CopyTo (target);

            Assert.AreEqual (0, target[0]);
            Assert.AreEqual (1, target[1]);
            Assert.AreEqual (2, target[2]);
        }


        [TestMethod]
        public void Test_CompareToOBJECT()
        {
            var objectSortedList = new System.Collections.SortedList();
            objectSortedList.Add (new Combination (8, 2), 0);
            objectSortedList.Add (new Combination (8, 6), 2);
            objectSortedList.Add (new Combination (8, 4), 1);

            int expectedIndex = 0;
            foreach (System.Collections.DictionaryEntry item in objectSortedList)
            {
                int actualIndex = (int) item.Value;
                Assert.AreEqual (expectedIndex, actualIndex);
                expectedIndex++;
            }

            Assert.AreEqual (3, expectedIndex);
        }


        [TestMethod]
        public void Test_CompareTo()
        {
            Combination c0 = null;
            Combination c520 = new Combination (5, 2, 0);
            Combination c521 = new Combination (5, 2, 1);

            int actual1 = c520.CompareTo (c0);
            Assert.IsTrue (actual1 > 0);

            int actual2 = c520.CompareTo (c521);
            Assert.IsTrue (actual2 < 0);
        }


        [TestMethod]
        public void Test_EqualsOBJECT()
        {
            Combination c0 = null;
            Combination c438a = new Combination (4, 3, 8);
            Combination c438b = new Combination (4, 3, 8);
            Combination c439 = new Combination (4, 3, 9);
            Combination c6 = new Combination (8, 6, 4);

            object j0 = (object) c0;
            object j438b = (object) c438b;
            object j439 = (object) c439;
            object j6 = (object) c6;

            Assert.IsFalse (c438a.Equals (j0));
            Assert.IsTrue (c438a.Equals (j438b));
            Assert.IsFalse (c438a.Equals (j439));
            Assert.IsFalse (c438a.Equals (j6));
        }


        [TestMethod]
        public void Test_Equals()
        {
            Combination c0 = null;
            Combination c438a = new Combination (4, 3, 8);
            Combination c438b = new Combination (4, 3, 8);
            Combination c439 = new Combination (4, 3, 9);
            Combination c6 = new Combination (8, 6, 4);

            Assert.IsFalse (c438a.Equals (c0));
            Assert.IsTrue (c438a.Equals (c438b));
            Assert.IsFalse (c438a.Equals (c439));
            Assert.IsFalse (c438a.Equals (c6));
        }


        [TestMethod]
        public void Test_EqualsOtherType()
        {
            Combination c54 = new Combination (5, 4, 1);
            string s = "Zappa";

            // Comparing to different type returns false.
            Assert.IsFalse (c54.Equals (s));
        }


        [TestMethod]
        public void Test_GetEnumeratorOBJECT()
        {
            Combination cn = new Combination (7, 7);

            System.Collections.IEnumerator nu = ((System.Collections.IEnumerable) cn).GetEnumerator();

            int expected = 0;
            while (nu.MoveNext())
            {
                int actual = (int) nu.Current;
                Assert.AreEqual (expected, actual);
                ++expected;
            }

            Assert.AreEqual (cn.Picks, expected);
        }


        [TestMethod]
        public void Test_GetEnumerator()
        {
            int n = 10;
            int k = 9;
            Combination cn = new Combination (n, k);

            int expectedElement = 0;
            foreach (int actualElement in cn)
            {
                Assert.AreEqual (expectedElement, actualElement);
                ++expectedElement;
            }

            Assert.AreEqual (k, expectedElement);
        }


        [TestMethod]
        public void Test_GetHash()
        {
            Combination cn = new Combination (5);
            int hash = cn.GetHashCode();
        }


        [TestMethod]
        public void Test_GetRowsEmpty()
        {
            Combination cn = new Combination();

            foreach (Combination row in cn.GetRows())
            {
                Assert.Fail ("Enumeration should be empty");
            }
        }


        [TestMethod]
        public void Test_GetRows()
        {
            int[,] expected = new int[,]
            { { 0, 1, 2 },
              { 0, 1, 3 },
              { 0, 1, 4 },
              { 0, 2, 3 },
              { 0, 2, 4 },
              { 0, 3, 4 },
              { 1, 2, 3 },
              { 1, 2, 4 },
              { 1, 3, 4 },
              { 2, 3, 4 }
            };

            Combination c53 = new Combination (5, 3);

            long actualCount = 0;
            foreach (Combination cx in c53.GetRows())
            {
                Assert.IsTrue (actualCount < cx.RowCount);

                for (int k = 0; k < cx.Picks; ++k)
                    Assert.AreEqual (expected[actualCount, k], cx[k]);

                ++actualCount;
            }

            Assert.AreEqual (c53.RowCount, actualCount);
        }


        [TestMethod]
        public void Test_GetRowsForAllPicksEmpty()
        {
            Combination cn = new Combination();

            foreach (Combination row in cn.GetRowsForAllPicks())
            {
                Assert.Fail ("Enumeration should be empty");
            }
        }


        [TestMethod]
        public void Test_GetRowsForAllPicks()
        {
            int[][] expected = new int[][]
                { new int[] { 0 }, new int[] { 1 }, new int[] { 2 },
                  new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 1, 2 },
                  new int[] { 0, 1, 2 }
                };

            Combination cn3 = new Combination (3);

            long counter = 0;
            foreach (Combination row in cn3.GetRowsForAllPicks())
            {
                Assert.AreEqual (expected[counter].Length, row.Picks);

                for (int k = 0; k < row.Picks; ++k)
                    Assert.AreEqual (expected[counter][k], row[k]);

                ++counter;
            }

            Assert.AreEqual (expected.Length, counter);
        }


        [TestMethod]
        public void Test_ToString0()
        {
            Combination cn = new Combination();

            string actual = cn.ToString();

            Assert.AreEqual ("{ }", actual);
        }


        [TestMethod]
        public void Test_ToString1()
        {
            Combination cn = new Combination(1);

            string actual = cn.ToString();

            Assert.AreEqual ("{ 0 }", actual);
        }


        [TestMethod]
        public void Test_ToString3()
        {
            Combination cn = new Combination (3);

            string actual = cn.ToString();

            Assert.AreEqual ("{ 0, 1, 2 }", actual);
        }

        #endregion

        #region Test static methods

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void Test_Permute_ArgumentNullException1()
        {
            Combination cn = null;
            string[] letters = new string[] { "A", "B" };
            List<string> item = Combination.Permute (cn, letters);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void Test_Permute_ArgumentNullException2()
        {
            Combination cn = new Combination (2);
            string[] nullSource = null;
            List<string> item = Combination.Permute (cn, nullSource);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void Test_Permute_ArgumentException()
        {
            Combination cn = new Combination (6, 3);
            string[] letters = new string[] { "A", "B", "C", "D" };

            // Not enough letters throws
            List<string> item = Combination.Permute (cn, letters);
        }


        [TestMethod]
        public void Test_Permute()
        {
            string[] expected = new string[] { "ABC", "ABD", "ACD", "BCD" };
            string[] letters = new string[] { "A", "B", "C", "D" };

            int actualCount = 0;
            foreach (Combination row in new Combination (letters.Length, letters.Length - 1).GetRows())
            {
                string a = "";
                foreach (string item in Combination.Permute (row, letters))
                    a += item;

                Assert.AreEqual (expected[actualCount], a);
                ++actualCount;
            }

            Assert.AreEqual (expected.Length, actualCount);
        }

        
        [TestMethod]
        public void Test_ComparisonOps()
        {
            Combination c0 = null;
            Combination d0 = null;
            Combination c1 = new Combination (6, 3, 7);
            Combination c11 = new Combination (6, 3, 7);
            Combination c2 = new Combination (6, 3, 9);
            Combination c3 = new Combination (9, 2, 15);
            Combination c4 = new Combination (8, 2, 15);

            Assert.IsTrue (c0 == d0);
            Assert.IsFalse (c0 == c1);
            Assert.IsFalse (c1 == c0);
            Assert.IsTrue (c1 == c11);
            Assert.IsFalse (c1 == c2);

            Assert.IsFalse (c0 != d0);
            Assert.IsTrue (c0 != c1);
            Assert.IsTrue (c1 != c0);
            Assert.IsFalse (c1 != c11);
            Assert.IsTrue (c1 != c2);

            Assert.IsFalse (c0 < d0);
            Assert.IsTrue (c0 < c1);
            Assert.IsFalse (c1 < c0);
            Assert.IsFalse (c1 < c11);
            Assert.IsTrue (c1 < c2);
            Assert.IsFalse (c2 < c1);

            Assert.IsTrue (c0 >= d0);
            Assert.IsFalse (c0 >= c1);
            Assert.IsTrue (c1 >= c0);
            Assert.IsTrue (c1 >= c11);
            Assert.IsFalse (c1 >= c2);
            Assert.IsTrue (c2 >= c1);

            Assert.IsFalse (c0 > d0);
            Assert.IsFalse (c0 > c1);
            Assert.IsTrue (c1 > c0);
            Assert.IsFalse (c1 > c11);
            Assert.IsFalse (c1 > c2);
            Assert.IsTrue (c2 > c1);

            Assert.IsTrue (c0 <= d0);
            Assert.IsTrue (c0 <= c1);
            Assert.IsFalse (c1 <= c0);
            Assert.IsTrue (c1 <= c11);
            Assert.IsTrue (c1 <= c2);
            Assert.IsFalse (c2 <= c1);

            Assert.IsTrue (c3 < c1);
            Assert.IsTrue (c3 != c4);
        }

        #endregion
    }
}
