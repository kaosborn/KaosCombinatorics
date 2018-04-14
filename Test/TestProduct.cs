using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kaos.Combinatorics;

namespace CombinatoricsTest
{
    [TestClass]
    public class TestProduct
    {
        #region Test constructors

        [TestMethod]
        public void UnitPt_Inheritance()
        {
            Product pt = new Product (new int[] { 2, 3, 4 });

            Assert.IsNotNull (pt as IComparable);
            Assert.IsNotNull (pt as System.Collections.IEnumerable);
            Assert.IsNotNull (pt as IComparable<Product>);
            Assert.IsNotNull (pt as IEquatable<Product>);
            Assert.IsNotNull (pt as IEnumerable<int>);
        }


        [TestMethod]
        public void UnitPt_Ctor0()
        {
            Product pt0 = new Product();
            Assert.AreEqual (0, pt0.RowCount);
            Assert.AreEqual (0, pt0.Width);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CrashPt_Ctor1A_ArgumentNull()
        {
            Product ptNull = null;
            Product pt = new Product (ptNull);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CrashPt_Ctor1B_ArgumentNull()
        {
            int[] nullSizes = null;
            Product pt = new Product (nullSizes);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPt_Ctor1B_ArgumentOutOfRange()
        {
            int[] sizes = new int[] { 2, -4, 3 };
            Product pt = new Product (sizes);
        }


        [TestMethod]
        public void UnitPt_Ctor1B()
        {
            int[,] expected2by3 = new int[,] { { 0, 0 }, { 0, 1 }, { 0, 2 }, { 1, 0 }, { 1, 1 }, { 1, 2 } };

            Product pt0 = new Product (new int[] { 5, 0, 3 });

            Assert.AreEqual (0, pt0.RowCount);
            Assert.AreEqual (3, pt0.Width);
            Assert.AreEqual (5, pt0.Size (0));
            Assert.AreEqual (0, pt0.Size (1));
            Assert.AreEqual (3, pt0.Size (2));

            int actualIterations = 0;
            foreach (Product row in pt0.GetRows())
            {
                ++actualIterations;
            }
            Assert.AreEqual (0, actualIterations);

            Product pt3 = new Product (new int[] { 2, 4, 3 });
            Assert.AreEqual (2 * 4 * 3, pt3.RowCount);

            Product pt4 = new Product (new int[0]);
            Assert.AreEqual (0, pt4.RowCount);
            Assert.AreEqual (0, pt4.Width);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CrashPt_Ctor2A_ArgumentNull()
        {
            int[] nullSizes = null;
            Product pt = new Product (nullSizes, 0);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPt_Ctor2A_ArgumentOutOfRange()
        {
            int[] sizes = new int[] { 2, -4, 3 };
            Product pt = new Product (sizes, 0);
        }


        [TestMethod]
        public void UnitPt_Ctor2A_Valid()
        {
            int[] sizes = new int[] { 2, 4, 3 };
            Product pt = new Product (sizes, 20);

            Assert.AreEqual (24, pt.RowCount);
            Assert.AreEqual (20, pt.Rank);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CrashPt_Ctor2B_ArgumentNull1()
        {
            int[] nullSizes = null;
            int[] source = new int[] { 1, 2, 3 };
            Product pt = new Product (nullSizes, source);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CrashPt_Ctor2B_ArgumentNull2()
        {
            int[] sizes = new int[] { 2, 3, 4 };
            int[] nullSource = null;
            Product pt = new Product (sizes, nullSource);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPt_Ctor2B_ArgumentOutOfRange1()
        {
            int[] sizes = new int[] { 2, -4, 3 };
            int[] source = new int[] { 0, 0, 0 };
            Product pt = new Product (sizes, source);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPt_Ctor2B_ArgumentOutOfRange2()
        {
            int[] sizes = new int[] { 2, -4, 3 };
            int[] source = new int[] { 1, -1, 2 };
            Product pt = new Product (sizes, source);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPt_Ctor2B_ArgumentOutOfRange3()
        {
            int[] sizes = new int[] { 2, 4, 3 };
            int[] source = new int[] { 1, 4, 2 };
            Product pt = new Product (sizes, source);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPt_Ctor2B_ArgumentOutOfRange4()
        {
            int[] sizes = new int[] { 2, 0, 3 };
            int[] source = new int[] { 1, 0, 2 };
            Product pt = new Product (sizes, source);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPt_Ctor2B_ArgumentOutOfRange5()
        {
            int[] sizes = new int[] { 2, 3, 4 };
            int[] source = new int[] { 1, -1, 2 };
            Product pt = new Product (sizes, source);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CrashPt_Ctor2B_ArgumentOutOfRange6()
        {
            int[] sizes = new int[] { 2, 3, 4 };
            int[] source = new int[] { 1, 1 };
            Product pt = new Product (sizes, source);
        }


        [TestMethod]
        public void UnitPt_Ctor2BA()
        {
            int[] sizes = new int[] { 2, 0, 2 };
            Product pt = new Product (sizes);

            pt.Rank = 1;

            Assert.AreEqual (0, pt.Rank);
            Assert.AreEqual (0, pt.RowCount);
        }


        [TestMethod]
        public void UnitPt_Ctor2BC()
        {
            int[] sizes = new int[] { 10, 20, 30 };
            int[] source = new int[sizes.Length];

            long rank = 1234;

            Product pt1 = new Product (sizes, rank);

            for (int i = 0; i < source.Length; ++i)
                source[i] = pt1[i];

            Product pt2 = new Product (sizes, source);

            Assert.AreEqual (rank, pt2.Rank);
        }


        [TestMethod]
        public void StressPt_Ctor2B()
        {
            // Use higher maxSizes, sizeFactor values for a longer running test.
#if STRESS
            const int maxSizes = 5, sizeFactor = 10;
#else
            const int maxSizes = 3, sizeFactor = 5;
#endif
            long counter = 0;

            for (int sizeNum = 1; sizeNum < maxSizes; ++sizeNum)
            {
                long maxRank = 1;
                int[] sizes = new int[sizeNum];
                int[] source = new int[sizeNum];

                for (int i = 0; i < sizeNum; ++i)
                {
                    sizes[i] = sizeFactor * (i + 1);
                    maxRank *= sizes[i];
                }

                for (int rank = 0; rank < maxRank; ++rank)
                {
                    Product row1 = new Product (sizes, rank);

                    row1.CopyTo (source);

                    Product row2 = new Product (sizes, source);

                    // verify that rank(unrank(x)) = x
                    Assert.AreEqual (rank, row1.Rank);
                    Assert.AreEqual (rank, row2.Rank);
                    ++counter;
                }
            }
        }

        #endregion

        #region Test properties

        [TestMethod]
        public void UnitPt_RankEmpty()
        {
            Product pt0 = new Product (new int[] { 5, 0, 3 });

            Assert.AreEqual (0, pt0.Rank);

            pt0.Rank = pt0.Rank + 1;

            Assert.AreEqual (0, pt0.Rank);
        }


        [TestMethod]
        public void UnitPt_Rank()
        {
            Product pt = new Product (new int[] { 3, 4 });

            pt.Rank = 5;
            Assert.AreEqual (5, pt.Rank);

            pt.Rank = pt.RowCount * -2 - 1;
            Assert.AreEqual (pt.RowCount-1, pt.Rank);

            pt.Rank = pt.RowCount * -2;
            Assert.AreEqual (0, pt.Rank);

            pt.Rank = pt.RowCount * -1 - 1;
            Assert.AreEqual (pt.RowCount-1, pt.Rank);

            pt.Rank = pt.RowCount * -1;
            Assert.AreEqual (0, pt.Rank);

            pt.Rank = -1;
            Assert.AreEqual (pt.RowCount-1, pt.Rank);
        }


        [TestMethod]
        public void UnitPt_RankIncrement()
        {
            Product pt = new Product (new int[] { 3, 5 });
            pt.Rank = 7;

            ++pt.Rank;
            Assert.AreEqual (8, pt.Rank);

            pt.Rank++;
            Assert.AreEqual (9, pt.Rank);
        }


        [TestMethod]
        public void UnitPt_RankDecrement()
        {
            Product pt = new Product (new int[] { 3, 5 });
            pt.Rank = 0;

            --pt.Rank;
            Assert.AreEqual (14, pt.Rank);

            pt.Rank--;
            Assert.AreEqual (13, pt.Rank);
        }


        [TestMethod]
        [ExpectedException (typeof (DivideByZeroException))]
        public void CrashPt_Index_DivideByZero()
        {
            Product pt = new Product (new int[] { 4, 0, 2 });

            int divBy0 = pt[0];
        }


        [TestMethod]
        public void UnitPt_Index()
        {
            Product pt = new Product (new int[] { 4, 3, 2 });
            pt.Rank = 23;

            Assert.AreEqual (3, pt[0]);
            Assert.AreEqual (2, pt[1]);
            Assert.AreEqual (1, pt[2]);
        }

        #endregion

        #region Test instance methods

        [TestMethod]
        public void UnitPt_oCompareTo()
        {
            var objectSortedList = new System.Collections.SortedList();

            Product pt1 = new Product (new int[] { 3, 4, 5 });
            Product pt2 = new Product (pt1);
            Product pt3 = new Product (pt1);

            pt1.Rank = 10;
            pt2.Rank = 30;
            pt3.Rank = 20;

            objectSortedList.Add (pt1, 10);
            objectSortedList.Add (pt2, 30);
            objectSortedList.Add (pt3, 20);

            int expectedValue = 10;
            foreach (System.Collections.DictionaryEntry item in objectSortedList)
            {
                int actualIndex = (int) item.Value;
                Assert.AreEqual (expectedValue, actualIndex);
                expectedValue += 10;
            }

            Assert.AreEqual (40, expectedValue);
        }


        [TestMethod]
        public void UnitPt_CompareTo()
        {
            Product pt0 = null;
            Product pt16 = new Product (new int[] { 2, 3, 4 }); pt16.Rank = 16;
            Product pt17 = new Product (new int[] { 2, 3, 4 }); pt17.Rank = 17;

            int actual1 = pt16.CompareTo (pt0);
            Assert.IsTrue (actual1 > 0);

            int actual2 = pt16.CompareTo (pt17);
            Assert.IsTrue (actual2 < 0);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CrashPt_CopyTo_ArgumentNull()
        {
            Product pt = new Product (new int[] { 3, 2, 4 }, 6);

            int[] nullTarget = null;
            pt.CopyTo (nullTarget);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CrashPt_CopyTo_Argument()
        {
            Product pt = new Product (new int[] { 3, 2, 4 }, 6);

            int[] nullTarget = new int[2];
            pt.CopyTo (nullTarget);
        }


        [TestMethod]
        public void UnitPt_CopyTo()
        {
            Product pt3 = new Product (new int[] { 3, 4, 5 }, 22);

            int[] target = new int[3];
            pt3.CopyTo (target);

            Assert.AreEqual (1, target[0]);
            Assert.AreEqual (0, target[1]);
            Assert.AreEqual (2, target[2]);
        }


        [TestMethod]
        public void UnitPt_oEquals()
        {
            Product p0 = null;
            Product p16a = new Product (new int[] { 2, 3, 4 }); p16a.Rank = 16;
            Product p16b = new Product (new int[] { 2, 3, 4 }); p16b.Rank = 16;
            Product p17 = new Product (new int[] { 2, 3, 4 }); p17.Rank = 17;
            Product q4 = new Product (new int[] { 1, 2, 3, 4 });

            object j0 = (object) p0;
            object j16b = (object) p16b;
            object j17 = (object) p17;
            object j4 = (object) q4;

            Assert.IsFalse (p16a.Equals (j0));
            Assert.IsTrue (p16a.Equals (j16b));
            Assert.IsFalse (p16a.Equals (j17));
            Assert.IsFalse (p16a.Equals (j4));
        }


        [TestMethod]
        public void UnitPt_Equals()
        {
            Product p0 = null;
            Product p16a = new Product (new int[] { 2, 3, 4 }); p16a.Rank = 16;
            Product p16b = new Product (new int[] { 2, 3, 4 }); p16b.Rank = 16;
            Product p17 = new Product (new int[] { 2, 3, 4 }); p17.Rank = 17;
            Product q = new Product (new int[] { 1, 2, 3, 4 });

            Assert.IsFalse (p16a.Equals (p0));
            Assert.IsTrue (p16a.Equals (p16b));
            Assert.IsFalse (p16a.Equals (p17));
            Assert.IsFalse (p16a.Equals (q));
        }


        [TestMethod]
        public void UnitPt_EqualsOtherType()
        {
            Product p234 = new Product (new int[] { 2, 3, 4 });
            string s = "Roxy";

            // Comparing to different type returns false.
            Assert.IsFalse (p234.Equals (s));
        }


        [TestMethod]
        public void UnitPt_oGetEnumerator()
        {
            Product pt = new Product (new int[] { 2, 3, 4 });
            pt.Rank = 6;

            System.Collections.IEnumerator nu = ((System.Collections.IEnumerable) pt).GetEnumerator();

            int expected = 0;
            while (nu.MoveNext())
            {
                int actual = (int) nu.Current;
                Assert.AreEqual (expected, actual);
                ++expected;
            }

            Assert.AreEqual (pt.Width, expected);
        }


        [TestMethod]
        public void UnitPt_GetEnumerator()
        {
            Product pt = new Product (new int[] { 3, 4, 5 });

            pt.Rank = 33;
            int[] expectedValues = new int[] { 1, 2, 3 };

            int index = 0;
            foreach (int actualValue in pt)
            {
                Assert.AreEqual (expectedValues[index], actualValue);
                index++;
            }

            Assert.AreEqual (expectedValues.Length, index);
        }


        [TestMethod]
        public void UnitPt_GetHashCode()
        {
            Product pt = new Product();
            int hash = pt.GetHashCode();
        }


        [TestMethod]
        public void UnitPt_GetRowsEmpty()
        {
            Product pt = new Product (new int[] { 2, 0, 3 });

            int actualCount = 0;
            foreach (Product row in pt.GetRows())
            {
                ++actualCount;
            }

            Assert.AreEqual (0, actualCount);
        }


        [TestMethod]
        public void UnitPt_GetRows()
        {
            Product pt = new Product (new int[] { 2, 3 });

            int[][] expected = new[]
            {
                new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 },
                new int[] { 1, 0 }, new int[] { 1, 1 }, new int[] { 1, 2 },
                new int[] { 9, 9 }
            };

            int actualCount = 0;

            foreach (Product row in pt.GetRows())
            {
                Assert.AreEqual (actualCount / 3, row[0]);
                Assert.AreEqual (actualCount % 3, row[1]);
                ++actualCount;
            }
            Assert.AreEqual (6, actualCount);
        }


        [TestMethod]
        [ExpectedException (typeof (IndexOutOfRangeException))]
        public void CrashPt_Size_IndexOutOfRange()
        {
            int[] sz = new int[] { 2, 3, 4 };
            Product pt = new Product (sz);
            int zz = pt.Size (5);
        }


        [TestMethod]
        public void UnitPt_Size()
        {
            int[] sz = new int[] { 3, 2, 4 };
            Product pt = new Product (sz);

            for (int ii = 0; ii < sz.Length; ++ii)
                Assert.AreEqual (sz[ii], pt.Size (ii));
        }


        [TestMethod]
        public void UnitPt_ToString0()
        {
            Product pt = new Product (new int[] { });
            string actual = pt.ToString();
            Assert.AreEqual ("{ }", actual);
        }


        [TestMethod]
        public void UnitPt_ToString1()
        {
            Product pt = new Product (new int[] { 2 });
            pt.Rank = 1;
            string actual = pt.ToString();
            Assert.AreEqual ("{ 1 }", actual);
        }


        [TestMethod]
        public void UnitPt_ToString3()
        {
            Product pt = new Product (new int[] { 4, 3, 2 });
            pt.Rank = 23;
            string actual = pt.ToString();
            Assert.AreEqual ("{ 3, 2, 1 }", actual);
        }

        #endregion

        #region Test static methods

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CrashPt_Permute_ArgumentNull1()
        {
            Product pt = new Product (new int[] { 2, 3 });
            object[][] nullSource = null;
            List<object> result = Product.Permute (pt, nullSource);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CrashPt_Permute_ArgumentNull2()
        {
            Product ptNull = null;
            object[][] source = new object[][] { new string[] { "A", "B" }, new string[] { "X", "Y", "Z" } };
            List<object> result = Product.Permute (ptNull, source);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CrashPt_Permute_Argument()
        {
            Product pt = new Product (new int[] { 2, 3 });
            object[][] source = new object[][] { new string[] { "A", "B" } };
            List<object> j = Product.Permute (pt, source);
        }


        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CrashPt_Permute_ArgumentOutOfRange()
        {
            Product pt = new Product (new int[] { 3 });
            pt.Rank = 2;
            object[][] source = new object[][] { new string[] { "A", "B" } };
            List<object> j = Product.Permute (pt, source);
        }


        [TestMethod]
        public void UnitPt_Permute()
        {
            string[] set1 = new string[] { "A", "B", "C" };
            string[] set2 = new string[] { "X", "Y" };
            object[][] ja = new object[][] { set1, set2 };
            string[] expected = new string[] { "AX", "AY", "BX", "BY", "CX", "CY" };

            int actualCount = 0;
            foreach (Product row in new Product (new int[] { set1.Length, set2.Length }).GetRows())
            {
                string actual = "";
                foreach (object j in Product.Permute (row, ja))
                    actual += j;
                Assert.AreEqual (expected[actualCount], actual);
                ++actualCount;
            }
            Assert.AreEqual (expected.Length, actualCount);
        }


        [TestMethod]
        public void UnitPt_Comparisons()
        {
            int[] dims1 = new int[] { 2, 3 };

            Product p0 = null;
            Product q0 = null;
            Product p1 = new Product (dims1); p1.Rank = 1;
            Product p11 = new Product (dims1); p11.Rank = 1;
            Product p2 = new Product (dims1); p2.Rank = 2;
            Product q4 = new Product (new int[] { 2, 3, 4, 5 }); q4.Rank = 42;

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
