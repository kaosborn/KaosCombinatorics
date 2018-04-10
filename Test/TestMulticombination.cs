using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kaos.Combinatorics;

namespace CombinatoricsTest
{
    [TestClass]
    public class TestMulticombination
    {
        #region Test constructors

        [TestMethod]
        public void Test_Interfaces()
        {
            Multicombination mc = new Multicombination (4, 2);

            Assert.IsNotNull (mc as IComparable);
            Assert.IsNotNull (mc as System.Collections.IEnumerable);
            Assert.IsNotNull (mc as IComparable<Multicombination>);
            Assert.IsNotNull (mc as IEquatable<Multicombination>);
            Assert.IsNotNull (mc as IEnumerable<int>);
        }


        [TestMethod]
        public void Test_Ctor0()
        {
            Multicombination mc = new Multicombination();

            Assert.AreEqual (0, mc.Choices);
            Assert.AreEqual (0, mc.Picks);
            Assert.AreEqual (0, mc.Rank);
            Assert.AreEqual (0, mc.RowCount);

            mc.Rank = 1;
            Assert.AreEqual (0, mc.Rank);

            mc.Rank = -1;
            Assert.AreEqual (0, mc.Rank);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void Test_Ctor1a_ArgumentNullException()
        {
            Multicombination nullSource = null;
            Multicombination mc = new Multicombination (nullSource);
        }


        [TestMethod]
        public void Test_Ctor1a()
        {
            Multicombination smc = new Multicombination (5, 3, 1);

            Multicombination mc = new Multicombination (smc);

            Assert.AreEqual (5, mc.Choices);
            Assert.AreEqual (3, mc.Picks);
            Assert.AreEqual (1, mc.Rank);
            Assert.AreEqual (35, mc.RowCount);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor1b_ArgumentOutOfRangeException()
        {
            int n = -1;
            Multicombination mc = new Multicombination (n);
        }


        [TestMethod]
        public void Test_Ctor1b1()
        {
            int n = 3;
            Multicombination mc = new Multicombination (n);

            Assert.AreEqual (n, mc.Choices);
            Assert.AreEqual (n, mc.Picks);
            Assert.AreEqual (0, mc.Rank);
            Assert.AreEqual (10, mc.RowCount);
        }


        [TestMethod]
        public void Test_Ctor1b2()
        {
            int n = 0;
            Multicombination mc = new Multicombination (n);

            Assert.AreEqual (n, mc.Choices);
            Assert.AreEqual (n, mc.Picks);
            Assert.AreEqual (0, mc.Rank);
            Assert.AreEqual (0, mc.RowCount);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor2a_ArgumentOutOfRangeException1()
        {
            Multicombination mc = new Multicombination (-2, 3);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor2a_ArgumentOutOfRangeException2a()
        {
            Multicombination mc = new Multicombination (2, -3);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor2a_ArgumentOutOfRangeException2b()
        {
            Multicombination mc = new Multicombination (0, 1);
        }


        [TestMethod]
        public void Test_Ctor2a1()
        {
            int n = 5;
            int k = 0;

            Multicombination mc = new Multicombination (n, k);

            Assert.AreEqual (n, mc.Choices);
            Assert.AreEqual (k, mc.Picks);
            Assert.AreEqual (0, mc.Rank);
            Assert.AreEqual (0, mc.RowCount);
        }


        [TestMethod]
        public void Test_Ctor2a2()
        {
            int n = 5;
            int k = 3;

            Multicombination mc53 = new Multicombination (n, k);

            Assert.AreEqual (n, mc53.Choices);
            Assert.AreEqual (k, mc53.Picks);
            Assert.AreEqual (0, mc53.Rank);
            Assert.AreEqual (35, mc53.RowCount);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void Test_Ctor2b_ArgumentNullException()
        {
            int[] nullus = null;
            Multicombination mc = new Multicombination (1, nullus);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor2b_ArgumentOutOfRangeException1()
        {
            int[] array = new int[1];

            Multicombination mc = new Multicombination (-1, array);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor2b_ArgumentOutOfRangeException4()
        {
            int[] array = new int[] { -1, 0 };
            Multicombination mc = new Multicombination (2, array);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor2b_ArgumentOutOfRangeException5()
        {
            int[] array = new int[] { 1 };
            Multicombination mc = new Multicombination (1, array);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor2b_ArgumentOutOfRangeException6()
        {
            int[] array = new int[] { 0 };
            Multicombination mc = new Multicombination (0, array);
        }


        [TestMethod]
        public void Test_Ctor2b_ChoicesWithoutPicks()
        {
            int[] array = new int[] { };
            Multicombination mc = new Multicombination (1, array);
        }


        [TestMethod]
        public void Test_Ctor2b1_Empty()
        {
            int[] array = new int[] { };

            Multicombination mc = new Multicombination (0, array);

            Assert.AreEqual (0, mc.RowCount);
        }


        [TestMethod]
        public void Test_Ctor2b1()
        {
            int n = 4;
            int[] mcVals1 = new int[] { 1, 0, 0 };

            Multicombination mc1 = new Multicombination (n, mcVals1);

            Assert.AreEqual (n, mc1.Choices);
            Assert.AreEqual (mcVals1.Length, mc1.Picks);
            Assert.AreEqual (1, mc1.Rank);
            Assert.AreEqual (20, mc1.RowCount);
        }


        [TestMethod]
        public void Test_Ctor2b2()
        {
            int n = 5;
            int[] mcVals1 = new int[] { 3, 3, 2, 1, 2, 0 };

            Multicombination mc1 = new Multicombination (n, mcVals1);

            Array.Sort (mcVals1);

            Assert.AreEqual (n, mc1.Choices);
            Assert.AreEqual (mcVals1.Length, mc1.Picks);
            Assert.AreEqual (210, mc1.RowCount);
            Assert.AreEqual (93, mc1.Rank);

            for (int ii = 0; ii < mcVals1.Length; ++ii)
                Assert.AreEqual (mcVals1[ii], mc1[ii]);
        }


        [TestMethod]
        public void StressMulticombination_Ctor2b()
        {
            // Use higher maxChoices values for a longer running test.
#if STRESS
            const int maxChoices = 7;
#else
            const int maxChoices = 4;
#endif
            int counter = 0;

            for (int choices = 1; choices <= maxChoices; ++choices)
                for (int picks = 0; picks <= choices + 2; ++picks)
                {
                    long maxRank = picks == 0? 0 : Combinatoric.BinomialCoefficient (picks + choices - 1, picks);

                    for (long rank = 0; rank < maxRank; ++rank)
                    {
                        Multicombination row1 = new Multicombination (choices, picks, rank);

                        int[] source = new int[picks];
                        row1.CopyTo (source);

                        Multicombination row2 = new Multicombination (choices, source);

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
            Multicombination mc = new Multicombination (-2, 3, 1);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor3a_ArgumentOutOfRangeException2()
        {
            Multicombination mc = new Multicombination (2, -3, 0);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor3a_ArgumentOutOfRangeException3()
        {
            Multicombination mc = new Multicombination (0, 1, 1);
        }


        [TestMethod]
        public void Test_Ctor3a3a()
        {
            Multicombination mc = new Multicombination (0, 0, 1);
            Assert.AreEqual (0, mc.Rank);
            Assert.AreEqual (0, mc.RowCount);
        }


        [TestMethod]
        public void Test_Ctor3a3b()
        {
            Multicombination mc = new Multicombination (4, 0, 9);

            Assert.AreEqual (0, mc.Rank);
            Assert.AreEqual (0, mc.RowCount);
        }


        [TestMethod]
        public void Test_Ctor3a3c()
        {
            Multicombination mc = new Multicombination (4, 3, 14);

            Assert.AreEqual (1, mc[0]);
            Assert.AreEqual (2, mc[1]);
            Assert.AreEqual (3, mc[2]);
            Assert.AreEqual (14, mc.Rank);
            Assert.AreEqual (20, mc.RowCount);
        }


        #endregion

        #region Test properties

        [TestMethod]
        public void Test_Rank0()
        {
            Multicombination mc = new Multicombination();

            mc.Rank = 1;
            Assert.AreEqual (0, mc.Rank);

            mc.Rank = 2;
            Assert.AreEqual (0, mc.Rank);

            mc.Rank = -1;
            Assert.AreEqual (0, mc.Rank);
        }


        [TestMethod]
        public void Test_Rank1()
        {
            Multicombination mc = new Multicombination (1);

            mc.Rank = 1;
            Assert.AreEqual (0, mc.Rank);

            mc.Rank = 2;
            Assert.AreEqual (0, mc.Rank);

            mc.Rank = -1;
            Assert.AreEqual (0, mc.Rank);
        }


        [TestMethod]
        public void Test_Rank2()
        {
            Multicombination mc = new Multicombination (6, 4);

            mc.Rank = 1;
            Assert.AreEqual (1, mc.Rank);

            mc.Rank = 2;
            Assert.AreEqual (2, mc.Rank);

            mc.Rank = -1;
            Assert.AreEqual (125, mc.Rank);
        }


        [TestMethod]
        public void Test_RankIncrement()
        {
            Multicombination mc = new Multicombination (4, 3, 1);

            ++mc.Rank;
            Assert.AreEqual (2, mc.Rank);

            mc.Rank++;
            Assert.AreEqual (3, mc.Rank);
        }


        [TestMethod]
        public void Test_RankDecrement()
        {
            Multicombination mc = new Multicombination (4, 3, 1);

            --mc.Rank;
            Assert.AreEqual (0, mc.Rank);

            mc.Rank--;
            Assert.AreEqual (19, mc.Rank);
        }

        #endregion

        #region Test instance methods

        [TestMethod]
        public void Test_CompareToOBJECT()
        {
            var objectSortedList = new System.Collections.SortedList();
            int[][] pairs = new int[][] { new int[] { 16, 2 }, new int[] { 12, 0 }, new int[] { 14, 1 } };

            // Act: Adding to an object based SortedList forces comparison by Object.
            foreach (int[] pair in pairs)
                objectSortedList.Add (new Multicombination (5, 3, pair[0]), pair[1]);

            int position = 0;
            foreach (System.Collections.DictionaryEntry item in objectSortedList)
            {
                Assert.AreEqual (position, (int) item.Value);
                ++position;
            }

            Assert.AreEqual (pairs.Length, position);
        }


        [TestMethod]
        public void Test_CompareTo()
        {
            Multicombination mcNull = null;
            Multicombination mc520 = new Multicombination (5, 2, 0);
            Multicombination mc521 = new Multicombination (5, 2, 1);

            int actual1 = mc520.CompareTo (mcNull);
            Assert.IsTrue (actual1 > 0);

            int actual2 = mc520.CompareTo (mc521);
            Assert.IsTrue (actual2 < 0);

            int actual3 = mc520.CompareTo (mc520);
            Assert.AreEqual (0, actual3);

            int actual4 = mc521.CompareTo (mc520);
            Assert.IsTrue (actual4 > 0);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void Test_CopyTo_ArgumentNullException()
        {
            Multicombination mc = new Multicombination (3, 3, 4);
            
            int[] nullTarget = null;
            mc.CopyTo (nullTarget);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void Test_CopyTo_ArgumentException()
        {
            Multicombination mc = new Multicombination (3, 3, 4);

            int[] nullTarget = new int[2];
            mc.CopyTo (nullTarget);
        }


        [TestMethod]
        public void Test_CopyTo1()
        {
            Multicombination mc3 = new Multicombination (3, 3, 4);
            
            int[] target = new int[3];
            mc3.CopyTo (target);

            Assert.AreEqual (0, target[0]);
            Assert.AreEqual (1, target[1]);
            Assert.AreEqual (2, target[2]);
        }


        [TestMethod]
        public void Test_EqualsOBJECT()
        {
            Multicombination mcNull = null;
            Multicombination mc438a = new Multicombination (4, 3, 8);
            Multicombination mc438b = new Multicombination (4, 3, 8);
            Multicombination mc439 = new Multicombination (4, 3, 9);
            Multicombination mc864 = new Multicombination (8, 6, 4);

            object j0 = (object) mcNull;
            object j438b = (object) mc438b;
            object j439 = (object) mc439;
            object j864 = (object) mc864;

            Assert.IsFalse (mc438a.Equals (j0));
            Assert.IsTrue (mc438a.Equals (j438b));
            Assert.IsFalse (mc438a.Equals (j439));
            Assert.IsFalse (mc438a.Equals (j864));
        }


        [TestMethod]
        public void Test_Equals()
        {
            Multicombination nullMc = null;
            Multicombination c438a = new Multicombination (4, 3, 8);
            Multicombination c438b = new Multicombination (4, 3, 8);
            Multicombination c439 = new Multicombination (4, 3, 9);
            Multicombination c6 = new Multicombination (8, 6, 4);

            Assert.IsFalse (c438a.Equals (nullMc));
            Assert.IsTrue (c438a.Equals (c438b));
            Assert.IsFalse (c438a.Equals (c439));
            Assert.IsFalse (c438a.Equals (c6));
        }


        [TestMethod]
        public void Test_GetEnumeratorOBJECT()
        {
            int picks = 3;
            Multicombination mc = new Multicombination (5, picks, 20);
            int[] expectedVals = new int[] { 1, 2, 3 };

            System.Collections.IEnumerator nu = ((System.Collections.IEnumerable) mc).GetEnumerator();

            int rowNum = 0;
            while (nu.MoveNext())
            {
                int actual = (int) nu.Current;
                Assert.AreEqual (expectedVals[rowNum], actual);
                ++rowNum;
            }

            Assert.AreEqual (picks, rowNum);
        }


        [TestMethod]
        public void Test_GetEnumerator()
        {
            int picks = 3;
            Multicombination mc = new Multicombination (5, picks, 20);
            int[] expectedVals = new int[] { 1, 2, 3 };

            int expectedElement = 0;
            int rowNum = 0;
            foreach (int actualElement in mc)
            {
                Assert.AreEqual (expectedVals[rowNum], actualElement);
                ++rowNum;
                ++expectedElement;
            }

            Assert.AreEqual (picks, expectedElement);
        }


        [TestMethod]
        public void Test_GetHash()
        {
            Multicombination mc5 = new Multicombination (5);
            int hash = mc5.GetHashCode();
        }


        [TestMethod]
        public void Test_GetRows0Empty()
        {
            Multicombination mc = new Multicombination();

            foreach (Multicombination row in mc.GetRows())
            {
                Assert.Fail ("Empty multicombo iterates");
            }
        }


        [TestMethod]
        public void Test_GetRows0()
        {
            int[,] expected = new int[,]
            {
                { 0, 0, 0, 0 },
                { 0, 0, 0, 1 },
                { 0, 0, 0, 2 },
                { 0, 0, 1, 1 },
                { 0, 0, 1, 2 },
                { 0, 0, 2, 2 },
                { 0, 1, 1, 1 },
                { 0, 1, 1, 2 },
                { 0, 1, 2, 2 },
                { 0, 2, 2, 2 },
                { 1, 1, 1, 1 },
                { 1, 1, 1, 2 },
                { 1, 1, 2, 2 },
                { 1, 2, 2, 2 },
                { 2, 2, 2, 2 }
            };

            Multicombination mc = new Multicombination (3, 4);

            long actualCount = 0;
            foreach (Multicombination row in mc.GetRows())
            {
                Assert.IsTrue (actualCount < row.RowCount);

                for (int k = 0; k < row.Picks; ++k)
                    Assert.AreEqual (expected[actualCount, k], row[k]);

                ++actualCount;
            }

            Assert.AreEqual (mc.RowCount, actualCount);
        }


        [TestMethod]
        [ExpectedException (typeof(ArgumentOutOfRangeException))]
        public void Test_GetRowsForPicks_ArgumentOutOfRangeException1()
        {
            Multicombination mc2 = new Multicombination (2, 3);

            foreach (Multicombination row in mc2.GetRowsForPicks (-1, 2))
            {
            }
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_GetRowsForPicks_ArgumentOutOfRangeException2()
        {
            Multicombination mc2 = new Multicombination (2, 3);

            foreach (Multicombination row in mc2.GetRowsForPicks (2, 1))
            {
            }
        }


        [TestMethod]
        public void Test_GetRowsForPicksEmpty1()
        {
            Multicombination mc0 = new Multicombination (0, 0);

            foreach (Multicombination row in mc0.GetRowsForPicks (1, 2))
            {
                Assert.Fail ("Enumeration should be empty");
            }
        }


        [TestMethod]
        public void Test_GetRowsForPicksEmpty2()
        {
            Multicombination mc2 = new Multicombination (2, 3);

            foreach (Multicombination row in mc2.GetRowsForPicks (0, 0))
            {
                Assert.Fail ("Enumeration should be empty");
            }
        }


        [TestMethod]
        public void Test_GetRowsForPicks1()
        {
            Multicombination mc2 = new Multicombination (2, 0);

            int counter = 0;
            foreach (Multicombination row in mc2.GetRowsForPicks (1, 2))
                ++counter;

            Assert.AreEqual (5, counter);
        }


        [TestMethod]
        public void Test_GetRowsForPicks2()
        {
            Multicombination mc2 = new Multicombination (2, 5);

            int counter = 0;
            foreach (Multicombination row in mc2.GetRowsForPicks (0, 2))
                ++counter;

            Assert.AreEqual (5, counter);
        }


        [TestMethod]
        public void Test_GetRowsForPicks3()
        {
            int[][] expected = new int[][]
                { new int[] { 0 }, new int[] { 1 },
                  new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 1, 1 },
                  new int[] { 0, 0, 0}, new int[] { 0, 0, 1 }, new int[] { 0, 1, 1 }, new int[] { 1, 1, 1 }
                };

            Multicombination mc2 = new Multicombination (2);

            int counter = 0;
            foreach (Multicombination row in mc2.GetRowsForPicks (1, 3))
            {
                int[] expectedList = expected[counter];

                Assert.AreEqual (expectedList.Length, row.Picks);

                for (int i = 0; i < row.Picks; ++i)
                {
                    Assert.AreEqual (expectedList[i], row[i]);
                }
                ++counter;
            }

            Assert.AreEqual (expected.Length, counter);
        }


        [TestMethod]
        public void Test_ToString0()
        {
            Multicombination mc = new Multicombination();

            string actual = mc.ToString();

            Assert.AreEqual ("{ }", actual);
        }


        [TestMethod]
        public void Test_ToString1()
        {
            Multicombination mc = new Multicombination (1);

            string actual = mc.ToString();

            Assert.AreEqual ("{ 0 }", actual);
        }


        [TestMethod]
        public void Test_ToString3()
        {
            Multicombination mc = new Multicombination (5, 3, 20);

            string actual = mc.ToString();

            Assert.AreEqual ("{ 1, 2, 3 }", actual);
        }


        #endregion

        #region Test static methods

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void Test_Permute_ArgumentNullException1()
        {
            Multicombination mcNull = null;
            string[] letters = new string[] { "A", "B" };
            List<string> item = Multicombination.Permute (mcNull, letters);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void Test_Permute_ArgumentNullException2()
        {
            Multicombination mc = new Multicombination (2);
            string[] nullSource = null;
            List<string> item = Multicombination.Permute (mc, nullSource);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void Test_Permute_ArgumentException()
        {
            Multicombination mc = new Multicombination (6);
            string[] letters = new string[] { "A", "B", "C", "D" };

            // Not enough letters throws
            List<string> item = Multicombination.Permute (mc, letters);
        }


        [TestMethod]
        public void Test_Permute()
        {
            string[] expected = new string[] { "AAA", "AAB", "ABB", "BBB" };
            string[] letters = new string[] { "A", "B" };

            int actualCount = 0;
            foreach (Multicombination mc in new Multicombination (2, 3).GetRows())
            {
                string a = "";
                foreach (string item in Multicombination.Permute (mc, letters))
                    a += item;

                Assert.AreEqual (expected[actualCount], a);
                ++actualCount;
            }

            Assert.AreEqual (expected.Length, actualCount);
        }


        [TestMethod]
        public void Test_ComparisonOps()
        {
            Multicombination c0 = null;
            Multicombination d0 = null;
            Multicombination c1 = new Multicombination (6, 3, 7);
            Multicombination c11 = new Multicombination (6, 3, 7);
            Multicombination c2 = new Multicombination (6, 3, 9);
            Multicombination c3 = new Multicombination (9, 2, 15);
            Multicombination c4 = new Multicombination (8, 2, 15);

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
