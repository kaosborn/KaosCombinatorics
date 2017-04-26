//
// Project: KaosCombinatorics
// File: Permutation.cs
//
// Copyright © 2009-2017 Kasey Osborn (github.com/kaosborn)
// MIT License - Use and redistribute freely
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Kaos.Combinatorics
{
    /// <summary>
    /// Represents an arrangement of distinct values taken from a supplied number of choices.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Unlike combinations, the arrangement of the elements of a permutation is significant.
    /// 
    /// Permutations typically contain all of the available choices. In contrast,
    /// <em>k</em>-permutations contain arrangements that pick fewer elements than the
    /// available choices. 
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
        /// Make an empty <see cref="Permutation"/>.
        /// </summary>
        public Permutation()
        {
            this.data = new int[0];
            this.choices = 0;
            this.rowCount = 0;
            this.rank = 0;
        }


        /// <summary>
        /// Make a copy of a <see cref="Permutation"/>.
        /// </summary>
        /// <param name="source">Instance to copy.</param>
        /// <exception cref="ArgumentNullException">When <em>source</em> is <b>null</b>.</exception>
        public Permutation (Permutation source)
        {
            if (source == null)
                throw new ArgumentNullException ("source");

            this.data = new int[source.data.Length];
            source.data.CopyTo (this.data, 0);

            this.choices = source.choices;
            this.rowCount = source.rowCount;
            this.rank = source.rank;
        }


        /// <summary>
        /// Make a new <see cref="Permutation"/> of all the supplied number of
        /// <em>choices</em> with a <see cref="Rank"/> of 0.
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
                throw new ArgumentOutOfRangeException ("choices", "Value is less than zero.");

            if (choices > MaxChoices)
                throw new ArgumentOutOfRangeException ("choices", "Value is greater than maximum allowed.");

            this.data = new int[choices];
            for (int ei = 0; ei < choices; ++ei)
                this.data[ei] = ei;

            this.choices = choices;
            this.rowCount = choices == 0? 0 : Combinatoric.Factorial (choices);
            this.rank = 0;
        }


        /// <summary>
        /// Make a new <see cref="Permutation"/> with <em>picks</em> number of elements taken
        /// from a possible number of <em>choices</em> of <see cref="Rank"/> 0.
        /// </summary>
        /// <param name="choices">Number of values to choose from.</param>
        /// <param name="picks">Number of elements in the sequence.</param>
        /// <example>
        /// <code source="..\Examples\Permutation\PnExample06\PnExample06.cs" lang="cs" />
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// When <em>picks</em> less than 0, greater than 20, or greater than <em>choices</em>;
        /// when <em>choices</em> greater than 20.
        /// </exception>
        public Permutation (int choices, int picks) : this (choices, picks, 0)
        { }


        /// <summary>
        /// Make a new <see cref="Permutation"/> with <em>picks</em> number of elements taken
        /// from a possible number of <em>choices</em> of the supplied <em>rank</em>.
        /// </summary>
        /// <remarks>
        /// If the supplied <em>rank</em> is out of the range (0..<see cref="RowCount"/>-1),
        /// it will be normalized to the valid range. For example, a value of -1 will
        /// produce the last row in the ordered table.
        /// </remarks>
        /// <param name="choices">Number of values to choose from.</param>
        /// <param name="picks">Number of elements in the sequence.</param>
        /// <param name="rank">Initial row index in the lexicographically ordered <see cref="Permutation"/> table.</param>
        /// <example>
        /// <code source="..\Examples\Permutation\PnExample05\PnExample05.cs" lang="cs" />
        /// </example>
        /// <exception cref="ArgumentOutOfRangeException">
        /// When <em>picks</em> less than 0 or greater than <em>choices</em>;
        /// when <em>choices</em> greater than 20.
        /// </exception>
        public Permutation (int choices, int picks, long rank)
        {
            if (picks < 0)
                throw new ArgumentOutOfRangeException ("picks", "Value is less than zero.");

            if (picks > choices)
                throw new ArgumentOutOfRangeException ("picks", "Value is greater than choices.");

            if (choices > MaxChoices)
                throw new ArgumentOutOfRangeException ("choices", "Value is greater than maximum allowed.");

            this.data = new int[picks];
            this.choices = choices;
            this.rowCount = CalcCount (choices, picks);
            Rank = rank;
        }


        /// <summary>
        /// Make a new <see cref="Permutation"/> from the supplied elements.
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
                throw new ArgumentNullException ("source");

            this.choices = source.Length;
            Construct (this, source);
        }


        /// <summary>
        /// Make a new <see cref="Permutation"/> from the supplied elements taken from the
        /// available number of <em>choices</em>.
        /// </summary>
        /// <remarks>
        /// Supplying a value for <em>choices</em> that is greater than the number of
        /// elements in <em>source</em> will create a <em>k</em>-permutation.
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
                throw new ArgumentNullException ("source");

            this.choices = choices;
            Construct (this, source);
        }

