//
// Project: KaosCombinatorics
// File: Combination.cs
//
// Copyright © 2009-2018 Kasey Osborn (github.com/kaosborn)
// MIT License - Use and redistribute freely
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Kaos.Combinatorics
{
    /// <summary>
    /// Represents an ascending sequence of distinct picks from a supplied range.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The defining variables of a combination
    /// are <em>n</em> which is the number of possible choices
    /// and <em>k</em> which is the number of distinct picks from those choices.
    /// When <em>k</em> is less than <em>n</em>, this is a <em>k</em>-combination
    /// also known as a pick-combination.
    /// Combinations are contrasted to multicombinations where the values in the sequence may repeat.
    /// </para>
    /// <para>
    /// The <see cref="Combination"/> class produces <em>k</em>-combinations with ascending elements.
    /// While sequence order of the elements is not a general requirement of combinations,
    /// producing an ascending sequence allows ranking the sequences into an ordered table.
    /// </para>
    /// <para>
    /// Use the <see cref="Picks"/> property to get the number of elements (<em>k</em>)
    /// of a <see cref="Combination"/> taken from a possible number of
    /// <see cref="Choices"/> (<em>n</em>).
    /// 
    /// Use the <see cref="RowCount"/> property to get the number of distinct possible
    /// <see cref="Combination"/> sequences.
    /// 
    /// Use the <see cref="P:Kaos.Combinatorics.Combination.Item(System.Int32)">indexer</see>
    /// to get a specified element of the sequence.
    /// 
    /// Use the <see cref="GetEnumerator">default enumerator</see> to iterate thru
    /// the elements of a <see cref="Combination"/>.
    /// 
    /// Use the <see cref="Permute">Permute</see> method to pick objects from a supplied array
    /// of choices based on the current sequence.
    /// </para>
    /// <para>
    /// Use the <see cref="Rank"/> property to get or set the row index in a lexicographically
    /// ordered <see cref="Combination"/> table of all possible sequences in an ascending order.
    /// 
    /// Use <see cref="GetRows"/> to iterate thru all possible sequences
    /// of the <see cref="Combination"/> ordered by <see cref="Rank"/>.
    /// 
    /// Use <see cref="GetRowsForAllPicks"/> to iterate
    /// thru every table of all picks in the range (1..<see cref="Picks"/>).
    /// </para>
    /// <para>
    /// The default appearance of a <see cref="Combination"/> row is a list of
    /// integers (starting at 0) enclosed in braces. The appearance may be tailored 3 ways:
    /// <ul>
    ///   <li>
    ///     Map each element to a different value using the
    ///     <see cref="GetEnumerator">default enumerator</see> or the
    ///     <see cref="P:Kaos.Combinatorics.Multicombination.Item(System.Int32)">indexer</see>.
    ///   </li>
    ///   <li>
    ///     Use the <see cref="Permute">Permute</see> method and output the rearranged values.
    ///   </li>
    ///   <li>
    ///     Define a subclass of <see cref="Combination"/> and override
    ///     <see cref="System.Object.ToString">ToString()</see>.
    ///     (See <see cref="GetRowsForAllPicks"/> for an example.)
    ///   </li>
    /// </ul>
    /// </para>
    /// <para>
    /// For more information about <em>k</em>-combinations, see:
    /// </para>
    /// <para>
    /// <em>https://en.wikipedia.org/wiki/Combination</em>
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// Iterating thru <c>new Combination (6, 3).GetRows()</c> produces:
    /// </para>
    /// <para>
    /// <c>{ 0, 1, 2 }</c><br/>
    /// <c>{ 0, 1, 3 }</c><br/>
    /// <c>{ 0, 1, 4 }</c><br/>
    /// <c>{ 0, 1, 5 }</c><br/>
    /// <c>{ 0, 2, 3 }</c><br/>
    /// <c>{ 0, 2, 4 }</c><br/>
    /// <c>{ 0, 2, 5 }</c><br/>
    /// <c>{ 0, 3, 4 }</c><br/>
    /// <c>{ 0, 3, 5 }</c><br/>
    /// <c>{ 0, 4, 5 }</c><br/>
    /// <c>{ 1, 2, 3 }</c><br/>
    /// <c>{ 1, 2, 4 }</c><br/>
    /// <c>{ 1, 2, 5 }</c><br/>
    /// <c>{ 1, 3, 4 }</c><br/>
    /// <c>{ 1, 3, 5 }</c><br/>
    /// <c>{ 1, 4, 5 }</c><br/>
    /// <c>{ 2, 3, 4 }</c><br/>
    /// <c>{ 2, 3, 5 }</c><br/>
    /// <c>{ 2, 4, 5 }</c><br/>
    /// <c>{ 3, 4, 5 }</c>
    /// </para>
    /// </example>
    public class Combination :
        IComparable,
        System.Collections.IEnumerable,
        IComparable<Combination>,
        IEquatable<Combination>,
        IEnumerable<int>
    {
        private int[] data;     // The picks for the current rank. Length is 'k'.
        private int choices;    // Number of possible values 'n'.
        private long rowCount;  // Row count of the table of k-combinations.
        private long rank;      // Row index.

        #region Constructors

        /// <summary>
        /// Initializes an empty combination instance.
        /// </summary>
        public Combination()
        {
            this.data = new int[0];
            this.choices = 0;
            this.rowCount = 0;
            this.rank = 0;
        }


        /// <summary>
        /// Initializes a new instance that is copied from the supplied combination.
        /// </summary>
        /// <param name="source">Instance to copy.</param>
        /// <exception cref="ArgumentNullException">When <em>source</em> is <b>null</b>.</exception>
        public Combination (Combination source)
        {
            if (source == null)
                throw new ArgumentNullException (nameof (source));

            this.data = new int[source.Picks];
            source.data.CopyTo (this.data, 0);

            this.choices = source.Choices;
            this.rowCount = source.RowCount;
            this.rank = source.Rank;
        }


        /// <summary>
        /// Initializes a new combination of <see cref="Rank"/> 0 with the supplied number of elements.
        /// </summary>
        /// <param name="choices">Number of elements in the sequence.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// When <em>choices</em> less than 0.
        /// </exception>
        public Combination (int choices)
        {
            if (choices < 0)
                throw new ArgumentOutOfRangeException (nameof (choices), "Value is less than zero.");

            this.data = new int[choices];
            for (int ki = 0; ki < choices; ++ki)
                this[ki] = ki;

            this.choices = choices;
            this.rowCount = choices == 0? 0 : 1;
            this.rank = 0;
        }


        /// <summary>
        /// Initializes a new combination of <see cref="Rank"/> 0
        /// with the supplied number of <em>picks</em> from the supplied number of <em>choices</em>.
        /// </summary>
        /// <param name="choices">Number of values to pick from.</param>
        /// <param name="picks">Number of elements in the sequence.</param>
        /// <remarks>
        /// Supplying a value for <em>choices</em> that is greater than <em>picks</em>
        /// will instantiate a <em>k</em>-combination also known as a pick-combination.
        /// </remarks>
        /// <example>
        /// <code source="..\Examples\Combination\CnExample01\CnExample01.cs" lang="cs" />
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// When negative value supplied; when <em>picks</em> greater than <em>choices</em>.
        /// </exception>
        /// <exception cref="OverflowException">When the numbers are just too big.</exception>
        public Combination (int choices, int picks)
        {
            if (choices < 0)
                throw new ArgumentOutOfRangeException (nameof (choices), "Value is less than zero.");

            if (picks < 0)
                throw new ArgumentOutOfRangeException (nameof (picks), "Value is less than zero.");

            if (picks > choices)
                throw new ArgumentOutOfRangeException (nameof (picks), "Value is greater than choices.");

            this.data = new int[picks];
            for (int ki = 0; ki < picks; ++ki)
                this[ki] = ki;

            this.choices = choices;
            this.rowCount = picks == 0? 0 : Combinatoric.BinomialCoefficient (choices, picks);
            this.rank = 0;
        }


        /// <summary>
        /// Initializes a new combination of the supplied <em>rank</em>
        /// with the supplied number of <em>picks</em> from the supplied number of <em>choices</em>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Supplying a value for <em>choices</em> that is greater than <em>picks</em>
        /// will instantiate a <em>k</em>-combination also known as a pick-combination.
        /// <para>
        /// </para>
        /// If the supplied <em>rank</em> is out of the range (0..<see cref="RowCount"/>-1),
        /// it will be normalized to the valid range.
        /// For example, a value of -1 will produce the last row in the ordered table.
        /// </para>
        /// </remarks>
        /// <param name="choices">Number of values to pick from.</param>
        /// <param name="picks">Number of elements in the sequence.</param>
        /// <param name="rank">Row index in the ordered <see cref="Combination"/> table.</param>
        /// <example>
        /// <code source="..\Examples\Combination\CnExample05\CnExample05.cs" lang="cs" />
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// When negative value supplied; when <em>picks</em> greater than <em>choices</em>.
        /// </exception>
        /// <exception cref="OverflowException">When too many <em>choices</em>.</exception>
        public Combination (int choices, int picks, long rank)
        {
            if (choices < 0)
                throw new ArgumentOutOfRangeException (nameof (choices), "Value is less than zero.");

            if (picks < 0)
                throw new ArgumentOutOfRangeException (nameof (picks), "Value is less than zero.");

            if (picks > choices)
                throw new ArgumentOutOfRangeException (nameof (picks), "Value is greater than choices.");

            this.data = new int[picks];
            this.choices = choices;
            this.rowCount = picks == 0? 0 : Combinatoric.BinomialCoefficient (choices, picks);
            Rank = rank;
        }


        /// <summary>
        /// Initializes a new combination from elements supplied in <em>source</em>
        /// picked from the supplied number of <em>choices</em>.
        /// </summary>
        /// <param name="choices">Number of values to pick from.</param>
        /// <param name="source">Array of integers.</param>
        /// <remarks>
        /// Supplying a value for <em>choices</em> that is greater than the number of elements in <em>source</em>
        /// will instantiate a <em>k</em>-combination also known as a pick-combination.
        /// </remarks>
        /// <example>
        /// <code source="..\Examples\Combination\CnExample04\CnExample04.cs" lang="cs" />
        /// </example>
        /// <exception cref="ArgumentNullException">When <em>source</em> is <b>null</b>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// When length of <em>source</em> is greater than <em>picks</em>;
        /// when <em>source</em> contains invalid data.
        /// </exception>
        public Combination (int choices, int[] source)
        {
            if (source == null)
                throw new ArgumentNullException (nameof (source));

            if (choices < 0)
                throw new ArgumentOutOfRangeException (nameof (choices), "Value is less than zero.");

            if (choices < source.Length)
                throw new ArgumentOutOfRangeException (nameof (choices), "Value is less than picks.");

            this.data = new int[source.Length];
            source.CopyTo (this.data, 0);
            Array.Sort (this.data);

            this.choices = choices;
            this.rowCount = Picks == 0? 0 : Combinatoric.BinomialCoefficient (choices, Picks);

            for (int ki = 0; ki < Picks; ++ki)
                if (this[ki] < 0 || this[ki] >= choices)
                    throw new ArgumentOutOfRangeException (nameof (source), "Element is out of range.");
                else if (ki > 0 && this[ki] == this[ki-1])
                    throw new ArgumentOutOfRangeException (nameof (source), "Elements must be unique.");

            //
            // Perform ranking:
            //

            this.rank = 0;
            int ji = 0;
            for (int ki = 0; ki < Picks; ++ki)
            {
                for (; ji < this[ki]; ++ji)
                    this.rank += Combinatoric.BinomialCoefficient (Choices - ji - 1, Picks - ki - 1);

                ji = this[ki] + 1;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get a element of the <see cref="Combination"/> at the supplied column.
        /// </summary>
        /// <param name="index">Zero-based index value.</param>
        /// <returns>Sequence value at <em>index</em>.</returns>
        /// <example>
        /// <code source="..\Examples\Combination\CnExample05\CnExample05.cs" lang="cs" />
        /// </example>
        /// <exception cref="IndexOutOfRangeException">
        /// When <em>index</em> not in range (0..<see cref="Picks"/>-1).
        /// </exception>
        public int this[int index]
        {
            get { return data[index]; }
            private set { data[index] = value; }
        }


        /// <summary>
        /// The available number of integers to choose from.
        /// </summary>
        /// <remarks>
        /// Also known as <em>n</em>.
        /// </remarks>
        public int Choices => choices;


        /// <summary>
        /// Number of elements in the <see cref="Combination"/>.
        /// </summary>
        /// <remarks>
        /// Also known as <em>k</em>.
        /// </remarks>
        public int Picks => data.Length;


        /// <summary>
        /// Row index in the ordered <see cref="Combination"/> table.
        /// </summary>
        /// <remarks>
        /// Any assigned value out of range will be normalized to (0..<see cref="RowCount"/>-1).
        /// </remarks>
        /// <example>
        /// <code source="..\Examples\Combination\CnExample04\CnExample04.cs" lang="cs" />
        /// </example>
        public long Rank
        {
            get { return rank; }
            set
            {
                if (RowCount == 0)
                    return;

                // Normalize the new rank.
                if (value < 0)
                {
                    rank = value % RowCount;
                    if (rank < 0)
                        rank += RowCount;
                }
                else
                    rank = value < RowCount? value : value % RowCount;

                //
                // Perform unranking:
                //

                long diminishingRank = RowCount - rank - 1;
                int combinaticAtom = Choices;

                for (int ki = Picks; ki > 0; --ki)
                    for (;;)
                    {
                        --combinaticAtom;

                        long trialCount = Combinatoric.BinomialCoefficient (combinaticAtom, ki);
                        if (trialCount <= diminishingRank)
                        {
                            diminishingRank -= trialCount;
                            this[Picks - ki] = Choices - combinaticAtom - 1;
                            break;
                        }
                    }
            }
        }


        /// <summary>
        /// Count of distinct sequences in the <see cref="Combination"/> table.
        /// </summary>
        public long RowCount => rowCount;

        #endregion

        #region Instance methods

        /// <summary>Compare two <see cref="Combination"/>s.</summary>
        /// <param name="obj">Target of the comparison.</param>
        /// <returns>
        /// A signed integer indicating the sort order of this instance to <em>obj</em>.
        /// </returns>
        public int CompareTo (object obj) => CompareTo (obj as Combination);


        /// <summary>Compare two <see cref="Combination"/>s.</summary>
        /// <param name="other">Target of the comparison.</param>
        /// <returns>
        /// A signed integer indicating the sort order of this instance to <em>other</em>.
        /// </returns>
        public int CompareTo (Combination other)
        {
            if ((object) other == null)
                return 1;

            int result = this.Picks - other.Picks;
            if (result == 0)
            {
                result = this.Choices - other.Choices;
                if (result == 0)
                {
                    long rankDiff = this.Rank - other.Rank;
                    result = rankDiff == 0 ? 0 : rankDiff < 0 ? -1 : 1;
                }
            }

            return result;
        }


        /// <summary>
        /// Copy the entire sequence to the supplied destination.
        /// </summary>
        /// <param name="array">Destination of copy.</param>
        /// <exception cref="ArgumentNullException">When <em>array</em> is <b>null</b>.</exception>
        /// <exception cref="ArgumentException">When not enough space in <em>array</em>.</exception>
        public void CopyTo (int[] array)
        {
            if (array == null)
                throw new ArgumentNullException (nameof (array));

            if (array.Length < Picks)
                throw new ArgumentException ("Destination array is not long enough.");

            this.data.CopyTo (array, 0);
        }


        /// <summary>
        /// Indicate whether two <see cref="Combination"/>s have the same value.
        /// </summary>
        /// <param name="obj">Target of the comparison.</param>
        /// <returns>
        /// <b>true</b> if <em>obj</em> has the same value as this object; otherwise, <b>false</b>.
        /// </returns>
        public override bool Equals (object obj)
            => Equals (obj as Combination);


        /// <summary>
        /// Indicate whether two <see cref="Combination"/>s have the same value.
        /// </summary>
        /// <param name="other">Target of the comparison.</param>
        /// <returns>
        /// <b>true</b> if <em>other</em> has the same value as this instance;
        /// otherwise, <b>false</b>.
        /// </returns>
        public bool Equals (Combination other)
            => (object) other != null && other.Rank == Rank && other.Choices == Choices && other.Picks == Picks;


        /// <summary>Get an object-based enumerator of the elements.</summary>
        /// <returns>Object-based elemental enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();


        /// <summary>Enumerate all elements of a <see cref="Combination"/>.</summary>
        /// <returns>
        /// An <c>IEnumerator&lt;int&gt;</c> for this <see cref="Combination"/>.
        /// </returns>
        /// <example>
        /// <code source="..\Examples\Combination\CnExample05\CnExample05.cs" lang="cs" />
        /// </example>
        public IEnumerator<int> GetEnumerator()
        {
            foreach (int element in this.data)
                yield return element;
        }


        /// <summary>Get the hash oode of the <see cref="Combination"/>.</summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode() => unchecked ((int) Rank);


        /// <summary>
        /// Iterate thru all rows of the <see cref="Combination"/> table
        /// for every value of <see cref="Rank"/> ascending.
        /// </summary>
        /// <returns>An iterator for a <see cref="Combination"/> table.</returns>
        /// <remarks>
        /// If the start row is not of <see cref="Rank"/> 0, the iteration will wrap around
        /// so that <see cref="RowCount"/> items are always produced.
        /// </remarks>
        /// <example>
        /// <code source="..\Examples\Combination\CnExample01\CnExample01.cs" lang="cs" />
        /// </example>
        public IEnumerable<Combination> GetRows()
        {
            if (RowCount != 0)
                for (var beginRank = Rank;;)
                {
                    yield return this;
                    Rank = Rank + 1;
                    if (Rank == beginRank)
                        break;
                }
        }


        /// <summary>
        /// Iterate thru all rows of all <see cref="Combination"/> tables for every
        /// pick in the range (1..<see cref="Picks"/>).
        /// </summary>
        /// <returns>An iterator for a series of <see cref="Combination"/> tables.</returns>
        /// <example>
        /// <code source="..\Examples\Combination\CnExample02\CnExample02.cs" lang="cs" />
        /// </example>
        public IEnumerable<Combination> GetRowsForAllPicks()
        {
            var beginRank = this.rank;
            var beginData = this.data;

            for (int p = 1; p <= beginData.Length; ++p)
            {
                this.data = new int[p];
                for (int e = 0; e < p; ++e)
                    this[e] = e;
                this.rank = 0;
                this.rowCount = Combinatoric.BinomialCoefficient (Choices, p);

                do
                {
                    yield return this;
                    Rank = Rank + 1;
                } while (Rank != 0);
            }

            this.data = beginData;
            this.rank = beginRank;
        }


        /// <summary>
        /// Provide a readable form of the <see cref="Combination"/> sequence.
        /// </summary>
        /// <returns>A <c>string</c> that represents the sequence.</returns>
        /// <remarks>Result is enclosed in braces and separated by commas.</remarks>
        /// <example>
        /// <code source="..\Examples\Combination\CnExample04\CnExample04.cs" lang="cs" />
        /// </example>
        public override string ToString()
        {
            if (RowCount == 0)
                return ("{ }");

            var result = new StringBuilder ("{ ");

            for (int ei = 0;;)
            {
                result.Append (this[ei]);

                ++ei;
                if (ei >= Picks)
                    break;

                result.Append (", ");
            }

            result.Append (" }");
            return result.ToString();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Apply a <see cref="Combination"/> sequence to rearrange the supplied list or array.
        /// </summary>
        /// <typeparam name="T">Type of items to rearrange.</typeparam>
        /// <param name="arrangement">New arrangement for items.</param>
        /// <param name="source">List of items to rearrange.</param>
        /// <returns>List of rearranged items.</returns>
        /// <example>
        /// <code source="..\Examples\Combination\CnExample03\CnExample03.cs" lang="cs" />
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// When <em>arrangement</em> or <em>source</em> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// When length of <em>source</em> is less than <see cref="Picks"/>.
        /// </exception>
        public static List<T> Permute<T> (Combination arrangement, IList<T> source)
        {
            if (arrangement == null)
                throw new ArgumentNullException (nameof (arrangement));

            if (source == null)
                throw new ArgumentNullException (nameof (source));

            if (source.Count < arrangement.Choices)
                throw new ArgumentException ("Not enough supplied values.", nameof (source));

            var result = new List<T> (arrangement.Picks);
            foreach (int element in arrangement)
                result.Add (source[element]);

            return result;
        }


        /// <summary>Indicate whether 2 <see cref="Combination"/>s are equal.</summary>
        /// <param name="param1">A <see cref="Combination"/> sequence.</param>
        /// <param name="param2">A <see cref="Combination"/> sequence.</param>
        /// <returns><b>true</b> if supplied sequences are equal;
        /// otherwise, <b>false</b>.</returns>
        public static bool operator == (Combination param1, Combination param2)
            => (object) param1 == null ? (object) param2 == null : param1.Equals (param2);


        /// <summary>Indicate whether 2 <see cref="Combination"/>s are not equal.</summary>
        /// <param name="param1">A <see cref="Combination"/> sequence.</param>
        /// <param name="param2">A <see cref="Combination"/> sequence.</param>
        /// <returns><b>true</b> if supplied sequences are not equal;
        /// otherwise, <b>false</b>.</returns>
        public static bool operator != (Combination param1, Combination param2)
            => (object) param1 == null ? (object) param2 != null : ! param1.Equals (param2);


        /// <summary>Indicate whether the left <see cref="Combination"/> is less than
        /// the right <see cref="Combination"/>.</summary>
        /// <param name="param1">A <see cref="Combination"/> sequence.</param>
        /// <param name="param2">A <see cref="Combination"/> sequence.</param>
        /// <returns><b>true</b> if the left sequence is less than
        /// the right sequence; otherwise, <b>false</b>.</returns>
        public static bool operator < (Combination param1, Combination param2)
            => (object) param1 == null ? (object) param2 != null : param1.CompareTo (param2) < 0;


        /// <summary>Indicate whether the left <see cref="Combination"/> is greater than
        /// or equal to the right <see cref="Combination"/>.</summary>
        /// <param name="param1">A <see cref="Combination"/> sequence.</param>
        /// <param name="param2">A <see cref="Combination"/> sequence.</param>
        /// <returns><b>true</b> if the left sequence is greater than or equal to
        /// the right sequence; otherwise, <b>false</b>.</returns>
        public static bool operator >= (Combination param1, Combination param2)
            => (object) param1 == null ? (object) param2 == null : param1.CompareTo (param2) >= 0;


        /// <summary>Indicate whether the left <see cref="Combination"/> is greater than
        /// the right <see cref="Combination"/>.</summary>
        /// <param name="param1">A <see cref="Combination"/> sequence.</param>
        /// <param name="param2">A <see cref="Combination"/> sequence.</param>
        /// <returns><b>true</b> if the left sequence is greater than
        /// the right sequence; otherwise, <b>false</b>.</returns>
        public static bool operator > (Combination param1, Combination param2)
            => (object) param1 == null ? false : param1.CompareTo (param2) > 0;


        /// <summary>Indicate whether the left <see cref="Combination"/> is less than or equal
        /// to the right <see cref="Combination"/>.</summary>
        /// <param name="param1">A <see cref="Combination"/> sequence.</param>
        /// <param name="param2">A <see cref="Combination"/> sequence.</param>
        /// <returns><b>true</b> if the left sequence is less than or equal to
        /// the right sequence; otherwise, <b>false</b>.</returns>
        public static bool operator <= (Combination param1, Combination param2)
            => (object) param1 == null ? true : param1.CompareTo (param2) <= 0;

        #endregion
    }
}
