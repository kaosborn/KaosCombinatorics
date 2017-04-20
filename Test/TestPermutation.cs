using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kaos.Combinatorics;

namespace CombinatoricsTest
{
    [TestClass]
    public class TestPermutation
    {
        #region Test constructors

        [TestMethod]
        public void Test_Interfaces()
        {
            Permutation pn = new Permutation (7);

            Assert.IsNotNull (pn as IComparable);
            Assert.IsNotNull (pn as System.Collections.IEnumerable);
            Assert.IsNotNull (pn as IComparable<Permutation>);
            Assert.IsNotNull (pn as IEquatable<Permutation>);
            Assert.IsNotNull (pn as IEnumerable<int>);
        }


        [TestMethod]
        public void Test_Ctor0()
        {
            Permutation pn0 = new Permutation();
            Assert.AreEqual (0, pn0.Choices);
            Assert.AreEqual (0, pn0.Picks);
            Assert.AreEqual (0, pn0.Rank);
            Assert.AreEqual (0, pn0.RowCount);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void Test_Ctor1a_ArgumentNullException()
        {
            Permutation nullus = null;
            Permutation pn = new Permutation (nullus);
        }


        [TestMethod]
        public void Test_Ctor1a1()
        {
            int n = 9;

            Permutation pn0 = new Permutation (0);
            Permutation pnx = new Permutation (n);

            Permutation pn0b = new Permutation (pn0);
            Permutation pnxb = new Permutation (pnx);

            Assert.AreEqual (0, pn0b.RowCount);

            for (int ei = 0; ei < n; ++ei)
                Assert.AreEqual (ei, pnxb[ei]);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor1b_ArgumentOutOfRangeException1()
        {
            Permutation pn = new Permutation (-1);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor1b_ArgumentOutOfRangeException2()
        {
            Permutation pn = new Permutation (100);
        }


        [TestMethod]
        public void Test_Ctor1b0()
        {
            Permutation pn0 = new Permutation (0);
            Assert.AreEqual (0, pn0.Choices);
            Assert.AreEqual (0, pn0.Picks);
            Assert.AreEqual (0, pn0.Rank);
            Assert.AreEqual (0, pn0.RowCount);
        }


        [TestMethod]
        public void Test_Ctor1b()
        {
            for (int ei = 1; ei <= Permutation.MaxChoices; ++ei)
            {
                Permutation pn = new Permutation (ei);

                Assert.AreEqual (ei, pn.Choices);
                Assert.AreEqual (ei, pn.Picks);
                Assert.AreEqual (Combinatoric.Factorial (ei), pn.RowCount);

                for (int i = 0; i < pn.Choices; ++i)
                    Assert.AreEqual (i, pn[i]);
            }
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void Test_Ctor1c_ArgumentNullException()
        {
            int[] nullus = null;
            Permutation pn = new Permutation (nullus);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void Test_Ctor1c_ArgumentException1()
        {
            int[] source = new int[] { 0, 2, 1, 1 };
            Permutation pn = new Permutation (source);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void Test_Ctor1c_ArgumentException2()
        {
            int[] source = new int[] { 2, 1, 1 };
            Permutation pn = new Permutation (source);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor1c_ArgumentOutOfRangeException1()
        {
            int[] source = new int[] { 2, 0, 3 };
            Permutation pn = new Permutation (source);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor1c_ArgumentOutOfRangeException2()
        {
            int[] source = new int[] { -1, 0, 1 };
            Permutation pn = new Permutation (source);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor1c_ArgumentOutOfRangeException3()
        {
            int[] source = new int[Permutation.MaxChoices + 1];
            for (int i = 0; i < source.Length; ++i)
                source[i] = i;

            Permutation pn = new Permutation (source);
        }


        [TestMethod]
        public void Test_Ctor1c()
        {
            int[] source = new int[] { 2, 1, 0 };
            Permutation pn = new Permutation (source);

            for (int k = 0; k < source.Length; ++k)
                Assert.AreEqual (source[k], pn[k]);
        }


        [TestMethod]
        public void StressPermutation_Ctor1b()
        {
            // Use higher values for maxWidth for a long running test.
            int maxWidth = 8;
            long counter = 0;

            for (int w = 1; w <= maxWidth; ++w)
                for (long rank = 0; rank < Combinatoric.Factorial (w); ++rank)
                {
                    Permutation row1 = new Permutation (choices:w, picks:w, rank:rank);

                    int[] source = new int[w];
                    row1.CopyTo (source);

                    Permutation row2 = new Permutation (source);

                    for (int i = 0; i < w; ++i)
                        Assert.AreEqual (row1[i], row2[i]);

                    // verify that rank(unrank(x)) = x
                    Assert.AreEqual (rank, row1.Rank);
                    Assert.AreEqual (rank, row2.Rank);
                    ++counter;
                }
        }


        [TestMethod]
        public void Test_Ctor2a0()
        {
            Permutation pn0 = new Permutation (0, 0);

            Assert.AreEqual (0, pn0.Choices);
            Assert.AreEqual (0, pn0.Picks);
            Assert.AreEqual (0, pn0.Rank);
            Assert.AreEqual (0, pn0.RowCount);
        }


        [TestMethod]
        public void Test_Ctor2a1()
        {
            for (int ei = 1; ei <= Permutation.MaxChoices; ++ei)
            {
                Permutation pn = new Permutation (ei, ei);
                Assert.AreEqual (ei, pn.Choices);
                Assert.AreEqual (ei, pn.Picks);
                Assert.AreEqual (0, pn.Rank);
                Assert.AreEqual (Combinatoric.Factorial (ei), pn.RowCount);

                for (int i = 0; i < pn.Choices; ++i)
                    Assert.AreEqual (i, pn[i]);
            }

        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor2b_ArgumentOutOfRangeException()
        {
            var source = new int[Permutation.MaxChoices + 1];
            for (int i = 0; i < source.Length; ++i)
                source[i] = i;

            Permutation pn = new Permutation (source, Permutation.MaxChoices);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor2bArgumentOutOfRangeException()
        {
            int nn=99;
            int[] source = new int[] { 5, 4, 1, 0 };
            Permutation pn = new Permutation (source, nn);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void Test_Ctor2bArgumentNullException()
        {
            Permutation pn = new Permutation (null, 9);
        }


        [TestMethod]
        public void Test_Ctor2b0()
        {
            int n = 4;
            int[] source = new int[] { };

            Permutation pn = new Permutation (source, n);

            Assert.AreEqual (n, pn.Choices);
            Assert.AreEqual (0, pn.Picks);
            Assert.AreEqual (0, pn.Rank);
            Assert.AreEqual (0, pn.RowCount);
        }


        [TestMethod]
        public void Test_Ctor2b()
        {
            int nn=6;
            int[] source = new int[] { 5, 4, 1, 0 };
            Permutation pn = new Permutation (source, nn);

            Assert.AreEqual (nn, pn.Choices);
            Assert.AreEqual (source.Length, pn.Picks);

            for (int ii = 0; ii < source.Length; ++ii)
                Assert.AreEqual (source[ii], pn[ii]);

            Assert.AreEqual (351, pn.Rank);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor3ArgumentOutOfRangeException1a()
        {
            Permutation pn = new Permutation (-1, 1, 0);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor3ArgumentOutOfRangeException1b()
        {
            Permutation pn = new Permutation (1, -1, 0);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor3ArgumentOutOfRangeException2a()
        {
            Permutation pn = new Permutation (3, 5, 0);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor3ArgumentOutOfRangeException3a()
        {
            Permutation pn = new Permutation (5, 21, 0);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Ctor3ArgumentOutOfRangeException3b()
        {
            Permutation pn = new Permutation (21, 5, 0);
        }


        [TestMethod]
        public void Test_Ctor3a1()
        {
            int nn=6, kk=4;
            long rr = 351;

            Permutation pn = new Permutation (nn, kk, rr);

            Assert.AreEqual (kk, pn.Picks);
            Assert.AreEqual (nn, pn.Choices);
            Assert.AreEqual (rr, pn.Rank);
            Assert.AreEqual (5, pn[0]);
            Assert.AreEqual (4, pn[1]);
            Assert.AreEqual (1, pn[2]);
            Assert.AreEqual (0, pn[3]);
        }


        [TestMethod]
        public void Test_Ctor3a2()
        {
            Permutation pn1 = new Permutation (choices: 3, picks: 3, rank: 9);
            Assert.AreEqual (3, pn1.Rank);

            Permutation pn2 = new Permutation (choices: 3, picks: 3, rank: -1);
            Assert.AreEqual (5, pn2.Rank);
        }


        [TestMethod]
        public void Test_Ctor3a_LastRank()
        {
            for (int nn = 1; nn <= Permutation.MaxChoices; ++nn)
            {
                Permutation pn = new Permutation (nn, nn, -1);
                Assert.AreEqual (nn, pn.Choices);
                Assert.AreEqual (nn, pn.Picks);
                Assert.AreEqual (Combinatoric.Factorial (nn) - 1, pn.Rank);
                Assert.AreEqual (Combinatoric.Factorial (nn), pn.RowCount);

                for (int i = 0; i < pn.Choices; ++i)
                    Assert.AreEqual (nn - i - 1, pn[i]);
            }
        }


        #endregion

        #region Test properties

        [TestMethod]
        [ExpectedException (typeof (InvalidOperationException))]
        public void Test_PlainRankInvalidOperationException1()
        {
            var pn54 = new Permutation (choices:5, picks:4);
            pn54.PlainRank = 1;
        }


        [TestMethod]
        [ExpectedException (typeof (InvalidOperationException))]
        public void Test_PlainRankInvalidOperationException2()
        {
            var pn54 = new Permutation (choices: 5, picks: 4);
            long plainRank = pn54.PlainRank;
        }


        [TestMethod]
        public void Test_PlainRankGet()
        {
            var pn0 = new Permutation (0);
            var pn1 = new Permutation (1);
            var pn4 = new Permutation (4, 4, 23);

            long pr0 = pn0.PlainRank;
            long pr1 = pn1.PlainRank;
            long pr4 = pn4.PlainRank;

            Assert.AreEqual (0, pr0);
            Assert.AreEqual (0, pr1);
            Assert.AreEqual (12, pr4);
        }


        [TestMethod]
        public void Test_PlainRankSet()
        {
            var pn0 = new Permutation (0);
            var pn1 = new Permutation (1);
            var pn4 = new Permutation (4);
            var pn5 = new Permutation (5);

            pn0.PlainRank = -1;
            pn1.PlainRank = 4;
            pn4.PlainRank = pn4.RowCount + 1;
            pn5.PlainRank = -2;

            Assert.AreEqual (0, pn0.PlainRank);
            Assert.AreEqual (0, pn1.PlainRank);
            Assert.AreEqual (1, pn4.PlainRank);
            Assert.AreEqual (Combinatoric.Factorial (5) - 2, pn5.PlainRank);
        }


        [TestMethod]
        public void TestPlainRankMany()
        {
            var expected = new int[][][]
            {
              new int[][] { new int[] { } },
              new int[][] { new int[] { 0 } },
              new int[][] { new int[] { 0, 1 }, new int[] { 1, 0 } },
              new int[][]
              {
                new int[] { 0, 1, 2 }, new int[] { 0, 2, 1 }, new int[] { 2, 0, 1 },
                new int[] { 2, 1, 0 }, new int[] { 1, 2, 0 }, new int[] { 1, 0, 2 }
              },
              new int[][]
              {
                new int[] { 0, 1, 2, 3 }, new int[] { 0, 1, 3, 2 }, new int[] { 0, 3, 1, 2 },
                new int[] { 3, 0, 1, 2 }, new int[] { 3, 0, 2, 1 }, new int[] { 0, 3, 2, 1 },
                new int[] { 0, 2, 3, 1 }, new int[] { 0, 2, 1, 3 }, new int[] { 2, 0, 1, 3 },
                new int[] { 2, 0, 3, 1 }, new int[] { 2, 3, 0, 1 }, new int[] { 3, 2, 0, 1 },
                new int[] { 3, 2, 1, 0 }, new int[] { 2, 3, 1, 0 }, new int[] { 2, 1, 3, 0 },
                new int[] { 2, 1, 0, 3 }, new int[] { 1, 2, 0, 3 }, new int[] { 1, 2, 3, 0 },
                new int[] { 1, 3, 2, 0 }, new int[] { 3, 1, 2, 0 }, new int[] { 3, 1, 0, 2 },
                new int[] { 1, 3, 0, 2 }, new int[] { 1, 0, 3, 2 }, new int[] { 1, 0, 2, 3 }
              }
            };

            for (var n = 1; n < expected.Length; ++n)
            {
                var pn = new Permutation (n);
                Assert.AreEqual (expected[n].Length, pn.RowCount);

                for (var pr = 0; pr < Combinatoric.Factorial (n); ++pr)
                {
                    pn.PlainRank = pr;

                    for (var ei = 0; ei < n; ++ei)
                        Assert.AreEqual (expected[n][pr][ei], pn[ei]);
                }
            }
        }


        [TestMethod]
        public void Test_RankIncrement()
        {
            Permutation pn = new Permutation (choices:4, picks:4, rank:4);

            ++pn.Rank;
            Assert.AreEqual (5, pn.Rank);

            pn.Rank++;
            Assert.AreEqual (6, pn.Rank);
        }


        [TestMethod]
        public void Test_RankDecrement()
        {
            Permutation pn = new Permutation (choices:4, picks:4, rank:5);

            --pn.Rank;
            Assert.AreEqual (4, pn.Rank);

            pn.Rank--;
            Assert.AreEqual (3, pn.Rank);
        }


        [TestMethod]
        public void Test_Rank0()
        {
            Permutation pn = new Permutation();

            pn.Rank = 1;
            Assert.AreEqual (0, pn.Rank);

            pn.Rank = 2;
            Assert.AreEqual (0, pn.Rank);

            pn.Rank = -1;
            Assert.AreEqual (0, pn.Rank);
        }


        [TestMethod]
        public void Test_Rank1()
        {
            Permutation pn = new Permutation (1);

            pn.Rank = 1;
            Assert.AreEqual (0, pn.Rank);

            pn.Rank = 2;
            Assert.AreEqual (0, pn.Rank);

            pn.Rank = -1;
            Assert.AreEqual (0, pn.Rank);
        }


        [TestMethod]
        public void Test_Swaps1()
        {
            var pn0 = new Permutation();
            Assert.AreEqual (0, pn0.Swaps);

            var pn1 = new Permutation (1);
            Assert.AreEqual (0, pn1.Swaps);

            var pn2 = new Permutation (2);
            Assert.AreEqual (0, pn2.Swaps);

            var pn21 = new Permutation (2, 2, 1);
            Assert.AreEqual (1, pn21.Swaps);
        }


        [TestMethod]
        public void StressPermutation_Swaps()
        {
            // Use higher values for n for a long running test.
            for (int n = 1; n <= 7; ++n)
                for (int k = 1; k <= n; ++k)
                {
                    var pn = new Permutation (n, k);

                    foreach (var px in pn.GetRows())
                    {
                        int parity = GetBubbleSortSwaps (px) % 2;
                        int swaps = px.Swaps;

                        Assert.AreEqual (parity, swaps % 2);
                    }
                }
        }


        private static int GetBubbleSortSwaps (Permutation pn)
        {
            int result = 0;
            var val = new int[pn.Picks];
            pn.CopyTo (val);

            for (int j = 1; j < val.Length; ++j)
                for (int i = val.Length - 1; i >= j; --i)
                    if (val[i] < val[i-1])
                    {
                        var temp = val[i];
                        val[i] = val[i-1];
                        val[i-1] = temp;
                        ++result;
                    }

            return result;
        }
        
        #endregion

        #region Test instance methods

        [TestMethod]
        [ExpectedException (typeof (InvalidOperationException))]
        public void Test_Backtrack_InvalidOperationException()
        {
            var pn = new Permutation (choices:4, picks:2);
            pn.Backtrack (1);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Backtrack_ArgumentOutOfRangeException1()
        {
            var pn = new Permutation (2);
            pn.Backtrack (-1);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void Test_Backtrack_ArgumentOutOfRangeException2()
        {
            var pn = new Permutation (2);
            pn.Backtrack (2);
        }


        [TestMethod]
        public void Test_Backtrack1()
        {
            var pn1 = new Permutation (new int[] { 2, 1, 0 });
            var pn2 = new Permutation (new int[] { 2, 1, 0 });
            var pn3 = new Permutation (new int[] { 0, 1, 2 });
            var pn4 = new Permutation (new int[] { 0, 2, 1 });
            var pn5 = new Permutation (new int[] { 2, 3, 1, 0 });
            var pn6 = new Permutation (new int[] { 1, 4, 0, 2, 3 });

            int result1 = pn1.Backtrack (0);
            int result2 = pn2.Backtrack (0);
            int result3 = pn3.Backtrack (1);
            int result4 = pn4.Backtrack (1);
            int result5 = pn5.Backtrack (2);
            int result6 = pn6.Backtrack (1);

            Assert.AreEqual (-1, result1);
            Assert.AreEqual (-1, result2);
            Assert.AreEqual (1, result3);
            Assert.AreEqual (0, result4);
            Assert.AreEqual (0, result5);
            Assert.AreEqual (0, result6);

            Assert.AreEqual (new Permutation (new int[] { 0, 2, 1 }).Rank, pn3.Rank);
            Assert.AreEqual (new Permutation (new int[] { 1, 0, 2 }).Rank, pn4.Rank);
            Assert.AreEqual (new Permutation (new int[] { 3, 0, 1, 2 }).Rank, pn5.Rank);
            Assert.AreEqual (new Permutation (new int[] { 2, 0, 1, 3, 4 }).Rank, pn6.Rank);
        }



        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void Test_CopyTo_ArgumentNullException()
        {
            Permutation pn = new Permutation (choices:3, picks:3, rank:4);

            int[] nullTarget = null;
            pn.CopyTo (nullTarget);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void Test_CopyTo_ArgumentException()
        {
            Permutation pn = new Permutation (choices:3, picks:3, rank:4);

            int[] nullTarget = new int[2];
            pn.CopyTo (nullTarget);
        }


        [TestMethod]
        public void Test_CopyTo1()
        {
            Permutation pn = new Permutation (choices:3, picks:3, rank:5);

            int[] target = new int[3];
            pn.CopyTo (target);

            Assert.AreEqual (2, target[0]);
            Assert.AreEqual (1, target[1]);
            Assert.AreEqual (0, target[2]);
        }


        [TestMethod]
        public void Test_CompareToOBJECT()
        {
            var objectSortedList = new System.Collections.SortedList();
            objectSortedList.Add (new Permutation (choices:8, picks:8, rank:2), 0);
            objectSortedList.Add (new Permutation (choices:8, picks:8, rank:6), 2);
            objectSortedList.Add (new Permutation (choices:8, picks:8, rank:4), 1);

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
            Permutation p0 = null;
            Permutation p520 = new Permutation (choices:5, picks:5, rank:0);
            Permutation p521 = new Permutation (choices:5, picks:5, rank:1);

            int actual1 = p520.CompareTo (p0);
            Assert.IsTrue (actual1 > 0);

            int actual2 = p520.CompareTo (p521);
            Assert.IsTrue (actual2 < 0);
        }


        [TestMethod]
        public void Test_GetHashCode()
        {
            Permutation source = new Permutation (3);
            int hash = source.GetHashCode();
        }


        [TestMethod]
        public void Test_EqualsOBJECT()
        {
            Permutation p0 = null;
            Permutation p30a = new Permutation (3);
            Permutation p30b = new Permutation (3);
            Permutation p31 = new Permutation (choices:3, picks:3, rank:1);
            Permutation p4 = new Permutation (4);

            object j0 = (object) p0;
            object j30b = (object) p30b;
            object j31 = (object) p31;
            object j4 = (object) p4;

            Assert.IsFalse (p30a.Equals (j0));
            Assert.IsTrue (p30a.Equals (j30b));
            Assert.IsFalse (p30a.Equals (j31));
            Assert.IsFalse (p30a.Equals (j4));
        }


        [TestMethod]
        public void Test_Equals()
        {
            Permutation p0 = null;
            Permutation p30a = new Permutation (3);
            Permutation p30b = new Permutation (3);
            Permutation p31 = new Permutation (choices:3, picks:3, rank:1);
            Permutation p4 = new Permutation (4);

            Assert.IsFalse (p30a.Equals (p0));
            Assert.IsTrue (p30a.Equals (p30b));
            Assert.IsFalse (p30a.Equals (p31));
            Assert.IsFalse (p30a.Equals (p4));
        }


        [TestMethod]
        public void Test_EqualsOtherType()
        {
            var p54 = new Permutation (5, 4, 1);
            string s = "Mazzy";

            // Comparing to different type returns false.
            Assert.IsFalse (p54.Equals (s));
        }


        [TestMethod]
        public void Test_GetEnumeratorOBJECT()
        {
            Permutation perm = new Permutation (7);

            System.Collections.IEnumerator nu = ((System.Collections.IEnumerable) perm).GetEnumerator();

            int expected = 0;
            while (nu.MoveNext())
            {
                int actual = (int) nu.Current;
                Assert.AreEqual (expected, actual);
                ++expected;
            }

            Assert.AreEqual (perm.Choices, expected);
        }


        [TestMethod]
        public void Test_GetEnumerator()
        {
            int order = 6;
            Permutation p0 = new Permutation (choices:order, picks:order, rank:-1);

            int expectedValue = order;
            foreach (int actualValue in p0)
            {
                expectedValue--;
                Assert.AreEqual (expectedValue, actualValue);
            }

            Assert.AreEqual (0, expectedValue);
        }


        [TestMethod]
        public void Test_GetRowsEmpty()
        {
            Permutation source = new Permutation();

            int actualCount = 0;
            foreach (Permutation p in source.GetRows())
                ++actualCount;

            Assert.AreEqual (0, actualCount);
        }


        [TestMethod]
        public void Test_GetRows()
        {
            int order = 3;
            long startRank = 2;
            Permutation source = new Permutation (choices:order, picks:order, rank:startRank);

            long actualCount = 0;
            long? firstRank = null;
            long? lastRank = null;

            foreach (Permutation p in source.GetRows())
            {
                if (! firstRank.HasValue)
                    firstRank = p.Rank;

                ++actualCount;
                lastRank = p.Rank;
            }

            Assert.AreEqual (Combinatoric.Factorial (order), actualCount);
            Assert.AreEqual (startRank, firstRank);
            Assert.AreEqual (1, lastRank);
        }


        [TestMethod]
        public void Test_GetRowsForAllChoices()
        {
            int[][] expected = new int[][]
            {
                new int[] { 0 },
                new int[] { 0, 1 }, new int[] { 1, 0 },
                new int[] { 0, 1, 2 }, new int[] { 0, 2, 1 }, new int[] { 1, 0, 2 },
                new int[] { 1, 2, 0 }, new int[] { 2, 0, 1 }, new int[] { 2, 1, 0 }
            };

            int actualCount = 0;
            foreach (Permutation px in new Permutation (3).GetRowsForAllChoices())
            {
                int[] expectedSet = expected[actualCount];
                int exlen = expectedSet.Length;
                Assert.AreEqual (exlen, px.Choices);

                for (int i = 0; i < exlen; ++i)
                    Assert.AreEqual (expectedSet[i], px[i]);

                ++actualCount;
            }
            Assert.AreEqual (expected.Length, actualCount);
        }


        [TestMethod]
        public void Test_GetRowsForAllPicks()
        {
            int[][] expected = new int[][]
            {
                new int[] { 0 }, new int[] { 1 }, new int[] { 2 },
                new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 1, 0 },
                new int[] { 1, 2 }, new int[] { 2, 0 }, new int[] { 2, 1 },
                new int[] { 0, 1, 2 }, new int[] { 0, 2, 1 }, new int[] { 1, 0, 2 },
                new int[] { 1, 2, 0 }, new int[] { 2, 0, 1 }, new int[] { 2, 1, 0 }
            };

            int actualCount = 0;
            foreach (Permutation pn in new Permutation (3).GetRowsForAllPicks())
            {
                int[] expectedSet = expected[actualCount];
                int exlen = expectedSet.Length;
                Assert.AreEqual (exlen, pn.Picks);

                for (int i = 0; i < exlen; ++i)
                    Assert.AreEqual (expectedSet[i], pn[i]);

                ++actualCount;
            }
            Assert.AreEqual (expected.Length, actualCount);
        }


        [TestMethod]
        [ExpectedException (typeof (InvalidOperationException))]
        public void Test_GetRowsOfPlainChangesInvalidOperationException2()
        {
            var pn54 = new Permutation (choices: 5, picks: 4);

            foreach (var pn in pn54.GetRowsOfPlainChanges())
            { }
        }


        [TestMethod]
        public void Test_GetRowsOfPlainChanges0()
        {
            foreach (var pn in new Permutation (0).GetRowsOfPlainChanges())
            {
                Assert.Fail ("Unreachable");
            }
        }


        [TestMethod]
        public void Stress_GetRowsOfPlainChanges()
        {
            // Higher values of n may be used for stress, but will allocate more space.
            for (int n = 1; n < 8; ++n)
            {
                long rc = n == 0? 0 : Combinatoric.Factorial (n);
                var mat = new int[rc][];

                int pcRank = 0;
                foreach (var pn in new Permutation (n).GetRowsOfPlainChanges())
                {
                    mat[pcRank] = new int[n];
                    pn.CopyTo (mat[pcRank]);
                    ++pcRank;
                }

                var isUsed = new bool[rc];
                var lexPerm0 = new Permutation (mat[0]);
                isUsed[lexPerm0.Rank] = true;

                for (int rv = 1; rv < rc; ++rv)
                {
                    var lexPerm = new Permutation (mat[rv]);
                    Assert.IsFalse (isUsed[lexPerm.Rank], "row already used");
                    isUsed[lexPerm.Rank] = true;

                    int e;
                    for (e = 0; e < n; ++e)
                        if (mat[rv][e] != mat[rv-1][e])
                        {
                            Assert.AreNotEqual (e, n-1, "last element only change");
                            Assert.AreEqual (mat[rv][e], mat[rv-1][e+1], "invalid swap");
                            Assert.AreEqual (mat[rv][e+1], mat[rv-1][e], "invalid swap");
                            e += 2;
                            break;
                        }
                    for (; e < n; ++e)
                        Assert.AreEqual (mat[rv][e], mat[rv-1][e], "more than 1 swap");
                }
            }
        }


        [TestMethod]
        public void Test_ToString0()
        {
            Permutation permu = new Permutation();

            string actual = permu.ToString();

            Assert.AreEqual ("{ }", actual);
        }


        [TestMethod]
        public void Test_ToString1()
        {
            Permutation permu = new Permutation (1);

            string actual = permu.ToString();

            Assert.AreEqual ("{ 0 }", actual);
        }


        [TestMethod]
        public void Test_ToString3()
        {
            Permutation permu = new Permutation (3);

            string actual = permu.ToString();

            Assert.AreEqual ("{ 0, 1, 2 }", actual);
        }

        #endregion

        #region Test static methods

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void Test_Permute_ArgumentNullException1()
        {
            Permutation nullSource = null;
            string[] pattern = new string[] { "A", "B", "C" };
            List<string> target = Permutation.Permute (nullSource, pattern);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void Test_Permute_ArgumentNullException2()
        {
            Permutation source = new Permutation (3);
            string[] nullPattern = null;
            List<string> target = Permutation.Permute (source, nullPattern);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void Test_Permute_ArgumentException()
        {
            Permutation source = new Permutation (4);
            string[] pattern = new string[] { "A", "B", "C" };

            List<string> target = Permutation.Permute (source, pattern);
        }


        [TestMethod]
        public void Test_Permute()
        {
            Permutation p0 = new Permutation (3);
            string[] pat = new string[] { "A", "B", "C" };

            string[] expected = new string[] { "ABC", "ACB", "BAC", "BCA", "CAB", "CBA" };
            int x = 0;

            foreach (Permutation p in p0.GetRows())
            {
                List<string> applied = Permutation.Permute (p, pat);
                string actual = "";

                foreach (object j in applied)
                    actual += (string) j;

                Assert.AreEqual (expected[x], actual);
                ++x;
            }

            Assert.AreEqual (expected.Length, x);
        }


        [TestMethod]
        public void Test_ComparisonOps()
        {
            Permutation p0 = null;
            Permutation q0 = null;
            Permutation p1 = new Permutation (choices:3, picks:3, rank:1);
            Permutation p11 = new Permutation (choices:3, picks:3, rank:1);
            Permutation p2 = new Permutation (choices:3, picks:3, rank:2);
            Permutation q4 = new Permutation (choices:4, picks:4, rank:2);

            Assert.IsTrue (p0 == q0);
            Assert.IsFalse (p0 == p1);
            Assert.IsFalse (p1 == p0);
            Assert.IsTrue (p1 == p11);
            Assert.IsFalse (p1 == p2);

            Assert.IsFalse (p0 != q0);
            Assert.IsTrue (p0 != p1);
            Assert.IsTrue (p1 != p0);
            Assert.IsFalse (p1 != p11);
            Assert.IsTrue (p1 != p2);

            Assert.IsFalse (p0 < q0);
            Assert.IsTrue (p0 < p1);
            Assert.IsFalse (p1 < p0);
            Assert.IsFalse (p1 < p11);
            Assert.IsTrue (p1 < p2);
            Assert.IsFalse (p2 < p1);

            Assert.IsTrue (p0 >= q0);
            Assert.IsFalse (p0 >= p1);
            Assert.IsTrue (p1 >= p0);
            Assert.IsTrue (p1 >= p11);
            Assert.IsFalse (p1 >= p2);
            Assert.IsTrue (p2 >= p1);

            Assert.IsFalse (p0 > q0);
            Assert.IsFalse (p0 > p1);
            Assert.IsTrue (p1 > p0);
            Assert.IsFalse (p1 > p11);
            Assert.IsFalse (p1 > p2);
            Assert.IsTrue (p2 > p1);

            Assert.IsTrue (p0 <= q0);
            Assert.IsTrue (p0 <= p1);
            Assert.IsFalse (p1 <= p0);
            Assert.IsTrue (p1 <= p11);
            Assert.IsTrue (p1 <= p2);
            Assert.IsFalse (p2 <= p1);

            Assert.IsTrue (p2 < q4);
            Assert.IsTrue (p2 != q4);
        }

        #endregion
    }
}
