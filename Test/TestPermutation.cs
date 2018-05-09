using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kaos.Combinatorics;

namespace CombinatoricsTest
{
    [TestClass]
    public class TestPermutation
    {
        #region Support methods

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

        #region Test constructors

        [TestMethod]
        public void UnitPn_Inheritance()
        {
            Permutation pn = new Permutation (7);

            Assert.IsNotNull (pn as IComparable);
            Assert.IsNotNull (pn as System.Collections.IEnumerable);
            Assert.IsNotNull (pn as IComparable<Permutation>);
            Assert.IsNotNull (pn as IEquatable<Permutation>);
            Assert.IsNotNull (pn as IEnumerable<int>);
        }


        [TestMethod]
        public void UnitPn_Ctor0()
        {
            Permutation pn0 = new Permutation();
            Assert.AreEqual (0, pn0.Choices);
            Assert.AreEqual (0, pn0.Picks);
            Assert.AreEqual (0, pn0.Rank);
            Assert.AreEqual (0, pn0.RowCount);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CrashPn_Ctor1A_ArgumentNull()
        {
            Permutation nullus = null;
            Permutation pn = new Permutation (nullus);
        }


        [TestMethod]
        public void UnitPn_Ctor1A1()
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
        public void CrashPn_Ctor1B_ArgumentOutOfRange1()
        {
            Permutation pn = new Permutation (-1);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPn_Ctor1B_ArgumentOutOfRange2()
        {
            Permutation pn = new Permutation (21);
        }


        [TestMethod]
        public void UnitPn_Ctor1B0()
        {
            Permutation pn0 = new Permutation (0);
            Assert.AreEqual (0, pn0.Choices);
            Assert.AreEqual (0, pn0.Picks);
            Assert.AreEqual (0, pn0.Rank);
            Assert.AreEqual (0, pn0.RowCount);
        }


        [TestMethod]
        public void UnitPn_Ctor1B()
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
        public void CrashPn_Ctor1C_ArgumentNull()
        {
            int[] nullus = null;
            Permutation pn = new Permutation (nullus);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CrashPn_Ctor1C_Argument1()
        {
            int[] source = new int[] { 0, 2, 1, 1 };
            Permutation pn = new Permutation (source);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CrashPn_Ctor1C_Argument2()
        {
            int[] source = new int[] { 2, 1, 1 };
            Permutation pn = new Permutation (source);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPn_Ctor1C_ArgumentOutOfRange1()
        {
            int[] source = new int[] { 2, 0, 3 };
            Permutation pn = new Permutation (source);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPn_Ctor1C_ArgumentOutOfRange2()
        {
            int[] source = new int[] { -1, 0, 1 };
            Permutation pn = new Permutation (source);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPn_Ctor1C_ArgumentOutOfRange3()
        {
            int[] source = new int[Permutation.MaxChoices + 1];
            for (int i = 0; i < source.Length; ++i)
                source[i] = i;

            Permutation pn = new Permutation (source);
        }


        [TestMethod]
        public void UnitPn_Ctor1C()
        {
            int[] source = new int[] { 2, 1, 0 };
            Permutation pn = new Permutation (source);

            for (int k = 0; k < source.Length; ++k)
                Assert.AreEqual (source[k], pn[k]);
        }


        [TestMethod]
        public void StressPn_Ctor1B()
        {
            // Use higher maxWidth values for a longer running test.
#if STRESS
            const int maxWidth = 8;
#else
            const int maxWidth = 5;
#endif
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
        public void UnitPn_Ctor2A0()
        {
            Permutation pn0 = new Permutation (0, 0);

            Assert.AreEqual (0, pn0.Choices);
            Assert.AreEqual (0, pn0.Picks);
            Assert.AreEqual (0, pn0.Rank);
            Assert.AreEqual (0, pn0.RowCount);
        }


        [TestMethod]
        public void UnitPn_Ctor2A1()
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
        public void CrashPn_Ctor2B_ArgumentOutOfRange1()
        {
            var source = Enumerable.Range (0, Permutation.MaxChoices+1).ToArray();
            Permutation pn = new Permutation (source);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPn_Ctor2B_ArgumentOutOfRange2()
        {
            int nn=99;
            int[] source = new int[] { 5, 4, 1, 0 };
            Permutation pn = new Permutation (source, nn);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPn_Ctor2B_ArgumentOutOfRange3()
        {
            var source = new int[] { 3, 2, 1, 0 };
            Permutation pn = new Permutation (source, -1);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CrashPn_Ctor2B_ArgumentNull()
        {
            Permutation pn = new Permutation (null, 9);
        }


        [TestMethod]
        public void UnitPn_Ctor2B0()
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
        public void UnitPn_Ctor2B()
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
        public void CrashPn_Ctor3_ArgumentOutOfRange1A()
        {
            Permutation pn = new Permutation (-1, 1, 0);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPn_Ctor3_ArgumentOutOfRange1B()
        {
            Permutation pn = new Permutation (1, -1, 0);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPn_Ctor3_ArgumentOutOfRange2A()
        {
            Permutation pn = new Permutation (3, 5, 0);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPn_Ctor3_ArgumentOutOfRange3A()
        {
            Permutation pn = new Permutation (5, 21, 0);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPn_Ctor3_ArgumentOutOfRange3B()
        {
            Permutation pn = new Permutation (21, 5, 0);
        }


        [TestMethod]
        public void UnitPn_Ctor3A1()
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
        public void UnitPn_Ctor3A2()
        {
            Permutation pn1 = new Permutation (choices: 3, picks: 3, rank: 9);
            Assert.AreEqual (3, pn1.Rank);

            Permutation pn2 = new Permutation (choices: 3, picks: 3, rank: -1);
            Assert.AreEqual (5, pn2.Rank);
        }


        [TestMethod]
        public void UnitPn_Ctor3ALastRank()
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
        public void CrashPn_PlainRank_InvalidOperation1()
        {
            var pn54 = new Permutation (choices:5, picks:4);
            pn54.PlainRank = 1;
        }


        [TestMethod]
        [ExpectedException (typeof (InvalidOperationException))]
        public void CrashPn_PlainRank_InvalidOperation2()
        {
            var pn54 = new Permutation (choices: 5, picks: 4);
            long plainRank = pn54.PlainRank;
        }


        [TestMethod]
        public void UnitPn_PlainRankGet()
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
        public void UnitPn_PlainRankSet()
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

            pn5.PlainRank = pn5.RowCount * -2 - 1;
            Assert.AreEqual (pn5.RowCount-1, pn5.PlainRank);

            pn5.PlainRank = pn5.RowCount * -2;
            Assert.AreEqual (0, pn5.PlainRank);

            pn5.PlainRank = pn5.RowCount * -2 + 1;
            Assert.AreEqual (1, pn5.PlainRank);
        }


        [TestMethod]
        public void UnitPn_PlainRankMany()
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
        public void UnitPn_RankIncrement()
        {
            Permutation pn = new Permutation (choices:4, picks:4, rank:4);

            ++pn.Rank;
            Assert.AreEqual (5, pn.Rank);

            pn.Rank++;
            Assert.AreEqual (6, pn.Rank);
        }


        [TestMethod]
        public void UnitPn_RankDecrement()
        {
            Permutation pn = new Permutation (choices:4, picks:4, rank:5);

            --pn.Rank;
            Assert.AreEqual (4, pn.Rank);

            pn.Rank--;
            Assert.AreEqual (3, pn.Rank);
        }


        [TestMethod]
        public void UnitPn_Rank0()
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
        public void UnitPn_Rank1()
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
        public void UnitPn_Rank4()
        {
            Permutation pn = new Permutation (4);

            pn.Rank = 23;
            Assert.AreEqual (23, pn.Rank);

            pn.Rank = 24;
            Assert.AreEqual (0, pn.Rank);

            pn.Rank = -23;
            Assert.AreEqual (1, pn.Rank);

            pn.Rank = -24;
            Assert.AreEqual (0, pn.Rank);

            pn.Rank = -25;
            Assert.AreEqual (23, pn.Rank);

            pn.Rank = -47;
            Assert.AreEqual (1, pn.Rank);

            pn.Rank = -48;
            Assert.AreEqual (0, pn.Rank);

            pn.Rank = -97;
            Assert.AreEqual (23, pn.Rank);
        }


        [TestMethod]
        public void UnitPn_Swaps1()
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
        public void StressPn_Swaps()
        {
            // Use higher maxWidth values for a longer running test.
#if STRESS
            const int maxWidth = 7;
#else
            const int maxWidth = 5;
#endif
            for (int n = 1; n <= maxWidth; ++n)
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

        #endregion

        #region Test instance methods

        [TestMethod]
        [ExpectedException (typeof (InvalidOperationException))]
        public void CrashPn_Backtrack_InvalidOperation()
        {
            var pn = new Permutation (choices:4, picks:2);
            pn.Backtrack (1);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPn_Backtrack_ArgumentOutOfRange1()
        {
            var pn = new Permutation (2);
            pn.Backtrack (-1);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPn_Backtrack_ArgumentOutOfRange2()
        {
            var pn = new Permutation (2);
            pn.Backtrack (2);
        }


        [TestMethod]
        public void UnitPn_Backtrack1()
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
        public void CrashPn_CopyTo_ArgumentNull()
        {
            Permutation pn = new Permutation (choices:3, picks:3, rank:4);

            int[] nullTarget = null;
            pn.CopyTo (nullTarget);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CrashPn_CopyTo_Argument()
        {
            Permutation pn = new Permutation (choices:3, picks:3, rank:4);

            int[] nullTarget = new int[2];
            pn.CopyTo (nullTarget);
        }


        [TestMethod]
        public void UnitPn_CopyTo1()
        {
            Permutation pn = new Permutation (choices:3, picks:3, rank:5);

            int[] target = new int[3];
            pn.CopyTo (target);

            Assert.AreEqual (2, target[0]);
            Assert.AreEqual (1, target[1]);
            Assert.AreEqual (0, target[2]);
        }


        [TestMethod]
        public void UnitPn_oCompareTo()
        {
            Permutation p0 = null;
            var p874 = new Permutation (8, 7, 4);
            var p882 = new Permutation (8, 8, 2);
            var p884 = new Permutation (8, 8, 4);
            var p984 = new Permutation (9, 8, 4);

            Assert.IsTrue (p884.CompareTo ((object) p0) > 0);
            Assert.IsTrue (p884.CompareTo ((object) p874) > 0);
            Assert.IsTrue (p874.CompareTo ((object) p884) < 0);
            Assert.IsTrue (p884.CompareTo ((object) p882) > 0);
            Assert.IsTrue (p882.CompareTo ((object) p884) < 0);
            Assert.IsTrue (p884.CompareTo ((object) p984) < 0);
            Assert.IsTrue (p984.CompareTo ((object) p884) > 0);
            Assert.IsTrue (p984.CompareTo ((object) p984) == 0);
        }


        [TestMethod]
        public void UnitPn_CompareTo()
        {
            var p0 = (Permutation) null;
            var p520 = new Permutation (choices:5, picks:2, rank:0);
            var p521 = new Permutation (choices:5, picks:2, rank:1);
            var p531 = new Permutation (choices:5, picks:3, rank:1);
            var p631 = new Permutation (choices:6, picks:3, rank:1);

            Assert.IsTrue (p520.CompareTo (p0) > 0);
            Assert.IsTrue (p520.CompareTo (p521) < 0);
            Assert.IsTrue (p521.CompareTo (p520) > 0);
            Assert.IsTrue (p521.CompareTo (p531) < 0);
            Assert.IsTrue (p531.CompareTo (p521) > 0);
            Assert.IsTrue (p531.CompareTo (p631) < 0);
            Assert.IsTrue (p631.CompareTo (p531) > 0);
            Assert.IsTrue (p631.CompareTo (p631) == 0);
        }


        [TestMethod]
        public void UnitPn_GetHashCode()
        {
            Permutation source = new Permutation (3);
            int hash = source.GetHashCode();
        }


        [TestMethod]
        public void UnitPn_oEquals()
        {
            Permutation p0 = null;
            Permutation p30a = new Permutation (3);
            Permutation p30b = new Permutation (3);
            Permutation p31 = new Permutation (choices:3, picks:3, rank:1);
            Permutation p4 = new Permutation (4);
            Permutation p431 = new Permutation (choices:4, picks:3, rank:1);

            object j0 = (object) p0;
            object j30b = (object) p30b;
            object j31 = (object) p31;
            object j4 = (object) p4;

            Assert.IsFalse (p30a.Equals (j0));
            Assert.IsTrue (p30a.Equals (j30b));
            Assert.IsFalse (p30a.Equals (j31));
            Assert.IsFalse (p30a.Equals (j4));
            Assert.IsFalse (p431.Equals (p31));
        }


        [TestMethod]
        public void UnitPn_Equals()
        {
            var p0 = (Permutation) null;
            var p530 = new Permutation (5, 3, 0);
            var p531 = new Permutation (5, 3, 1);
            var p541 = new Permutation (5, 4, 1);
            var p641 = new Permutation (6, 4, 1);

            Assert.IsFalse (p530.Equals (p0));
            Assert.IsFalse (p530.Equals (p531));
            Assert.IsFalse (p531.Equals (p530));
            Assert.IsFalse (p531.Equals (p541));
            Assert.IsFalse (p541.Equals (p531));
            Assert.IsFalse (p541.Equals (p641));
            Assert.IsFalse (p641.Equals (p541));
            Assert.IsTrue (p641.Equals (p641));
        }


        [TestMethod]
        public void UnitPn_EqualsOtherType()
        {
            var p54 = new Permutation (5, 4, 1);
            string s = "Mazzy";

            // Comparing to different type returns false.
            Assert.IsFalse (p54.Equals (s));
        }


        [TestMethod]
        public void UnitPn_oGetEnumerator()
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
        public void UnitPn_GetEnumerator()
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
        public void UnitPn_GetRowsEmpty()
        {
            Permutation source = new Permutation();

            int actualCount = 0;
            foreach (Permutation p in source.GetRows())
                ++actualCount;

            Assert.AreEqual (0, actualCount);
        }


        [TestMethod]
        public void UnitPn_GetRows()
        {
            var expect = new int[][]
            {
                new int[] { 0,1,2 }, new int[] { 0,2,1 }, new int[] { 1,0,2 },
                new int[] { 1,2,0 }, new int[] { 2,0,1 }, new int[] { 2,1,0 }
            };

            int order = 3;
            long startRank = 2;
            var pn0 = new Permutation (choices:order, picks:order, rank:startRank);
            Assert.AreEqual (expect.Length, pn0.RowCount);
            var beginData = new int[3];
            pn0.CopyTo (beginData);

            Assert.AreEqual (expect.Length, pn0.RowCount);

            int actualCount = 0;
            foreach (Permutation pn in pn0.GetRows())
            {
                long expectRank = (actualCount + startRank) % expect.Length;
                Assert.AreEqual (expectRank, pn.Rank);
                Assert.AreEqual (expectRank, pn0.Rank);
                Assert.IsTrue (Enumerable.SequenceEqual (expect[expectRank], pn0));
                Assert.IsTrue (Enumerable.SequenceEqual (pn, pn0));
                ++actualCount;
            }

            Assert.AreEqual (expect.Length, actualCount);
            Assert.AreEqual (startRank, pn0.Rank);
            Assert.IsTrue (Enumerable.SequenceEqual (beginData, pn0));
        }


        [TestMethod]
        public void UnitPn_GetRowsForAllChoices()
        {
            var expect = new int[][]
            {
                new int[] { 0 },
                new int[] { 0,1 }, new int[] { 1,0 },
                new int[] { 0,1,2 }, new int[] { 0,2,1 }, new int[] { 1,0,2 },
                new int[] { 1,2,0 }, new int[] { 2,0,1 }, new int[] { 2,1,0 }
            };

            long startRank = 2;
            var pn0 = new Permutation (3, 2, startRank);
            var beginData = new int[pn0.Picks];
            pn0.CopyTo (beginData);
            var beginChoices = pn0.Choices;

            int actualCount = 0;
            foreach (Permutation pn in pn0.GetRowsForAllChoices())
            {
                Assert.IsTrue (Enumerable.SequenceEqual (expect[actualCount], pn));
                Assert.IsTrue (Enumerable.SequenceEqual (pn, pn0));
                ++actualCount;
            }

            Assert.AreEqual (expect.Length, actualCount);
            Assert.AreEqual (startRank, pn0.Rank);
            Assert.AreEqual (beginChoices, pn0.Choices);
            Assert.IsTrue (Enumerable.SequenceEqual (beginData, pn0));
        }


        [TestMethod]
        public void UnitPn_GetRowsForAllPicks()
        {
            var expect = new int[][]
            {
                new int[] { 0 }, new int[] { 1 }, new int[] { 2 },
                new int[] { 0,1 }, new int[] { 0,2 }, new int[] { 1,0 },
                new int[] { 1,2 }, new int[] { 2,0 }, new int[] { 2,1 },
                new int[] { 0,1,2 }, new int[] { 0,2,1 }, new int[] { 1,0,2 },
                new int[] { 1,2,0 }, new int[] { 2,0,1 }, new int[] { 2,1,0 }
            };

            long startRank = 2;
            var pn0 = new Permutation (3, 3, startRank);
            var beginData = new int[pn0.Picks];
            pn0.CopyTo (beginData);

            int actualCount = 0;
            foreach (Permutation pn in pn0.GetRowsForAllPicks())
            {
                Assert.IsTrue (Enumerable.SequenceEqual (expect[actualCount], pn));
                Assert.IsTrue (Enumerable.SequenceEqual (pn, pn0));
                ++actualCount;
            }

            Assert.AreEqual (expect.Length, actualCount);
            Assert.AreEqual (startRank, pn0.Rank);
            Assert.IsTrue (Enumerable.SequenceEqual (beginData, pn0));
        }


        [TestMethod]
        [ExpectedException (typeof (InvalidOperationException))]
        public void CrashPn_GetRowsOfPlainChanges_InvalidOperation2()
        {
            var pn54 = new Permutation (choices: 5, picks: 4);

            foreach (var pn in pn54.GetRowsOfPlainChanges())
            { }
        }


        [TestMethod]
        public void UnitPn_GetRowsOfPlainChanges0()
        {
            foreach (var pn in new Permutation (0).GetRowsOfPlainChanges())
            {
                Assert.Fail ("Unreachable");
            }
        }

        [TestMethod]
        public void UnitPn_GetRowsOfPlainChanges()
        {
            var expect= new int[][]
            {
                new int[] { 0,1,2 }, new int[] { 0,2,1 }, new int[] { 2,0,1 },
                new int[] { 2,1,0 }, new int[] { 1,2,0 }, new int[] { 1,0,2 }
            };

            long startPlainRank = 3;
            var pn0 = new Permutation (3);
            pn0.PlainRank = startPlainRank;
            var beginData = new int[pn0.Picks];
            pn0.CopyTo (beginData);

            int actualCount = 0;
            foreach (var pn in pn0.GetRowsOfPlainChanges())
            {
                long expectRank = (actualCount + startPlainRank) % expect.Length;
                Assert.AreEqual (expectRank, pn.PlainRank);
                Assert.AreEqual (expectRank, pn0.PlainRank);
                Assert.IsTrue (Enumerable.SequenceEqual (expect[expectRank], pn));
                Assert.IsTrue (Enumerable.SequenceEqual (pn, pn0));
                ++actualCount;
            }

            Assert.AreEqual (startPlainRank, pn0.PlainRank);
            Assert.IsTrue (Enumerable.SequenceEqual (beginData, pn0));
        }


        [TestMethod]
        public void StressPn_GetRowsOfPlainChanges()
        {
            // Higher maxWidth values may be used for stress, but will allocate more space.
#if STRESS
            const int maxWidth = 8;
#else
            const int maxWidth = 6;
#endif
            for (int n = 1; n < maxWidth; ++n)
            {
                long rc = n == 0 ? 0 : Combinatoric.Factorial (n);
                var mat = new int[rc][];

                int pcRank = 0;
                var pn0 = new Permutation (n);
                var beginData = new int[n];
                pn0.CopyTo (beginData);

                foreach (var pn in pn0.GetRowsOfPlainChanges())
                {
                    mat[pcRank] = new int[n];
                    pn.CopyTo (mat[pcRank]);
                    ++pcRank;

                    Assert.AreEqual (pn.Rank, pn0.Rank);
                    Assert.IsTrue (Enumerable.SequenceEqual (pn, pn0));
                }
                Assert.AreEqual (0, pn0.Rank);
                Assert.IsTrue (Enumerable.SequenceEqual (beginData, pn0));

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
        public void UnitPn_ToString0()
        {
            Permutation permu = new Permutation();
            string actual = permu.ToString();
            Assert.AreEqual ("{ }", actual);
        }


        [TestMethod]
        public void UnitPn_ToString1()
        {
            Permutation permu = new Permutation (1);
            string actual = permu.ToString();
            Assert.AreEqual ("{ 0 }", actual);
        }


        [TestMethod]
        public void UnitPn_ToString3()
        {
            Permutation permu = new Permutation (3);
            string actual = permu.ToString();
            Assert.AreEqual ("{ 0, 1, 2 }", actual);
        }

        #endregion

        #region Test static methods

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CrashPn_Permute_ArgumentNull1()
        {
            Permutation nullSource = null;
            string[] pattern = new string[] { "A", "B", "C" };
            List<string> target = Permutation.Permute (nullSource, pattern);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CrashPn_Permute_ArgumentNull2()
        {
            Permutation source = new Permutation (3);
            string[] nullPattern = null;
            List<string> target = Permutation.Permute (source, nullPattern);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CrashPn_Permute_Argument()
        {
            Permutation source = new Permutation (4, 3);
            string[] pattern = new string[] { "A", "B", "C" };

            List<string> target = Permutation.Permute (source, pattern);
        }


        [TestMethod]
        public void UnitPn_Permute()
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
        public void UnitPn_ComparisonOps()
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
