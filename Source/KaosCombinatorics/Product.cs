//
// Project: KaosCombinatorics
// File: Product.cs
//
// Copyright © 2009-2021 Kasey Osborn (github.com/kaosborn)
// MIT License - Use and redistribute freely
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Kaos.Combinatorics
{
    /// <summary>
    /// Represents a join of values taken from a supplied array of ranges.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A cartesian product is a set of sets where each subset is constructed by picking
    /// one element from each of a given number of sets. This process of joining elements to
    /// form new sets is repeated until all possible distinct joins are made.
    /// </para>
    /// <para>
    /// The <see cref="Product"/> class uses an array of integers as input where each integer
    /// is the size of each of the composing sets. The joined sets are represented as rows in
    /// a table where each element is a value in the range of these supplied sizes. Rows are
    /// constructed by looping thru the rightmost ranges fastest so that the resulting table
    /// is lexicographically ordered.
    /// </para>
    /// <para>
    /// Use the <see cref="Width"/> property to get the number of joined elements.
    /// 
    /// Use the <see cref="RowCount"/> property to get the number of distinct joins
    /// in the <see cref="Product"/> table.
    ///
    /// Use the <see cref="P:Kaos.Combinatorics.Product.Item(System.Int32)">indexer</see>
    /// to get an element of the row.
    /// 
    /// Use the <see cref="GetEnumerator">default enumerator</see> to iterate thru
    /// the elements of a <see cref="Product"/> row.
    ///
    /// Use the <see cref="Permute">Permute</see>
    /// method to rearrange a supplied list based on the values in a row.
    /// </para>
    /// <para>
    /// Use the <see cref="Rank"/> property to get or set the row index in the ordered
    /// <see cref="Product"/> table of joins.
    /// 
    /// Use <see cref="GetRows"/> to iterate thru all possible joins
    ///  of the<see cref="Product"/> ordered by <see cref="Rank"/>.
    /// </para>
    /// <para>
    /// The default appearance of a <see cref="Product"/> row is a list of integers
    /// (starting at 0) enclosed in braces. The appearance may be tailored 3 ways:
    /// <ul>
    ///   <li>
    ///     Map each element to a different value using the
    ///     <see cref="GetEnumerator">default enumerator</see> or the
    ///     <see cref="P:Kaos.Combinatorics.Product.Item(System.Int32)">indexer</see>.
    ///   </li>
    ///   <li>
    ///     Use the <see cref="Permute">Permute</see> method and output the rearranged values.
    ///   </li>
    ///   <li>
    ///     Define a subclass of <see cref="Product"/> and override
    ///     <see cref="System.Object.ToString">ToString()</see>.
    ///   </li>
    /// </ul>
    /// </para>
    /// <para>
    /// For more information about cartesian products, see:
    /// </para>
    /// <para>
    /// <em>https://en.wikipedia.org/wiki/Cartesian_product</em>
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// Iterating thru <c>new Product (new int[] { 2, 3, 2 }).GetRows()</c> produces:
    /// </para>
    /// <para>
    /// <c>{ 0, 0, 0 }</c><br/>
    /// <c>{ 0, 0, 1 }</c><br/>
    /// <c>{ 0, 1, 0 }</c><br/>
    /// <c>{ 0, 1, 1 }</c><br/>
    /// <c>{ 0, 2, 0 }</c><br/>
    /// <c>{ 0, 2, 1 }</c><br/>
    /// <c>{ 1, 0, 0 }</c><br/>
    /// <c>{ 1, 0, 1 }</c><br/>
    /// <c>{ 1, 1, 0 }</c><br/>
    /// <c>{ 1, 1, 1 }</c><br/>
    /// <c>{ 1, 2, 0 }</c><br/>
    /// <c>{ 1, 2, 1 }</c>
    /// </para>
    /// </example>
    public class Product :
        IComparable,
        System.Collections.IEnumerable,
        IComparable<Product>,
        IEquatable<Product>,
        IEnumerable<int>
    {
        private readonly int[] sizes;    // Size of each column.
        private readonly long[] factors; // Running multiple of sizes.
        private readonly long rowCount;  // Row count of the table of products.
        private long rank;               // Row index.

        #region Constructors

        /// <summary>Initializes an empty product instance.</summary>
        public Product()
        {
            this.sizes = new int[0];
            this.factors = new long[0];
            this.rowCount = 0;
            this.rank = 0;
        }

        /// <summary>Initializes a new instance that is copied from the supplied product.</summary>
        /// <param name="source">Instance to copy.</param>
        /// <exception cref="ArgumentNullException">When <em>source</em> is <b>null</b>.</exception>
        public Product (Product source)
        {
            if (source == null)
                throw new ArgumentNullException (nameof (source));

            this.sizes = new int[source.Width];
            this.factors = new long[source.Width];

            source.sizes.CopyTo (this.sizes, 0);
            source.factors.CopyTo (this.factors, 0);
            this.rowCount = source.RowCount;
            this.rank = source.Rank;
        }

        /// <summary>Initializes a new product of <see cref="Rank"/> 0 with the supplied <em>sizes</em> of columns.</summary>
        /// <param name="sizes">Size of each column.</param>
        /// <example><code source="..\Examples\Product\PtExample01\PtExample01.cs" lang="cs"/></example>
        /// <exception cref="ArgumentNullException">When <em>sizes</em> is <b>null</b>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">When any column size less than 0.</exception>
        /// <exception cref="OverflowException">When product is too big.</exception>
        public Product (int[] sizes)
        {
            if (sizes == null)
                throw new ArgumentNullException (nameof (sizes));

            this.factors = new long[sizes.Length];
            this.rowCount = sizes.Length == 0 ? 0 : 1;

            for (int ei = sizes.Length - 1; ei >= 0; --ei)
            {
                if (sizes[ei] < 0)
                    throw new ArgumentOutOfRangeException (nameof (sizes), "Value is less than zero.");

                this.factors[ei] = this.rowCount;
                this.rowCount = checked (this.rowCount * sizes[ei]);
            }

            this.sizes = new int[sizes.Length];
            sizes.CopyTo (this.sizes, 0);
        }

        /// <summary>Initializes a new product of the supplied <em>rank</em>with the supplied <em>sizes</em> of columns.</summary>
        /// <remarks>
        /// If the supplied <em>rank</em> is out of the range (0..<see cref="RowCount"/>-1),
        /// it will be normalized to the valid range.
        /// For example, a value of -1 will produce the last row in the ordered table.
        /// </remarks>
        /// <param name="sizes">Size of each column.</param>
        /// <param name="rank">Row index in the ordered <see cref="Product"/> table.</param>
        /// <exception cref="ArgumentNullException">When <em>sizes</em> is <b>null</b>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">When any column size less than 0.</exception>
        public Product (int[] sizes, long rank) : this (sizes)
         => Rank = rank;

        /// <summary>Initializes a new product of the supplied values with the supplied <em>sizes</em> of columns.</summary>
        /// <param name="sizes">Size of each column.</param>
        /// <param name="source">Integer values for the columns.</param>
        /// <example><code source="..\Examples\Product\PtExample04\PtExample04.cs" lang="cs"/></example>
        /// <exception cref="ArgumentNullException">When <em>sizes</em> or <em>source</em> is <b>null</b>.</exception>
        /// <exception cref="ArgumentException">When <em>source</em> length does not match <em>sizes</em> length.</exception>
        /// <exception cref="ArgumentOutOfRangeException">When any column size less than 0; when <em>source</em> contains invalid values.</exception>
        public Product (int[] sizes, int[] source) : this (sizes)
        {
            if (source == null)
                throw new ArgumentNullException (nameof (source));
            if (sizes.Length != source.Length)
                throw new ArgumentException ("Length is not valid.", nameof (source));

            for (int si = 0; si < source.Length; ++si)
            {
                if (source[si] < 0 || source[si] >= sizes[si])
                    throw new ArgumentOutOfRangeException (nameof (source), "Element is out of range.");

                this.rank = this.rank * sizes[si] + source[si];
            }
        }

        #endregion

        #region Properties

        /// <summary>Get an element of the <see cref="Product"/> at the supplied column.</summary>
        /// <param name="index">Index value.</param>
        /// <returns>Sequence value at <em>index</em>.</returns>
        /// <example><code source="..\Examples\Product\PtExample05\PtExample05.cs" lang="cs"/></example>
        /// <exception cref="IndexOutOfRangeException">When <em>index</em> not in range (0..<see cref="Width"/>-1).</exception>
        /// <exception cref="DivideByZeroException">When <see cref="RowCount"/> is 0.</exception>
        public int this[int index]
        {
            get
            {
                long rankToElement = Rank;
                if (index > 0)
                    rankToElement %= factors[index - 1];
                return (int) (rankToElement / factors[index]);
            }
        }

        /// <summary>Row index of the join in the lexicographically ordered <see cref="Product"/> table.</summary>
        /// <remarks>Any assigned value out of range will be normalized to (0..<see cref="RowCount"/>-1).</remarks>
        /// <example><code source="..\Examples\Product\PtExample04\PtExample04.cs" lang="cs"/></example>
        public long Rank
        {
            get => rank;
            set
            {
                if (RowCount == 0)
                    rank = 0;
                else
                {
                    // Normalize the new rank.
                    if (value < 0)
                    {
                        rank = value % RowCount;
                        if (rank < 0)
                            rank += RowCount;
                    }
                    else
                        rank = value < RowCount ? value : value % RowCount;
                }
            }
        }

        /// <summary>Count of distinct joins in the <see cref="Product"/> table.</summary>
        public long RowCount => rowCount;

        /// <summary>Number of columns in the <see cref="Product"/>.</summary>
        public int Width => sizes.Length;

        #endregion

        #region Instance methods

        /// <summary>Compare two <see cref="Product"/>s.</summary>
        /// <param name="obj">Target of the comparison.</param>
        /// <returns>A signed integer indicating the sort order of this instance to <em>obj</em>.</returns>
        public int CompareTo (object obj)
         => CompareTo (obj as Product);

        /// <summary>Compare two <see cref="Product"/>s.</summary>
        /// <param name="other">Target of the comparison.</param>
        /// <returns>A signed integer indicating the sort order of this instance to <em>other</em>.</returns>
        public int CompareTo (Product other)
        {
            if (other is null)
                return 1;

            int result = this.Width - other.Width;

            if (result == 0)
                if (this.Rank > other.Rank)
                    result = 1;
                else if (this.Rank < other.Rank)
                    result = -1;

            return result;
        }

        /// <summary>Copy the entire sequence to the supplied destination.</summary>
        /// <param name="array">Destination of copy.</param>
        /// <exception cref="ArgumentNullException">When <em>array</em> is <b>null</b>.</exception>
        /// <exception cref="ArgumentException">When not enough space in <em>array</em>.</exception>
        public void CopyTo (int[] array)
        {
            if (array == null)
                throw new ArgumentNullException (nameof (array));
            if (array.Length < Width)
                throw new ArgumentException ("Destination array is not long enough.");

            for (int ei = 0; ei < Width; ++ei)
                array[ei] = this[ei];
        }

        /// <summary>Indicate whether two <see cref="Product"/>s have the same value.</summary>
        /// <param name="obj">Target of the comparison.</param>
        /// <returns><b>true</b> if <em>obj</em> has the same value as this object; otherwise, <b>false</b>.</returns>
        public override bool Equals (object obj)
         => Equals (obj as Product);

        /// <summary>Indicate whether two <see cref="Product"/>s have the same value.</summary>
        /// <param name="other">Target of the comparison.</param>
        /// <returns><b>true</b> if <em>other</em> has the same value as this object; otherwise, <b>false</b>.</returns>
        public bool Equals (Product other)
         => other is object && other.Rank == Rank && other.Width == Width;

        /// <summary>Get an object-based enumerator of the elements.</summary>
        /// <returns>Object-based elemental enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
         => GetEnumerator();

        /// <summary>Enumerate all elements of a <see cref="Product"/>.</summary>
        /// <returns>An <see cref="System.Collections.Generic.IEnumerator{T}"/> for this <see cref="Product"/>.</returns>
        /// <example><code source="..\Examples\Product\PtExample05\PtExample05.cs" lang="cs"/></example>
        public IEnumerator<int> GetEnumerator()
        {
            for (int ei = 0; ei < Width; ++ei)
                yield return this[ei];
        }

        /// <summary>Get the hash oode of the <see cref="Product"/>.</summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
         => unchecked ((int) rank);

        /// <summary>Iterate thru all rows of the <see cref="Product"/> table for every <see cref="Rank"/> ascending.</summary>
        /// <returns>An iterator for a <see cref="Product"/> table.</returns>
        /// <remarks>
        /// If the start row is not <see cref="Rank"/> 0, the iteration will wrap around
        /// so that <see cref="RowCount"/> items are always produced.
        /// </remarks>
        /// <example><code source="..\Examples\Product\PtExample01\PtExample01.cs" lang="cs"/></example>
        public IEnumerable<Product> GetRows()
        {
            if (RowCount != 0)
                for (long beginRank = Rank;;)
                {
                    yield return this;
                    Rank += 1;
                    if (Rank == beginRank)
                        yield break;
                }
        }

        /// <summary>Get the size of a column.</summary>
        /// <param name="column">Column index.</param>
        /// <returns>Number of distinct values (starting at 0) that a column may take.</returns>
        /// <exception cref="IndexOutOfRangeException">When <em>column</em> not in range (0..<see cref="Width"/>-1).</exception>
        public int Size (int column)
         => sizes[column];

        /// <summary>Provide readable form of the <see cref="Product"/> row.</summary>
        /// <returns>A <c>string</c> that represents the sequence.</returns>
        /// <remarks>Result is enclosed in braces and separated by commas.</remarks>
        /// <example><code source="..\Examples\Product\PtExample04\PtExample04.cs" lang="cs"/></example>
        public override string ToString()
        {
            if (RowCount == 0)
                return "{ }";

            var result = new StringBuilder ("{ ");

            for (int ei = 0;;)
            {
                result.Append (this[ei]);

                ++ei;
                if (ei >= Width)
                    break;

                result.Append (", ");
            }

            result.Append (" }");
            return result.ToString();
        }

        #endregion

        #region Static methods

        /// <summary>Apply a <see cref="Product"/> sequence to select from the supplied lists or arrays.</summary>
        /// <typeparam name="T">Type of items to rearrange.</typeparam>
        /// <param name="arrangement">New arrangement for items.</param>
        /// <param name="source">List of List of Items or arrays to rarrange.</param>
        /// <returns>List of rearranged items.</returns>
        /// <example><code source="..\Examples\Product\PtExample03\PtExample03.cs" lang="cs"/></example>
        /// <exception cref="ArgumentNullException">When <em>arrangement</em> or <em>source</em> is <b>null</b>.</exception>
        /// <exception cref="ArgumentException">
        /// When length of <em>source</em> is less than <em>arrangement</em>.<see cref="Width"/>;
        /// when a <em>source</em> list is too small.
        /// </exception>
        public static List<T> Permute<T> (Product arrangement, IList<IList<T>> source)
        {
            if (arrangement == null)
                throw new ArgumentNullException (nameof (arrangement));

            if (source == null)
                throw new ArgumentNullException (nameof (source));

            if (source.Count < arrangement.Width)
                throw new ArgumentException ("Not enough supplied values.", nameof (source));

            for (int ix = 0; ix < arrangement.Width; ++ix)
                if (source[ix].Count < arrangement.Size (ix))
                    throw new ArgumentException ("Not enough supplied values.", nameof (source));

            var result = new List<T> (arrangement.Width);
            for (int ix = 0; ix < arrangement.Width; ++ix)
                result.Add (source[ix][arrangement[ix]]);

            return result;
        }

        /// <summary>Indicate whether 2 <see cref="Product"/>s are equal.</summary>
        /// <param name="param1">A <see cref="Product"/> sequence.</param>
        /// <param name="param2">A <see cref="Product"/> sequence.</param>
        /// <returns><b>true</b> if supplied sequences are equal;
        /// otherwise, <b>false</b>.</returns>
        public static bool operator == (Product param1, Product param2)
         => param1 is null ? param2 is null : param1.Equals (param2);

        /// <summary>Indicate whether 2 <see cref="Product"/>s are not equal.</summary>
        /// <param name="param1">A <see cref="Product"/> sequence.</param>
        /// <param name="param2">A <see cref="Product"/> sequence.</param>
        /// <returns><b>true</b> if supplied sequences are not equal;
        /// otherwise, <b>false</b>.</returns>
        public static bool operator != (Product param1, Product param2)
         => param1 is null ? param2 is object : ! param1.Equals (param2);

        /// <summary>Indicate whether the left <see cref="Product"/> is less than
        /// the right <see cref="Product"/>.</summary>
        /// <param name="param1">A <see cref="Product"/> sequence.</param>
        /// <param name="param2">A <see cref="Product"/> sequence.</param>
        /// <returns><b>true</b> if the left sequence is less than
        /// the right sequence otherwise, <b>false</b>.</returns>
        public static bool operator < (Product param1, Product param2)
         => param1 is null ? param2 is object : param1.CompareTo (param2) < 0;

        /// <summary>Indicate whether the left <see cref="Product"/> is greater than
        /// or equal to the right <see cref="Product"/>.</summary>
        /// <param name="param1">A <see cref="Product"/> sequence.</param>
        /// <param name="param2">A <see cref="Product"/> sequence.</param>
        /// <returns><b>true</b> if the left sequence is greater than or
        /// equal to the right sequence otherwise, <b>false</b>.</returns>
        public static bool operator >= (Product param1, Product param2)
         => param1 is null ? param2 is null : param1.CompareTo (param2) >= 0;

        /// <summary>Indicate whether the left <see cref="Product"/> is greater than
        /// the right <see cref="Product"/>.</summary>
        /// <param name="param1">A <see cref="Product"/> sequence.</param>
        /// <param name="param2">A <see cref="Product"/> sequence.</param>
        /// <returns><b>true</b> if the left sequence is greater than
        /// the right sequence otherwise, <b>false</b>.</returns>
        public static bool operator > (Product param1, Product param2)
         => ! (param1 is null) && param1.CompareTo (param2) > 0;

        /// <summary>Indicate whether the left <see cref="Product"/> is less than or equal to
        /// the right <see cref="Product"/>.</summary>
        /// <param name="param1">A <see cref="Product"/> sequence.</param>
        /// <param name="param2">A <see cref="Product"/> sequence.</param>
        /// <returns><b>true</b> if the left sequence is less than or equal to
        /// the right sequence otherwise, <b>false</b>.</returns>
        public static bool operator <= (Product param1, Product param2)
         => param1 is null || param1.CompareTo (param2) <= 0;

        #endregion
    }
}
