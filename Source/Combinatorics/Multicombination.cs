//
// Project: KaosCombinatorics
// File: Multicombination.cs
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
    /// Represents an ascending sequence of repeating picks from a supplied range.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The defining variables of a multicombination
    /// are <em>n</em> which is the number of possible choices
    /// and <em>k</em> which is the number of repeatable picks from those choices.
    /// When <em>k</em> is less than <em>n</em>, this is a <em>k</em>-multicombination
    /// also known as a <em>k</em>-combination with repetition.
    /// Multicombinations are contrasted to combinations where the values in the sequence do not repeat.
    /// </para>
    /// <para>
    /// The <see cref="Multicombination"/> class produces <em>k</em>-multicombinations with
    /// ascending elements that may repeat as many as <em>k</em> times. 
    /// While sequence order of the elements is not a general requirement of multicombinations,
    /// producing an ascending sequence allows ranking the rows into an ordered table.
    /// </para>
    /// <para>
    /// Use the <see cref="Picks"/> property to get the number of elements (<em>k</em>)
    /// of a <see cref="Multicombination"/> taken from a possible number of
    /// <see cref="Choices"/> (<em>n</em>).
    /// Use the <see cref="RowCount"/> property to get the number of distinct possible
    /// <see cref="Multicombination"/> sequences.
    /// 
    /// Use the <see cref="P:Kaos.Combinatorics.Multicombination.Item(System.Int32)">indexer</see>
    /// to get a specified element of the sequence.
    ///
    /// Use the <see cref="GetEnumerator">default enumerator</see> to iterate thru
    /// the elements of a <see cref="Multicombination"/>.
    /// 
    /// Use the <see cref="Permute">Permute</see> method to pick objects from a supplied array
    /// of choices based on the current sequence.
    /// </para>
    /// <para>
    /// Use the <see cref="Rank"/> property to get or set the row index in a lexicographically
    /// ordered <see cref="Multicombination"/> table of all possible non-descending sequences.
    /// 
    /// Use <see cref="GetRows"/> to iterate thru all possible sequences of the
    /// <see cref="Multicombination"/> ordered by <see cref="Rank"/>.
    /// 
    /// Use <see cref="GetRowsForPicks">GetRowsForPicks (startPick, stopPick)</see> to iterate
    /// thru every table of all picks in the range (<em>startPick</em>..<em>stopPick</em>).
    /// </para>
    /// <para>
    /// The default appearance of a <see cref="Multicombination"/> row is a list of
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
    ///     Define a subclass of <see cref="Multicombination"/> and override
    ///     <see cref="System.Object.ToString">ToString()</see>.
    ///     (See the <see cref="GetRowsForPicks">GetRowsForPicks</see> method
    ///     for an example.)
    ///   </li>
    /// </ul>
    /// </para>
    /// <para>
    /// For more information about <em>k</em>-multicombinations, see:
    /// </para>
    /// <para>
    /// <em>https://en.wikipedia.org/wiki/Combination#Number_of_combinations_with_repetition</em>
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// Iterating thru <c>new Multicombination (4, 3).GetRows()</c> produces:
    /// </para>
    /// <para>
    /// <c>{ 0, 0, 0 }</c><br/>
    /// <c>{ 0, 0, 1 }</c><br/>
    /// <c>{ 0, 0, 2 }</c><br/>
    /// <c>{ 0, 0, 3 }</c><br/>
    /// <c>{ 0, 1, 1 }</c><br/>
    /// <c>{ 0, 1, 2 }</c><br/>
    /// <c>{ 0, 1, 3 }</c><br/>
    /// <c>{ 0, 2, 2 }</c><br/>
    /// <c>{ 0, 2, 3 }</c><br/>
    /// <c>{ 0, 3, 3 }</c><br/>
    /// <c>{ 1, 1, 1 }</c><br/>
    /// <c>{ 1, 1, 2 }</c><br/>
    /// <c>{ 1, 1, 3 }</c><br/>
    /// <c>{ 1, 2, 2 }</c><br/>
    /// <c>{ 1, 2, 3 }</c><br/>
    /// <c>{ 1, 3, 3 }</c><br/>
    /// <c>{ 2, 2, 2 }</c><br/>
    /// <c>{ 2, 2, 3 }</c><br/>
    /// <c>{ 2, 3, 3 }</c><br/>
    /// <c>{ 3, 3, 3 }</c>
    /// </para>
    /// </example>
    public class Multicombination :
        IComparable,
        System.Collections.IEnumerable,
        IComparable<Multicombination>,
        IEquatable<Multicombination>,
        IEnumerable<int>
    {
        private int[] data;     // The picks for the current rank. Length is 'k'.
        private int choices;    // Number of possible values 'n'.
        private long rowCount;  // Row count of the table of k-multicombinations.
        private long rank;      // Row index.

        #region Constructors

        /// <summary>Initializes an empty multicombination instance.</summary>
        public Multicombination()
        {
            this.data = new int[0];
            this.choices = 0;
            this.rowCount = 0;
            this.rank = 0;
        }


        /// <summary>Initializes a new instance that is copied from the supplied multicombination.</summary>
        /// <param name="source">Instance to copy.</param>
        /// <exception cref="ArgumentNullException">When <em>source</em> is <b>null</b>.</exception>
        public Multicombination (Multicombination source)
        {
            if (source == null)
                throw new ArgumentNullException (nameof (source));

            this.data = new int[source.Picks];
            source.data.CopyTo (this.data, 0);

            this.choices = source.Choices;
            this.rowCount = source.RowCount;
            this.rank = source.Rank;
        }


        /// <summary>Initializes a new multicombination of <see cref="Rank"/> 0 with the supplied number of elements.</summary>
        /// <param name="choices">Number of elements in the sequence.</param>
        /// <exception cref="ArgumentOutOfRangeException">When <em>choices</em> less than 0.</exception>
        public Multicombination (int choices)
        {
            if (choices < 0)
                throw new ArgumentOutOfRangeException (nameof (choices), "Value is less than zero.");

            this.data = new int[choices];
            this.choices = choices;
            this.rank = 0;
            CalcRowCount();
        }


        /// <summary>
        /// Initializes a new multicombination of <see cref="Rank"/> 0
        /// with the supplied number of <em>picks</em> from the supplied number of <em>choices</em>.
        /// </summary>
        /// <param name="choices">Number of values to pick from.</param>
        /// <param name="picks">Number of elements in the sequence.</param>
        /// <remarks>
        /// Supplying a value for <em>choices</em> that is greater than <em>picks</em>
        /// will instantiate a <em>k</em>-multicombination also known as a <em>k</em>-combination with repetition.
        /// </remarks>
        /// <example><code source="..\Examples\Multicombination\McExample01\McExample01.cs" lang="cs"/></example>
        /// <exception cref="ArgumentOutOfRangeException">When negative value supplied; when <em>choices</em> is 0 and <em>picks</em> is not 0.</exception>
        /// <exception cref="OverflowException">When the numbers are just too big.</exception>
        public Multicombination (int choices, int picks)
        {
            if (choices < 0)
                throw new ArgumentOutOfRangeException (nameof (choices), "Value is less than zero.");
            if (picks < 0)
                throw new ArgumentOutOfRangeException (nameof (picks), "Value is less than zero.");
            if (choices == 0 && picks > 0)
                throw new ArgumentOutOfRangeException (nameof (choices), "Value is zero and picks is nonzero.");

            this.data = new int[picks];
            this.choices = choices;
            this.rank = 0;
            CalcRowCount();
        }


        /// <summary>
        /// Initializes a new multicombination of the supplied <em>rank</em>
        /// with the supplied number of <em>picks</em> from the supplied number of <em>choices</em>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Supplying a value for <em>choices</em> that is greater than <em>picks</em>
        /// will instantiate a <em>k</em>-multicombination also known as a <em>k</em>-combination with repetition.
        /// </para>
        /// <para>
        /// If the supplied <em>rank</em> is out of the range (0..<see cref="RowCount"/>-1),
        /// it will be normalized to the valid range.
        /// For example, a value of -1 will produce the last row in the ordered table.
        /// </para>
        /// </remarks>
        /// <param name="choices">Number of values to pick from.</param>
        /// <param name="picks">Number of elements in the sequence.</param>
        /// <param name="rank">Row index in the ordered <see cref="Multicombination"/> table.</param>
        /// <example><code source="..\Examples\Multicombination\McExample05\McExample05.cs" lang="cs"/></example>
        /// <exception cref="ArgumentOutOfRangeException">When negative value supplied; when <em>choices</em> is 0 and <em>picks</em> is not 0.</exception>
        /// <exception cref="OverflowException">When too many <em>choices</em>.</exception>
        public Multicombination (int choices, int picks, long rank)
        {
            if (choices < 0)
                throw new ArgumentOutOfRangeException (nameof (choices), "Value is less than zero.");
            if (picks < 0)
                throw new ArgumentOutOfRangeException (nameof (picks), "Value is less than zero.");
            if (choices == 0 && picks > 0)
                throw new ArgumentOutOfRangeException (nameof (choices), "Value is zero and picks is nonzero.");

            this.data = new int[picks];
            this.choices = choices;
            CalcRowCount();
            Rank = rank;
        }


        /// <summary>
        /// Initializes a new multicombination from elements supplied in <em>source</em> picked
        /// from the supplied number of <em>choices</em>.
        /// </summary>
        /// <param name="choices">Number of values to pick from.</param>
        /// <param name="source">Array of integers.</param>
        /// <remarks>
        /// Supplying a value for <em>choices</em> that is greater than the number of elements in <em>source</em>
        /// will instantiate a <em>k</em>-multicombination also known as a <em>k</em>-combination with repetition.
        /// </remarks>
        /// <example><code source="..\Examples\Multicombination\McExample04\McExample04.cs" lang="cs"/></example>
        /// <exception cref="ArgumentNullException">When <em>source</em> is <b>null</b>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">When <em>source</em> contains invalid data; when <em>choices</em> is 0 and <em>source</em> is not empty.</exception>
        public Multicombination (int choices, int[] source)
        {
            if (source == null)
                throw new ArgumentNullException (nameof (source));
            if (choices < 0)
                throw new ArgumentOutOfRangeException (nameof (choices), "Value is less than zero.");
            if (choices == 0 && source.Length > 0)
                throw new ArgumentOutOfRangeException (nameof (choices), "Value is zero and picks is nonzero.");

            this.data = new int[source.Length];
            source.CopyTo (this.data, 0);
            Array.Sort (this.data);

            this.choices = choices;
            CalcRowCount();

            foreach (int element in this.data)
                if (element < 0 || element >= choices)
                    throw new ArgumentOutOfRangeException (nameof (source), "Element is out of range.");

            //
            // Perform ranking:
            //

            this.rank = 0;
            if (RowCount == 0)
                return;

            int comboElement = this[0];
            int ji = 0;
            for (int ki = 0;;)
            {
                for (; ji < comboElement; ++ji)
                    this.rank += Combinatoric.BinomialCoefficient (Choices + Picks - ji - 2, Picks - ki - 1);

                ++ki;
                if (ki >= Picks)
                    break;

                ji = comboElement + 1;
                comboElement = this[ki] - this[ki-1] + ji;
            }
        }

        #endregion

        #region Private methods

        private void CalcRowCount()
            => rowCount = Picks == 0 ? 0 : Combinatoric.BinomialCoefficient (Picks + Choices - 1, Picks);

        #endregion

        #region Properties

        /// <summary>Get a element of the <see cref="Multicombination"/> at the supplied column.</summary>
        /// <param name="index">Zero-based index value.</param>
        /// <returns>Sequence value at <em>index</em>.</returns>
        /// <example><code source="..\Examples\Multicombination\McExample05\McExample05.cs" lang="cs"/></example>
        /// <exception cref="IndexOutOfRangeException">When <em>index</em> not in range (0..<see cref="Picks"/>-1).</exception>
        public int this[int index]
        {
            get { return data[index]; }
            private set { data[index] = value; }
        }


        /// <summary>The available number of integers to choose from.</summary>
        /// <remarks>Also known as <em>n</em>.</remarks>
        public int Choices => choices;


        /// <summary>Number of elements in the <see cref="Multicombination"/>.</summary>
        /// <remarks>Also known as <em>k</em>.</remarks>
        public int Picks => data.Length;


        /// <summary>Row index in the ordered <see cref="Multicombination"/> table.</summary>
        /// <remarks>Any assigned value out of range will be normalized to (0..<see cref="RowCount"/>-1).</remarks>
        /// <example><code source="..\Examples\Multicombination\McExample04\McExample04.cs" lang="cs"/></example>
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
                    rank = value < RowCount ? value : value % RowCount;

                //
                // Perform unranking:
                //

                long diminishingRank = RowCount - rank - 1;
                int combinaticAtom = Choices + Picks - 1;

                for (int ki = Picks; ki > 0; --ki)
                    for (;;)
                    {
                        --combinaticAtom;

                        long trialCount = Combinatoric.BinomialCoefficient (combinaticAtom, ki);
                        if (trialCount <= diminishingRank)
                        {
                            diminishingRank -= trialCount;
                            this[Picks - ki] = Choices - combinaticAtom + ki - 2;
                            break;
                        }
                    }
            }
        }


        /// <summary>Count of distinct sequences in the <see cref="Multicombination"/> table.</summary>
        public long RowCount => rowCount;

        #endregion

        #region Instance methods

        /// <summary>Compare two <see cref="Multicombination"/>s.</summary>
        /// <param name="obj">Target of the comparison.</param>
        /// <returns>A signed integer indicating the sort order of this instance to <em>obj</em>.</returns>
        public int CompareTo (object obj) => CompareTo (obj as Multicombination);


        /// <summary>Compare two <see cref="Multicombination"/>s.</summary>
        /// <param name="other">Target of the comparison.</param>
        /// <returns>A signed integer indicating the sort order of this instance to <em>other</em>.</returns>
        public int CompareTo (Multicombination other)
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

                    if (rankDiff == 0)
                        result = 0;
                    else
                        result = rankDiff < 0 ? -1 : 1;
                }
            }

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
            if (array.Length < Picks)
                throw new ArgumentException ("Destination array is not long enough.");

            this.data.CopyTo (array, 0);
        }


        /// <summary>Indicate whether two <see cref="Multicombination"/>s have the same value.</summary>
        /// <param name="obj">Target of the comparison.</param>
        /// <returns><b>true</b> if <em>obj</em> has the same value as this object; otherwise, <b>false</b>.</returns>
        public override bool Equals (object obj)
            => Equals (obj as Multicombination);


        /// <summary>Indicate whether two <see cref="Multicombination"/>s have the same value.</summary>
        /// <param name="other">Target of the comparison.</param>
        /// <returns><b>true</b> if <em>other</em> has the same value as this instance; otherwise, <b>false</b>.</returns>
        public bool Equals (Multicombination other)
            => (object) other != null && other.Rank == Rank && other.Choices == Choices && other.Picks == Picks;


        /// <summary>Get an object-based enumerator of the elements.</summary>
        /// <returns>Object-based elemental enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();


        /// <summary>Enumerate all elements of a <see cref="Multicombination"/>.</summary>
        /// <returns>An <see cref="System.Collections.Generic.IEnumerator{T}"/> for this <see cref="Multicombination"/>.</returns>
        /// <example><code source="..\Examples\Multicombination\McExample05\McExample05.cs" lang="cs"/></example>
        public IEnumerator<int> GetEnumerator()
        {
            foreach (int element in this.data)
                yield return element;
        }


        /// <summary>Get the hash oode of the <see cref="Multicombination"/>.</summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode() => unchecked ((int) Rank);


        /// <summary>
        /// Iterate thru all rows of the <see cref="Multicombination"/> table for every value of <see cref="Rank"/> ascending.
        /// </summary>
        /// <returns>An iterator for a <see cref="Multicombination"/> table.</returns>
        /// <remarks>
        /// If the start row is not of <see cref="Rank"/> 0, the iteration will wrap around
        /// so that <see cref="RowCount"/> items are always produced.
        /// </remarks>
        /// <example><code source="..\Examples\Multicombination\McExample01\McExample01.cs" lang="cs"/></example>
        public IEnumerable<Multicombination> GetRows()
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
        /// Iterate thru all rows of all <see cref="Multicombination"/> tables for every pick
        /// in the range (<em>startPicks</em>..<em>stopPicks</em>).
        /// </summary>
        /// <returns>An iterator for a series of <see cref="Multicombination"/> tables.</returns>
        /// <remarks>Unlike <see cref="Combination"/>, <see cref="Picks"/> may exceed <see cref="Choices"/>.</remarks>
        /// <param name="startPicks">Number of picks for first table.</param>
        /// <param name="stopPicks">Number of picks for last table.</param>
        /// <example><code source="..\Examples\Multicombination\McExample02\McExample02.cs" lang="cs"/></example>
        /// <exception cref="ArgumentOutOfRangeException">When <em>startPicks</em> less than 0 or greater than <em>stopPicks</em>.</exception>
        public IEnumerable<Multicombination> GetRowsForPicks (int startPicks, int stopPicks)
        {
            if (startPicks < 0 || startPicks > stopPicks)
                throw new ArgumentOutOfRangeException (nameof (startPicks), "Pick range is not valid.");

            if (Choices == 0)
                yield break;

            var beginRank = this.rank;
            var beginData = this.data;

            for (int p = Math.Max (1, startPicks); p <= stopPicks; ++p)
            {
                this.data = new int[p];
                this.rank = 0;
                CalcRowCount();

                do
                {
                    yield return this;
                    Rank = Rank + 1;
                } while (Rank != 0);
            }

            this.data = beginData;
            this.rank = beginRank;
        }


        /// <summary>Provide a readable form of the <see cref="Multicombination"/> sequence.</summary>
        /// <returns>A <c>string</c> that represents the sequence.</returns>
        /// <remarks>Result is enclosed in braces and separated by commas.</remarks>
        /// <example><code source="..\Examples\Multicombination\McExample04\McExample04.cs" lang="cs"/></example>
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

        /// <summary>Apply a <see cref="Multicombination"/> sequence to rearrange the supplied list or array.</summary>
        /// <typeparam name="T">Type of items to rearrange.</typeparam>
        /// <param name="arrangement">New arrangement for items.</param>
        /// <param name="source">List of items to rearrange.</param>
        /// <returns>List of rearranged items.</returns>
        /// <example><code source="..\Examples\Multicombination\McExample03\McExample03.cs" lang="cs"/></example>
        /// <exception cref="ArgumentNullException">When <em>arrangement</em> or <em>source</em> is <b>null</b>.</exception>
        /// <exception cref="ArgumentException">When length of <em>source</em> is less than arrangement.<see cref="Choices"/>.</exception>
        public static List<T> Permute<T> (Multicombination arrangement, IList<T> source)
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


        /// <summary>Indicate whether 2 <see cref="Multicombination"/>s are equal.</summary>
        /// <param name="param1">A <see cref="Multicombination"/> sequence.</param>
        /// <param name="param2">A <see cref="Multicombination"/> sequence.</param>
        /// <returns><b>true</b> if supplied sequences are equal;
        /// otherwise, <b>false</b>.</returns>
        public static bool operator == (Multicombination param1, Multicombination param2)
            => (object) param1 == null ? (object) param2 == null : param1.Equals (param2);


        /// <summary>Indicate whether 2 <see cref="Multicombination"/>s are not equal.</summary>
        /// <param name="param1">A <see cref="Multicombination"/> sequence.</param>
        /// <param name="param2">A <see cref="Multicombination"/> sequence.</param>
        /// <returns><b>true</b> if supplied sequences are not equal;
        /// otherwise, <b>false</b>.</returns>
        public static bool operator != (Multicombination param1, Multicombination param2)
            => (object) param1 == null ? (object) param2 != null : ! param1.Equals (param2);


        /// <summary>Indicate whether the left <see cref="Multicombination"/> is less than
        /// the right <see cref="Multicombination"/>.</summary>
        /// <param name="param1">A <see cref="Multicombination"/> sequence.</param>
        /// <param name="param2">A <see cref="Multicombination"/> sequence.</param>
        /// <returns><b>true</b> if the left sequence is less than
        /// the right sequence; otherwise, <b>false</b>.</returns>
        public static bool operator < (Multicombination param1, Multicombination param2)
            => (object) param1 == null ? (object) param2 != null : param1.CompareTo (param2) < 0;


        /// <summary>Indicate whether the left <see cref="Multicombination"/> is greater than
        /// or equal to the right <see cref="Multicombination"/>.</summary>
        /// <param name="param1">A <see cref="Multicombination"/> sequence.</param>
        /// <param name="param2">A <see cref="Multicombination"/> sequence.</param>
        /// <returns><b>true</b> if the left sequence is greater than or equal to
        /// the right sequence; otherwise, <b>false</b>.</returns>
        public static bool operator >= (Multicombination param1, Multicombination param2)
            => (object) param1 == null ? (object) param2 == null : param1.CompareTo (param2) >= 0;


        /// <summary>Indicate whether the left <see cref="Multicombination"/> is greater than
        /// the right <see cref="Multicombination"/>.</summary>
        /// <param name="param1">A <see cref="Multicombination"/> sequence.</param>
        /// <param name="param2">A <see cref="Multicombination"/> sequence.</param>
        /// <returns><b>true</b> if the left sequence is greater than
        /// the right sequence; otherwise, <b>false</b>.</returns>
        public static bool operator > (Multicombination param1, Multicombination param2)
            => (object) param1 == null ? false : param1.CompareTo (param2) > 0;


        /// <summary>Indicate whether the left <see cref="Multicombination"/> is less than
        /// or equal to the right <see cref="Multicombination"/>.</summary>
        /// <param name="param1">A <see cref="Multicombination"/> sequence.</param>
        /// <param name="param2">A <see cref="Multicombination"/> sequence.</param>
        /// <returns><b>true</b> if the left sequence is less than or equal to
        /// the right sequence; otherwise, <b>false</b>.</returns>
        public static bool operator <= (Multicombination param1, Multicombination param2)
            => (object) param1 == null ? true : param1.CompareTo (param2) <= 0;

        #endregion
    }
}