#endregion

#region Private static methods

        // On entry: source may be unvalidated
        // On exit: pn will have correct rank, rowcount for source data
        static private void Construct (Permutation pn, int[] source)
        {
            if (source.Length > MaxChoices)
                throw new ArgumentOutOfRangeException ("source", "Too many values.");

            if (pn.choices < 0 || pn.choices > MaxChoices)
                throw new ArgumentOutOfRangeException ("choices");

            pn.data = new int[source.Length];
            source.CopyTo (pn.data, 0);

            bool[] isUsed = new bool[pn.Choices];
            for (int ei = 0; ei < pn.Picks; ++ei)
            {
                if (pn.data[ei] < 0 || pn.data[ei] >= pn.Choices)
                    throw new ArgumentOutOfRangeException ("source", "Value is out of range.");

                if (isUsed[pn.data[ei]])
                    throw new ArgumentException ("Value is repeated.", "source");
                isUsed[pn.data[ei]] = true;
            }

            pn.rowCount = CalcCount (pn.Choices, pn.Picks);
            pn.rank = CalcRank (pn.data, pn.Choices);
        }


        // On entry: choices, picks assumed valid.
        private static long CalcCount (int choices, int picks)
        {
            if (picks == 0)
                return 0;

            long result = Combinatoric.Factorial (choices);
            if (picks < choices)
                result = result / Combinatoric.Factorial (choices - picks);

            return result;
        }


        // On entry: elements & choices assumed valid, picks = elements.Length.
        private static long CalcRank (int[] elements, int choices)
        {
            long result = 0;
            var isUsed = new bool[choices];

            //
            // Perform ranking:
            //

            for (int ei1 = 0; ei1 < elements.Length; ++ei1)
            {
                isUsed[elements[ei1]] = true;

                int digit = 0;
                for (int ei2 = 0; ei2 < elements[ei1]; ++ei2)
                    if (! isUsed[ei2])
                        ++digit;

                result += digit * Combinatoric.Factorial (choices - ei1 - 1);
            }

            if (elements.Length < choices)
                result = result / Combinatoric.Factorial (choices - elements.Length);

            return result;
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

            if (n <= 1)
                return 0;

            for (int ei = 1; ei < n; ++ei)
            {
                int xr = 0;
                for (int i = 0; i < n; ++i)
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

        /// <summary
        /// >Number of available choices for the elements of the <see cref="Permutation"/>.
        /// </summary>
        /// <remarks>
        /// If no <em>picks</em> value was specified when constructing this
        /// <see cref="Permutation"/>, then this is also the number of elements.
        /// </remarks>
        public int Choices
        {
            get { return choices; }
        }


        /// <summary
        /// >Number of elements in the <see cref="Permutation"/>.
        /// </summary>
        /// <remarks>
        /// Also known as <em>k</em>. If value is less than <em>Choices</em>,
        /// then this is a <em>k</em>-permutation.
        /// </remarks>
        public int Picks
        {
            get { return data.Length; }
        }


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
                rank = CalcRank (data, Choices);
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

                var isUsed = new bool[Choices];
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
                        if (! isUsed[newAtom])
                            if (factoradic[fi] > 0)
                                --factoradic[fi];
                            else
                            {
                                data[Choices - fi - 1] = newAtom;
                                isUsed[newAtom] = true;
                                break;
                            }
            }
        }


        /// <summary>
        /// Returns number of distinct possible arrangements of this <see cref="Permutation"/>.
        /// </summary>
        public long RowCount
        {
            get { return rowCount; }
        }


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
                for (int ei = 0; ei < Picks; ++ei)
                    elements[ei] = this[ei];

                return CalcSwapCount (elements, Choices);
            }
        }


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

            if (nodeIndex < 0 || nodeIndex >= this.data.Length)
                throw new ArgumentOutOfRangeException ("nodeIndex", "Value is out of range.");

            Array.Sort (this.data, nodeIndex + 1, this.data.Length - nodeIndex - 1);
            for (int tailIndex = nodeIndex+1; tailIndex < this.data.Length; ++tailIndex)
            {
                int swap = this.data[tailIndex];
                if (swap > this.data[nodeIndex])
                {
                    this.data[tailIndex] = this.data[nodeIndex];
                    this.data[nodeIndex] = swap;
                    this.rank = CalcRank (this.data, this.choices);
                    return nodeIndex;
                }
            }

            for (;;)
            {
                if (--nodeIndex < 0)
                    return nodeIndex;

                int tail = this.data[nodeIndex+1];
                for (int tailIndex = nodeIndex+2; tailIndex < this.data.Length; ++tailIndex)
                    if (this.data[tailIndex] < this.data[nodeIndex])
                        this.data[tailIndex-1] = this.data[tailIndex];
                    else
                    {
                        this.data[tailIndex-1] = this.data[nodeIndex];
                        this.data[nodeIndex] = this.data[tailIndex];
                        while (++tailIndex < this.data.Length)
                            this.data[tailIndex-1] = this.data[tailIndex];
                        this.data[tailIndex-1] = tail;
                        this.rank = CalcRank (this.data, this.choices);
                        return nodeIndex;
                    }
                if (tail > this.data[nodeIndex])
                {
                    this.data[this.data.Length-1] = this.data[nodeIndex];
                    this.data[nodeIndex] = tail;
                    this.rank = CalcRank (this.data, this.choices);
                    return nodeIndex;
                }
                this.data[this.data.Length-1] = tail;
            }
        }


        /// <summary>Compare 2 <see cref="Permutation"/>s.</summary>
        /// <param name="obj">Target of the comparison.</param>
        /// <returns>A signed integer indicating the sort order of this instance to <em>obj</em>.</returns>
        public int CompareTo (object obj)
        { return CompareTo (obj as Permutation); }


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
                throw new ArgumentNullException ("array");

            if (array.Length < this.data.Length)
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
        { return Equals (obj as Permutation); }


        /// <summary>
        /// Indicate whether 2 <see cref="Permutation"/>s have the same value.
        /// </summary>
        /// <param name="other">Target of the comparison.</param>
        /// <returns>
        /// <b>true</b> if <em>other</em> has the same value as this instance;
        /// otherwise, <b>false</b>.
        /// </returns>
        public bool Equals (Permutation other)
        {
            return (object) other != null && other.Rank == Rank && other.Picks == Picks;
        }


        /// <summary>Get an object-based enumerator of the elements.</summary>
        /// <returns>Object-based elemental enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        { return GetEnumerator(); }


        /// <summary>Enumerate all elements of a <see cref="Permutation"/>.</summary>
        /// <returns>
        /// An <c>IEnumerator&lt;int&gt;</c> for this <see cref="Permutation"/>.
        /// </returns>
        /// <example>
        /// <code source="..\Examples\Permutation\PnExample05\PnExample05.cs" lang="cs" />
        /// </example>
        public IEnumerator<int> GetEnumerator()
        {
            for (int ei = 0; ei < Picks; ++ei)
                yield return this.data[ei];
        }


        /// <summary>Get the hash oode of the <see cref="Permutation"/>.</summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        { return unchecked ((int) Rank); }


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
            if (RowCount > 0)
            {
                long startRank = Rank;
                for (Permutation current = (Permutation) MemberwiseClone();;)
                {
                    yield return current;
                    current.Rank = current.Rank + 1;
                    if (current.Rank == startRank)
                        break;
                }
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
            for (int w = 1; w <= Choices; ++w)
            {
                Permutation current = (Permutation) MemberwiseClone();
                current.data = new int[w];
                for (int ei = 0; ei < current.data.Length; ++ei)
                    current.data[ei] = ei;
                current.choices = w;
                current.rowCount = CalcCount (w, w);
                current.rank = 0;

                for (;;)
                {
                    yield return current;
                    current.Rank = current.Rank + 1;
                    if (current.Rank == 0)
                        break;
                }
            }
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
            for (int w = 1; w <= Picks; ++w)
            {
                Permutation current = (Permutation) MemberwiseClone();
                current.data = new int[w];
                for (int ei = 0; ei < current.data.Length; ++ei)
                    current.data[ei] = ei;
                current.rowCount = CalcCount (current.Choices, w);
                current.rank = 0;

                for (;;)
                {
                    yield return current;
                    current.Rank = current.Rank + 1;
                    if (current.Rank == 0)
                        break;
                }
            }
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
                Permutation current = (Permutation) MemberwiseClone();

                long startRank = Rank;
                for (long plainRank = CalcPlainRank (data);;)
                {
                    yield return current;

                    plainRank = (plainRank + 1) % RowCount;

                    CalcPlainUnrank (current.data, plainRank);
                    current.rank = CalcRank (data, current.Choices);

                    if (current.Rank == startRank)
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
                result.Append (this.data[ei]);

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
        /// <exception cref="ArgumentOutOfRangeException">When length of
        /// <em>source</em> is less than <see cref="Picks"/>.</exception>
        public static List<T> Permute<T> (Permutation arrangement, IList<T> source)
        {
            if (arrangement == null)
                throw new ArgumentNullException ("arrangement");

            if (source == null)
                throw new ArgumentNullException ("source");

            if (source.Count < arrangement.Picks)
                throw new ArgumentException ("Not enough supplied values.", "source");

            var result = new List<T> (arrangement.Picks);

            for (int ai = 0; ai < arrangement.Picks; ++ai)
                result.Add (source[arrangement[ai]]);

            return result;
        }


        /// <summary>Indicate whether 2 <see cref="Permutation"/>s are equal.</summary>
        /// <param name="param1">A <see cref="Permutation"/>.</param>
        /// <param name="param2">A <see cref="Permutation"/>.</param>
        /// <returns><b>true</b> if supplied <see cref="Permutation"/>s are equal;
        /// otherwise, <b>false</b>.</returns>
        public static bool operator == (Permutation param1, Permutation param2)
        {
            if ((object) param1 == null)
                return (object) param2 == null;
            else
                return param1.Equals (param2);
        }


        /// <summary>Indicate whether 2 <see cref="Permutation"/>s are not equal.</summary>
        /// <param name="param1">A <see cref="Permutation"/>.</param>
        /// <param name="param2">A <see cref="Permutation"/>.</param>
        /// <returns><b>true</b> if supplied sequences are not equal;
        /// otherwise, <b>false</b>.</returns>
        public static bool operator != (Permutation param1, Permutation param2)
        { return ! (param1 == param2); }


        /// <summary>Indicate whether the left <see cref="Permutation"/> is less than
        /// the right <see cref="Permutation"/>.</summary>
        /// <param name="param1">A <see cref="Permutation"/>.</param>
        /// <param name="param2">A <see cref="Permutation"/>.</param>
        /// <returns><b>true</b> if the left sequence is less than
        /// the right sequence otherwise, <b>false</b>.</returns>
        public static bool operator < (Permutation param1, Permutation param2)
        {
            if ((object) param1 == null)
                return (object) param2 != null;
            else
                return param1.CompareTo (param2) < 0;
        }


        /// <summary>Indicate whether the left <see cref="Permutation"/> is greater than
        /// or equal to the right <see cref="Permutation"/>.</summary>
        /// <param name="param1">A <see cref="Permutation"/>.</param>
        /// <param name="param2">A <see cref="Permutation"/>.</param>
        /// <returns><b>true</b> if the left sequence is greater than or equal to
        /// the right sequence otherwise, <b>false</b>.</returns>
        public static bool operator >= (Permutation param1, Permutation param2)
        { return ! (param1 < param2); }


        /// <summary>Indicate whether the left <see cref="Permutation"/> is greater than
        /// the right <see cref="Permutation"/>.</summary>
        /// <param name="param1">A <see cref="Permutation"/>.</param>
        /// <param name="param2">A <see cref="Permutation"/>.</param>
        /// <returns><b>true</b> if the left sequence is greater than
        /// the right sequence otherwise, <b>false</b>.</returns>
        public static bool operator > (Permutation param1, Permutation param2)
        {
            if ((object) param1 == null)
                return false;
            else
                return param1.CompareTo (param2) > 0;
        }

        /// <summary>Indicate whether the left permutation is less than or equal to
        /// the right permutation.</summary>
        /// <param name="param1">A <see cref="Permutation"/>.</param>
        /// <param name="param2">A <see cref="Permutation"/>.</param>
        /// <returns><b>true</b> if the left sequence is less than or equal to
        /// the right sequence otherwise, <b>false</b>.</returns>
        public static bool operator <= (Permutation param1, Permutation param2)
        { return ! (param1 > param2); }


        /// <summary>
        /// Returns the maximum number of elements that may be in a <see cref="Permutation"/>.
        /// </summary>
        /// <returns>
        /// The maximum number of elements that may be in any <see cref="Permutation"/>
        /// due to Int64 computational limitations.
        /// </returns>
        static public int MaxChoices
        {
            get { return Combinatoric.FactorialLength - 1; }
        }

#endregion
    }
}
