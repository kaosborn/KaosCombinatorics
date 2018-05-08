//
// Project: KaosCombinatorics
// File: Permutation.cs
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
    /// Represents a specific arrangement of distinct picks from a supplied range.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The defining variables of a permutation
    /// are <em>n</em> which is the number of possible choices
    /// and <em>k</em> which is the number of distinct picks from those choices.
    /// When <em>k</em> is less than <em>n</em>, this is a <em>k</em>-permutation
    /// also known as a variation.
    /// Permutations are contrasted to combinations where the arrangement is generally not significant (except for ranking).
    /// </para>
    /// <para>
    /// The <see cref="Permutation"/> class uses the inherent sequencing of the elements
    /// to arrange the rows into an lexicographically ordered table.
    /// 
    /// Support for <em>k</em>-permutations is provided by supplying a <em>picks</em> value
    /// that is less than the supplied <em>choices</em> value to the appropriate constructors.
    /// </para>
    /// <para>
    /// Use the <see cref="Choices"/> property to get the number of elements to choose from.
    /// 
    /// Use the <see cref="Picks"/> property to get the number of elements of a
    /// <see cref="Permutation"/>.
    /// 
    /// Use the <see cref="RowCount"/> property to get the number of distinct possible
    /// sequences of a <see cref="Permutation"/>.
    /// 
    /// Use the <see cref="P:Kaos.Combinatorics.Permutation.Item(System.Int32)">indexer</see>
    /// to get a specified element of the sequence.
    /// 
    /// Use the <see cref="GetEnumerator">default enumerator</see> to iterate thru
    /// the elements of a <see cref="Permutation"/>.
    /// 
    /// Use the <see cref="Permute">Permute</see> method to
    /// rearrange a supplied array based on the current sequence.
    /// </para>
    /// <para>
    /// Use the <see cref="Rank"/> property to get or set the row index in a lexicographically
    /// ordered <see cref="Permutation"/> table of all possible sequences.
    /// 
    /// Use <see cref="GetRows"/> to iterate thru all possible sequences
    /// of the <see cref="Permutation"/> ordered by <see cref="Rank"/>.
    /// 
    /// Use <see cref="GetRowsForAllChoices"/> to iterate
    /// thru every table of all choices in the range (1..<see cref="Choices"/>).
    /// 
    /// Use <see cref="GetRowsForAllPicks"/> to iterate
    /// thru every table of all picks in the range (1..<see cref="Picks"/>).
    /// </para>
    /// <para>
    /// Use the <see cref="PlainRank"/> property to get or set the row index in a table
    /// ordered for plain changes where adjacent rows differ by only a single swap of
    /// 2 adjacent elements.
    /// 
    /// Use <see cref="GetRowsOfPlainChanges"/> to iterate thru all possible sequences
    /// of a <see cref="Permutation"/> ordered by <see cref="PlainRank"/>.
    /// </para>
    /// <para>
    /// Use the <see cref="Swaps"/> property to get the number of element swaps that would
    /// transform a row into the sequence of <see cref="Rank"/> 0.
    /// 
    /// Use the <see cref="Backtrack">Backtrack</see> method to minimally advance
    /// <see cref="Rank"/> while changing a specified element.
    /// </para>
    /// <para>
    /// The default appearance of a <see cref="Permutation"/> row is a list of integers
    /// (starting at 0) enclosed in braces. The appearance may be tailored 3 ways:
    /// <ul>
    ///   <li>
    ///     Map each element to a different value using the
    ///     <see cref="GetEnumerator">default enumerator</see> or the
    ///     <see cref="P:Kaos.Combinatorics.Permutation.Item(System.Int32)">indexer</see>.
    ///   </li>
    ///   <li>
    ///     Use the <see cref="Permute">Permute</see> method and output the rearranged values.
    ///   </li>
    ///   <li>
    ///     Define a subclass of <see cref="Permutation"/> and override
    ///     <see cref="System.Object.ToString">ToString()</see>.
    ///     (See <see cref="GetRowsForAllPicks"/> for an example.)
    ///   </li>
    /// </ul>
    /// </para>
    /// <para>
    /// For more information about permutations and <em>k</em>-permutations, see:
    /// </para>
    /// <para>
    /// <em>https://en.wikipedia.org/wiki/Permutation</em><br/>
    /// <em>https://en.wikipedia.org/wiki/Eight_queens_puzzle</em>
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// Iterating thru <c>new Permutation (4).GetRows()</c> produces:
    /// </para>
    /// <para>
    /// <c>{ 0, 1, 2, 3 }</c><br/>
    /// <c>{ 0, 1, 3, 2 }</c><br/>
    /// <c>{ 0, 2, 1, 3 }</c><br/>
    /// <c>{ 0, 2, 3, 1 }</c><br/>
    /// <c>{ 0, 3, 1, 2 }</c><br/>
    /// <c>{ 0, 3, 2, 1 }</c><br/>
    /// <c>{ 1, 0, 2, 3 }</c><br/>
    /// <c>{ 1, 0, 3, 2 }</c><br/>
    /// <c>{ 1, 2, 0, 3 }</c><br/>
    /// <c>{ 1, 2, 3, 0 }</c><br/>
    /// <c>{ 1, 3, 0, 2 }</c><br/>
    /// <c>{ 1, 3, 2, 0 }</c><br/>
    /// <c>{ 2, 0, 1, 3 }</c><br/>
    /// <c>{ 2, 0, 3, 1 }</c><br/>
    /// <c>{ 2, 1, 0, 3 }</c><br/>
    /// <c>{ 2, 1, 3, 0 }</c><br/>
    /// <c>{ 2, 3, 0, 1 }</c><br/>
    /// <c>{ 2, 3, 1, 0 }</c><br/>
    /// <c>{ 3, 0, 1, 2 }</c><br/>
    /// <c>{ 3, 0, 2, 1 }</c><br/>
    /// <c>{ 3, 1, 0, 2 }</c><br/>
    /// <c>{ 3, 1, 2, 0 }</c><br/>
    /// <c>{ 3, 2, 0, 1 }</c><br/>
    /// <c>{ 3, 2, 1, 0 }</c>
    /// </para>
    /// <para>
    /// Iterating thru <c>new Permutation (4, 3).GetRows()</c> produces:
    /// </para>
    /// <para>
    /// <c>{ 0, 1, 2 }</c><br/>
    /// <c>{ 0, 1, 3 }</c><br/>
    /// <c>{ 0, 2, 1 }</c><br/>
    /// <c>{ 0, 2, 3 }</c><br/>
    /// <c>{ 0, 3, 1 }</c><br/>
    /// <c>{ 0, 3, 2 }</c><br/>
    /// <c>{ 1, 0, 2 }</c><br/>
    /// <c>{ 1, 0, 3 }</c><br/>
    /// <c>{ 1, 2, 0 }</c><br/>
    /// <c>{ 1, 2, 3 }</c><br/>
    /// <c>{ 1, 3, 0 }</c><br/>
    /// <c>{ 1, 3, 2 }</c><br/>
    /// <c>{ 2, 0, 1 }</c><br/>
    /// <c>{ 2, 0, 3 }</c><br/>
    /// <c>{ 2, 1, 0 }</c><br/>
    /// <c>{ 2, 1, 3 }</c><br/>
    /// <c>{ 2, 3, 0 }</c><br/>
    /// <c>{ 2, 3, 1 }</c><br/>
    /// <c>{ 3, 0, 1 }</c><br/>
    /// <c>{ 3, 0, 2 }</c><br/>
    /// <c>{ 3, 1, 0 }</c><br/>
    /// <c>{ 3, 1, 2 }</c><br/>
    /// <c>{ 3, 2, 0 }</c><br/>
    /// <c>{ 3, 2, 1 }</c>
    /// </para>
    /// <para>
    /// Iterating thru <c>new Permutation (4).GetRowsOfPlainChanges()</c> produces:
    /// </para>
    /// <para>
    /// <c>{ 0, 1, 2, 3 }</c><br/>
    /// <c>{ 0, 1, 3, 2 }</c><br/>
    /// <c>{ 0, 3, 1, 2 }</c><br/>
    /// <c>{ 3, 0, 1, 2 }</c><br/>
    /// <c>{ 3, 0, 2, 1 }</c><br/>
    /// <c>{ 0, 3, 2, 1 }</c><br/>
    /// <c>{ 0, 2, 3, 1 }</c><br/>
    /// <c>{ 0, 2, 1, 3 }</c><br/>
    /// <c>{ 2, 0, 1, 3 }</c><br/>
    /// <c>{ 2, 0, 3, 1 }</c><br/>
    /// <c>{ 2, 3, 0, 1 }</c><br/>
    /// <c>{ 3, 2, 0, 1 }</c><br/>
    /// <c>{ 3, 2, 1, 0 }</c><br/>
    /// <c>{ 2, 3, 1, 0 }</c><br/>
    /// <c>{ 2, 1, 3, 0 }</c><br/>
    /// <c>{ 2, 1, 0, 3 }</c><br/>
    /// <c>{ 1, 2, 0, 3 }</c><br/>
    /// <c>{ 1, 2, 3, 0 }</c><br/>
    /// <c>{ 1, 3, 2, 0 }</c><br/>
    /// <c>{ 3, 1, 2, 0 }</c><br/>
    /// <c>{ 3, 1, 0, 2 }</c><br/>
    /// <c>{ 1, 3, 0, 2 }</c><br/>
    /// <c>{ 1, 0, 3, 2 }</c><br/>
    /// <c>{ 1, 0, 2, 3 }</c>
    /// </para>
    /// </example>
    public class Permutation :
        IComparable,
        System.Collections.IEnumerable,
        IComparable<Permutation>,
        IEquatable<Permutation>,
        IEnumerable<int>
    {
        private int[] data;     // The arrangement for the current rank. Length is 'k'.
        private int choices;    // Number of possible values 'n'.
        private long rowCount;  // Row count of the table of (k-)permutations.
        private long rank;      // Row index.

        #region Constructors

        /// <summary>
        /// Initializes an empty permutation instance.
        /// </summary>
        public Permutation()
        {
            this.data = new int[0];
            this.choices = 0;
            this.rowCount = 0;
            this.rank = 0;
        }


        /// <summary>
        /// Initializes a new instance that is copied from the supplied permutation.
        /// </summary>
        /// <param name="source">Instance to copy.</param>
        /// <exception cref="ArgumentNullException">When <em>source</em> is <b>null</b>.</exception>
        public Permutation (Permutation source)
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
        /// Initializes a new permutation of <see cref="Rank"/> 0 with the supplied number of elements.
        /// </summary>
        /// <param name="choices">Number of elements in the sequence.</param>
        /// <example>
        /// <code source="..\Examples\Permutation\PnExample01\PnExample01.cs" lang="cs" />
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// When <em>choices</em> is less than 0 or greater than 20.
        /// </exception>
        public Permutation (int choices)
        {
            if (choices < 0)
                throw new ArgumentOutOfRangeException (nameof (choices), "Value is less than zero.");

            if (choices > MaxChoices)
                throw new ArgumentOutOfRangeException (nameof (choices), "Value is greater than maximum allowed.");

            this.data = new int[choices];
            for (int ei = 0; ei < choices; ++ei)
                this[ei] = ei;

            this.choices = choices;
            this.rowCount = choices == 0? 0 : Combinatoric.Factorial (choices);
            this.rank = 0;
        }


        /// <summary>
        /// Initializes a new permutation of <see cref="Rank"/> 0
        /// with the supplied number of <em>picks</em> from the supplied number of <em>choices</em>.
        /// </summary>
        /// <param name="choices">Number of values to choose from.</param>
        /// <param name="picks">Number of elements in the sequence.</param>
        /// <remarks>
        /// Supplying a value for <em>choices</em> that is greater than <em>picks</em>
        /// will instantiate a <em>k</em>-permutation also known as a variation.
        /// </remarks>
        /// <example>
        /// <code source="..\Examples\Permutation\PnExample06\PnExample06.cs" lang="cs" />
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// When <em>picks</em> less than 0 or greater than <em>choices</em>;
        /// when <em>choices</em> greater than 20.
        /// </exception>
        public Permutation (int choices, int picks) : this (choices, picks, 0)
        { }


        /// <summary>
        /// Initializes a new permutation of the supplied <em>rank</em>
        /// with the supplied number of <em>picks</em> from the supplied number of <em>choices</em>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Supplying a value for <em>choices</em> that is greater than <em>picks</em>
        /// will instantiate a <em>k</em>-permutation also known as a variation.
        /// </para>
        /// <para>
        /// If <em>rank</em> is out of the range (0..<see cref="RowCount"/>-1),
        /// it will be normalized to the valid range.
        /// For example, a value of -1 will produce the last row in the ordered table.
        /// </para>
        /// </remarks>
        /// <param name="choices">Number of values to choose from.</param>
        /// <param name="picks">Number of elements in the sequence.</param>
        /// <param name="rank">Row index in the ordered <see cref="Permutation"/> table.</param>
        /// <example>
        /// This is an example of a <em>k</em>-permutation.
        /// <code source="..\Examples\Permutation\PnExample05\PnExample05.cs" lang="cs" />
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// When <em>picks</em> less than 0 or greater than <em>choices</em>;
        /// when <em>choices</em> greater than 20.
        /// </exception>
        public Permutation (int choices, int picks, long rank)
        {
            if (picks < 0)
                throw new ArgumentOutOfRangeException (nameof (picks), "Value is less than zero.");

            if (picks > choices)
                throw new ArgumentOutOfRangeException (nameof (picks), "Value is greater than choices.");

            if (choices > MaxChoices)
                throw new ArgumentOutOfRangeException (nameof (choices), "Value is greater than maximum allowed.");

            this.data = new int[picks];
            this.choices = choices;
            CalcRowCount();
            Rank = rank;
        }


        /// <summary>
        /// Initializes a new permutation from the elements supplied in <em>source</em>.
        /// </summary>
        /// <param name="source">Array of integers.</param>
        /// <example>
        /// <code source="..\Examples\Permutation\PnExample04\PnExample04.cs" lang="cs" />
        /// </example>
        /// <exception cref="ArgumentNullException">When <em>source</em> is <b>null</b>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// When length of <em>source</em> is greater than 20 or contains invalid data;
        /// When <em>source</em> contains out of range values.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// When <em>source</em> contains repeated values.
        /// </exception>
        public Permutation (int[] source)
        {
            if (source == null)
                throw new ArgumentNullException (nameof (source));

            this.choices = source.Length;
            Construct (source);
        }


        /// <summary>
        /// Initializes a new permutation from the elements supplied in <em>source</em>
        /// picked from the supplied number of <em>choices</em>.
        /// </summary>
        /// <remarks>
        /// Supplying a value for <em>choices</em> that is greater than the number of elements in <em>source</em>
        /// will instantiate a <em>k</em>-permutation.
        /// </remarks>
        /// <param name="source">Array of integers with elements in the range (0..<em>choices</em>-1).</param>
        /// <param name="choices">Number of values that <em>source</em> may pick from.</param>
        /// <exception cref="ArgumentNullException">When <em>source</em> is <b>null</b>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// When length of <em>source</em> is greater than 20 or contains invalid data;
        /// When <em>source</em> contains out of range values;
        /// When <em>choices</em> is less than 0 or greater than 20.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// When <em>source</em> contains repeated values.
        /// </exception>
        public Permutation (int[] source, int choices)
        {
            if (source == null)
                throw new ArgumentNullException (nameof (source));

            if (choices < 0 || choices > MaxChoices)
                throw new ArgumentOutOfRangeException (nameof (choices));

            this.choices = choices;
            Construct (source);
        }

        #endregion

        #region Private methods

        // On entry: source may be unvalidated
        // On exit: instance will have rank, rowCount for source data
        private void Construct (int[] source)
        {
            if (source.Length > MaxChoices)
                throw new ArgumentOutOfRangeException (nameof (source), "Too many elements.");

            int isUsed = 0;
            foreach (int element in source)
            {
                if (element < 0 || element >= Choices)
                    throw new ArgumentOutOfRangeException (nameof (source), "Element is out of range.");

                int flag = 1 << element;
                if ((isUsed & flag) != 0)
                    throw new ArgumentException ("Elements must be unique.", nameof (source));
                isUsed |= flag;
            }

            this.data = new int[source.Length];
            source.CopyTo (this.data, 0);

            CalcRowCount();
            CalcRank();
        }


        private void CalcRowCount()
        {
            if (Picks == 0)
                this.rowCount = 0;
            else
            {
                this.rowCount = Combinatoric.Factorial (Choices);
                if (Choices != Picks)
                    this.rowCount /= Combinatoric.Factorial (Choices - Picks);
            }
        }


        private void CalcRank()
        {
            int isUsed = 0;
            int toGo = Choices;
            this.rank = 0;

            foreach (int e1 in this.data)
            {
                isUsed |= 1 << e1;

                int digit = 0;
                for (int e2 = 0; e2 < e1; ++e2)
                    if ((isUsed & 1 << e2) == 0)
                        ++digit;

                this.rank += digit * Combinatoric.Factorial (--toGo);
            }

            if (toGo != 0)
                this.rank /= Combinatoric.Factorial (toGo);
        }


        // On exit: returns swap count, array is garbled
        private static int CalcSwapCount (int[] array, int choices)
        {
            int result = 0;
            int md = 0;
            var val = new int[array.Length];
            var map = new int[choices];

            for (int i1 = 0; i1 < array.Length; ++i1)
                val[i1] = array[i1];

            for (var ei = 0; ei < choices; ++ei)
                map[ei] = -1;

            for (var ei = 0; ei < array.Length; ++ei)
                map[array[ei]] = ei;

            for (var mi = 0; mi < choices; ++mi)
            {
                int ep = map[mi];
                if (ep < 0)
                    --md;
                else if (ep > mi + md)
                {
                    int ei = val[mi + md];
                    map[ei] = ep;
                    val[ep] = ei;
                    ++result;
                }
            }

            return result;
        }


        private static long CalcPlainRank (int[] elements)
        {
            int n = elements.Length;
            long pr = 0;

            for (int ei = 1; ei < n; ++ei)
            {
                int xr = 0;
                for (int i = 0;; ++i)
                    if (elements[i] <= ei)
                    {
                        ++xr;
                        if (elements[i] == ei)
                            break;
                    }

                pr = pr * (ei + 1) + (pr % 2 == 0? ei - xr + 1 : xr - 1);
            }

            return pr;
        }


        // On entry: elements allocated for choices.
        // On exit: result will be in elements.
        private static void CalcPlainUnrank (int[] elements, long plainRank)
        {
            elements[0] = 0;
            for (int ei = 1; ei < elements.Length; ++ei)
            {
                int yd = (int) (Combinatoric.Factorial (elements.Length) / Combinatoric.Factorial (ei+1));
                int yi = (int) ((plainRank / yd) % ((ei + 1) * 2));
                int ip = yi <= ei? ei - yi : yi - ei - 1;

                for (int si = ei; si > ip; --si)
                    elements[si] = elements[si-1];
                elements[ip] = ei;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get an element of the <see cref="Permutation"/> at the supplied column.
        /// </summary>
        /// <param name="index">Index value.</param>
        /// <returns>Sequence value at <em>index</em>.</returns>
        /// <example>
        /// <code source="..\Examples\Permutation\PnExample05\PnExample05.cs" lang="cs" />
        /// </example>
        /// <exception cref="IndexOutOfRangeException">
        /// When <em>index</em> not in range (0..<see cref="Picks"/>-1).
        /// </exception>
        public int this[int index]
        {
            get { return data[index]; }
            private set { data[index] = value; }
        }


        /// <summary
        /// >Number of available choices for the elements of the <see cref="Permutation"/>.
        /// </summary>
        /// <remarks>
        /// If no <em>picks</em> value was specified when constructing this
        /// <see cref="Permutation"/>, then this is also the number of elements.
        /// </remarks>
        public int Choices => choices;


        /// <summary
        /// >Number of elements in the <see cref="Permutation"/>.
        /// </summary>
        /// <remarks>
        /// Also known as <em>k</em>. If value is less than <em>Choices</em>,
        /// then this is a <em>k</em>-permutation.
        /// </remarks>
        public int Picks => data.Length;


        /// <summary>
        /// Row index of the sequence in the plain ordered <see cref="Permutation"/> table.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Plain changes produces a table where adjacent rows differ by only a single swap of
        /// 2 adjacent elements. The table always begins with the same row that begins the
        /// lexicographically ordered table of the same <see cref="Choices"/>.
        /// </para>
        /// <para>
        /// Any assigned value out of range will be normalized to (0..<see cref="RowCount"/>-1).
        /// </para>
        /// </remarks>
        /// <example>
        /// <code source="..\Examples\Permutation\PnExample07\PnExample07.cs" lang="cs" />
        /// </example>
        /// <exception cref="InvalidOperationException">
        /// When <em>Choices</em> not equal to <em>Picks</em>.
        /// </exception>
        /// <seealso cref="GetRowsOfPlainChanges"/>
        public long PlainRank
        {
            get
            {
                if (Picks != Choices)
                    throw new InvalidOperationException ("Choices and Picks must be equal.");

                return CalcPlainRank (data);
            }
            set
            {
                if (Picks != Choices)
                    throw new InvalidOperationException ("Choices and Picks must be equal.");

                if (RowCount == 0)
                    return;

                // Normalize the new rank.
                if (value < 0)
                {
                    value = value % RowCount;
                    if (value < 0)
                        value += RowCount;
                }
                else if (value >= RowCount)
                    value = value % RowCount;

                CalcPlainUnrank (data, value);
                CalcRank();
            }
        }


        /// <summary>
        /// Row index of the sequence in the lexicographically ordered
        /// <see cref="Permutation"/> table.
        /// </summary>
        /// <remarks>
        /// Any assigned value out of range will be normalized to (0..<see cref="RowCount"/>-1).
        /// </remarks>
        /// <example>
        /// <code source="..\Examples\Permutation\PnExample04\PnExample04.cs" lang="cs" />
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
                    value = value % RowCount;
                    if (value < 0)
                        value += RowCount;
                }
                else if (value >= RowCount)
                    value = value % RowCount;

                rank = value;

                //
                // Perform unranking:
                //

                int isUsed = 0;
                var factoradic = new int[Choices];

                // Build the factoradic from the diminishing rank.
                value = value * Combinatoric.Factorial (Choices - Picks);
                for (int fi = Choices - 1; fi >= 0; --fi)
                {
                    long divisor = Combinatoric.Factorial (fi);
                    int quotient = factoradic[fi] = (int) (value / divisor);
                    value -= quotient * divisor;
                }

                // Build the permutation from the diminishing factoradic.
                for (int fi = Choices - 1; fi >= Choices - Picks; --fi)
                    for (int newAtom = 0; ; ++newAtom)
                        if ((isUsed & 1 << newAtom) == 0)
                            if (--factoradic[fi] < 0)
                            {
                                this[Choices - fi - 1] = newAtom;
                                isUsed |= 1 << newAtom;
                                break;
                            }
            }
        }


        /// <summary>
        /// Returns number of distinct possible arrangements of this <see cref="Permutation"/>.
        /// </summary>
        public long RowCount => rowCount;


        /// <summary>
        /// Returns number of element swaps needed to transform this <see cref="Permutation"/>
        /// into <see cref="Rank"/> 0.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If additional swaps are applied resulting again in a row of <see cref="Rank"/> 0,
        /// those additional swaps will always be a multiple of 2.
        /// </para>
        /// <para>
        /// Any <see cref="Permutation"/> with a <see cref="Rank"/> of 0 always has a
        /// <see cref="PlainRank"/> of 0.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code source="..\Examples\Permutation\PnExample07\PnExample07.cs" lang="cs" />
        /// </example>
        public int Swaps
        {
            get
            {
                var elements = new int[Picks];
                this.data.CopyTo (elements, 0);
                return CalcSwapCount (elements, Choices);
            }
        }

        #endregion

        #region Instance methods

        /// <summary>
        /// Advance <see cref="Rank"/> a minimum while changing element at <em>nodeIndex</em>.
        /// </summary>
        /// <returns>Lowest index of actual changed element if successful; else <b>-1</b>.</returns>
        /// <remarks>
        /// This method provides support for backtracking algorithms by pruning permutations that
        /// cannot be completed to a solution.
        /// </remarks>
        /// <param name="nodeIndex">Element to change.</param>
        /// <example>
        /// <code source="..\Examples\Queens\PnBacktrack\PnBacktrack.cs" lang="cs" />
        /// </example>
        /// <exception cref="InvalidOperationException">
        /// When <em>Choices</em> not equal to <em>Picks</em>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// When <em>nodeIndex</em> not in range (0..<see cref="Picks"/>-1).
        /// </exception>
        public int Backtrack (int nodeIndex)
        {
            if (Picks != Choices)
                throw new InvalidOperationException ("Choices and Picks must be equal.");

            if (nodeIndex < 0 || nodeIndex >= Picks)
                throw new ArgumentOutOfRangeException (nameof (nodeIndex), "Value is out of range.");

            Array.Sort (this.data, nodeIndex + 1, Picks - nodeIndex - 1);
            for (int tailIndex = nodeIndex+1; tailIndex < Picks; ++tailIndex)
            {
                int swap = this[tailIndex];
                if (swap > this[nodeIndex])
                {
                    this[tailIndex] = this[nodeIndex];
                    this[nodeIndex] = swap;
                    CalcRank();
                    return nodeIndex;
                }
            }

            for (;;)
            {
                if (--nodeIndex < 0)
                    return nodeIndex;

                int tail = this[nodeIndex+1];
                for (int tailIndex = nodeIndex+2; tailIndex < data.Length; ++tailIndex)
                    if (this[tailIndex] < this[nodeIndex])
                        this[tailIndex-1] = this[tailIndex];
                    else
                    {
                        this[tailIndex-1] = this[nodeIndex];
                        this[nodeIndex] = this[tailIndex];
                        while (++tailIndex < Picks)
                            this[tailIndex-1] = this[tailIndex];
                        this[tailIndex-1] = tail;
                        CalcRank();
                        return nodeIndex;
                    }
                if (tail > this[nodeIndex])
                {
                    this[Picks-1] = this[nodeIndex];
                    this[nodeIndex] = tail;
                    CalcRank();
                    return nodeIndex;
                }
                this[Picks-1] = tail;
            }
        }


        /// <summary>Compare 2 <see cref="Permutation"/>s.</summary>
        /// <param name="obj">Target of the comparison.</param>
        /// <returns>A signed integer indicating the sort order of this instance to <em>obj</em>.</returns>
        public int CompareTo (object obj) => CompareTo (obj as Permutation);


        /// <summary>Compare 2 <see cref="Permutation"/>s.</summary>
        /// <param name="other">Target of the comparison.</param>
        /// <returns>A signed integer indicating the sort order of this instance to <em>other</em>.</returns>
        public int CompareTo (Permutation other)
        {
            if ((object) other == null)
                return 1;

            int result = this.Picks - other.Picks;

            if (result == 0)
                if (this.Rank > other.Rank)
                    result = 1;
                else if (this.Rank < other.Rank)
                    result = -1;

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
        /// Indicate whether 2 <see cref="Permutation"/>s have the same value.
        /// </summary>
        /// <param name="obj">Target of the comparison.</param>
        /// <returns>
        /// <b>true</b> if <em>obj</em> has the same value as this object; otherwise, <b>false</b>.
        /// </returns>
        public override bool Equals (object obj)
            => Equals (obj as Permutation);


        /// <summary>
        /// Indicate whether 2 <see cref="Permutation"/>s have the same value.
        /// </summary>
        /// <param name="other">Target of the comparison.</param>
        /// <returns>
        /// <b>true</b> if <em>other</em> has the same value as this instance;
        /// otherwise, <b>false</b>.
        /// </returns>
        public bool Equals (Permutation other)
            => (object) other != null && other.Rank == Rank && other.Picks == Picks;


        /// <summary>Get an object-based enumerator of the elements.</summary>
        /// <returns>Object-based elemental enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();


        /// <summary>Enumerate all elements of a <see cref="Permutation"/>.</summary>
        /// <returns>An <c>IEnumerator&lt;int&gt;</c> for this <see cref="Permutation"/>.</returns>
        /// <example>
        /// <code source="..\Examples\Permutation\PnExample05\PnExample05.cs" lang="cs" />
        /// </example>
        public IEnumerator<int> GetEnumerator()
        {
            foreach (int element in this.data)
                yield return element;
        }


        /// <summary>Get the hash oode of the <see cref="Permutation"/>.</summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode() => unchecked ((int) Rank);


        /// <summary>
        /// Iterate thru all rows of the <see cref="Permutation"/> table
        /// for every value of <see cref="Rank"/> ascending.
        /// </summary>
        /// <returns>An iterator for a <see cref="Permutation"/> table.</returns>
        /// <remarks>
        /// If the start row is not of <see cref="Rank"/> 0, the iteration will wrap around
        /// so that <see cref="RowCount"/> items are always produced.
        /// </remarks>
        /// <example>
        /// <code source="..\Examples\Permutation\PnExample01\PnExample01.cs" lang="cs" />
        /// </example>
        public IEnumerable<Permutation> GetRows()
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
        /// Iterate thru all rows of all <see cref="Permutation"/> tables for every
        /// <see cref="Choices"/> value in the range (1..<see cref="Choices"/>).
        /// </summary>
        /// <returns>An iterator for a series of <see cref="Permutation"/> tables.</returns>
        /// <example>
        /// <code source="..\Examples\Permutation\PnExample02\PnExample02.cs" lang="cs" />
        /// </example>
        public IEnumerable<Permutation> GetRowsForAllChoices()
        {
            var beginRank = Rank;
            var beginChoices = Choices;
            var beginData = this.data;

            for (int c = 1; c <= beginChoices; ++c)
            {
                this.data = new int[c];
                for (int e = 0; e < c; ++e)
                    this[e] = e;
                this.choices = c;
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


        /// <summary>
        /// Iterate thru all rows of all <see cref="Permutation"/> tables for every
        /// <see cref="Picks"/> value in the range (1..<see cref="Picks"/>).
        /// </summary>
        /// <returns>An iterator for a series of <see cref="Permutation"/> tables.</returns>
        /// <example>
        /// <code source="..\Examples\Permutation\PnExample02\PnExample02.cs" lang="cs" />
        /// </example>
        public IEnumerable<Permutation> GetRowsForAllPicks()
        {
            var beginRank = this.rank;
            var beginData = this.data;

            for (int p = 1; p <= beginData.Length; ++p)
            {
                this.data = new int[p];
                for (int e = 0; e < p; ++e)
                    this[e] = e;
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


        /// <summary>
        /// Iterate thru all rows of the <see cref="Permutation"/> table
        /// while swapping only 2 values in each result.
        /// </summary>
        /// <returns>An iterator for a <see cref="Permutation"/> table.</returns>
        /// <remarks>
        /// <para>
        /// The results of this iterator are commonly known as "plain changes".
        /// </para>
        /// <para>
        /// Usage note:
        /// <ul>
        ///   <li>
        ///     Using this iterator will not perform as fast as using a class that is
        ///     designed and optimized for generating plain changes without the overhead
        ///     of calculating the lexicographical rank for each row.
        ///   </li>
        /// </ul>
        /// </para>
        /// </remarks>
        /// <example>
        /// <code source="..\Examples\Permutation\PnExample07\PnExample07.cs" lang="cs" />
        /// </example>
        /// <exception cref="InvalidOperationException">
        /// When <em>Choices</em> not equal to <em>Picks</em>.
        /// </exception>
        /// <seealso cref="PlainRank"/>
        public IEnumerable<Permutation> GetRowsOfPlainChanges()
        {
            if (Picks != Choices)
                throw new InvalidOperationException ("Choices and Picks must be equal.");

            if (RowCount > 0)
            {
                long plainRank = CalcPlainRank (this.data);
                for (var beginRank = Rank;;)
                {
                    yield return this;

                    plainRank = (plainRank + 1) % RowCount;
                    CalcPlainUnrank (this.data, plainRank);
                    CalcRank();

                    if (Rank == beginRank)
                        yield break;
                }
            }
        }


        /// <summary>
        /// Provide a readable form of the <see cref="Permutation"/> sequence.
        /// </summary>
        /// <returns>A <b>string</b> that represents the sequence.</returns>
        /// <remarks>Result is enclosed in braces and separated by commas.</remarks>
        /// <example>
        /// <code source="..\Examples\Permutation\PnExample04\PnExample04.cs" lang="cs" />
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
        /// Apply a <see cref="Permutation"/> sequence to rearrange the supplied list or array.
        /// </summary>
        /// <typeparam name="T">Type of items to rearrange.</typeparam>
        /// <param name="arrangement">New arrangement for items.</param>
        /// <param name="source">list of items to rearrange.</param>
        /// <returns>Rearranged objects.</returns>
        /// <example>
        /// <code source="..\Examples\Permutation\PnExample03\PnExample03.cs" lang="cs" />
        /// </example>
        /// <exception cref="ArgumentNullException">When <em>arrangement</em> or <em>source</em> is <b>null</b>.</exception>
        /// <exception cref="ArgumentException">When length of
        /// <em>source</em> is less than arrangement.<see cref="Choices"/>.</exception>
        public static List<T> Permute<T> (Permutation arrangement, IList<T> source)
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


        /// <summary>Indicate whether 2 <see cref="Permutation"/>s are equal.</summary>
        /// <param name="param1">A <see cref="Permutation"/>.</param>
        /// <param name="param2">A <see cref="Permutation"/>.</param>
        /// <returns><b>true</b> if supplied <see cref="Permutation"/>s are equal;
        /// otherwise, <b>false</b>.</returns>
        public static bool operator == (Permutation param1, Permutation param2)
            => (object) param1 == null ? (object) param2 == null : param1.Equals (param2);


        /// <summary>Indicate whether 2 <see cref="Permutation"/>s are not equal.</summary>
        /// <param name="param1">A <see cref="Permutation"/>.</param>
        /// <param name="param2">A <see cref="Permutation"/>.</param>
        /// <returns><b>true</b> if supplied sequences are not equal;
        /// otherwise, <b>false</b>.</returns>
        public static bool operator != (Permutation param1, Permutation param2)
            => (object) param1 == null ? (object) param2 != null : !param1.Equals (param2);


        /// <summary>Indicate whether the left <see cref="Permutation"/> is less than
        /// the right <see cref="Permutation"/>.</summary>
        /// <param name="param1">A <see cref="Permutation"/>.</param>
        /// <param name="param2">A <see cref="Permutation"/>.</param>
        /// <returns><b>true</b> if the left sequence is less than
        /// the right sequence otherwise, <b>false</b>.</returns>
        public static bool operator < (Permutation param1, Permutation param2)
            => (object) param1 == null ? (object) param2 != null : param1.CompareTo (param2) < 0;


        /// <summary>Indicate whether the left <see cref="Permutation"/> is greater than
        /// or equal to the right <see cref="Permutation"/>.</summary>
        /// <param name="param1">A <see cref="Permutation"/>.</param>
        /// <param name="param2">A <see cref="Permutation"/>.</param>
        /// <returns><b>true</b> if the left sequence is greater than or equal to
        /// the right sequence otherwise, <b>false</b>.</returns>
        public static bool operator >= (Permutation param1, Permutation param2)
            => (object) param1 == null ? (object) param2 == null : param1.CompareTo (param2) >= 0;


        /// <summary>Indicate whether the left <see cref="Permutation"/> is greater than
        /// the right <see cref="Permutation"/>.</summary>
        /// <param name="param1">A <see cref="Permutation"/>.</param>
        /// <param name="param2">A <see cref="Permutation"/>.</param>
        /// <returns><b>true</b> if the left sequence is greater than
        /// the right sequence otherwise, <b>false</b>.</returns>
        public static bool operator > (Permutation param1, Permutation param2)
            => (object) param1 == null ? false : param1.CompareTo (param2) > 0;


        /// <summary>Indicate whether the left permutation is less than or equal to
        /// the right permutation.</summary>
        /// <param name="param1">A <see cref="Permutation"/>.</param>
        /// <param name="param2">A <see cref="Permutation"/>.</param>
        /// <returns><b>true</b> if the left sequence is less than or equal to
        /// the right sequence otherwise, <b>false</b>.</returns>
        public static bool operator <= (Permutation param1, Permutation param2)
            => (object) param1 == null ? true : param1.CompareTo (param2) <= 0;


        /// <summary>
        /// Returns the maximum number of elements that may be in a <see cref="Permutation"/>.
        /// </summary>
        /// <returns>
        /// The maximum number of elements that may be in any <see cref="Permutation"/>
        /// due to Int64 computational limitations.
        /// </returns>
        static public int MaxChoices => Combinatoric.FactorialLength - 1;

        #endregion
    }
}
