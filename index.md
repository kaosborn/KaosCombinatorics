## Welcome to KaosCombinatorics

The Kaos.Combinatorics library consists of four classes that provide four different ways of generating ordered lists of combinations of numbers. These combinations may be used to permute (rearrange) other lists of objects. Combinatorics are useful for software testing by allowing the generation of various types of possible combinations of input. Other applications include mathematical problems, games of chance, and cryptography.

The classes provided are:

* **Combination**: An ascending sequence of non-repeating picks from a supplied number of choices.
* **Multicombination**: An ascending sequence of repeating picks from a supplied number of choices.
* **Permutation**: An arrangement of all or a subset of values from a supplied range.
* **Product**: A join of values from a supplied array of ranges.

Two key features this library provides are unranking and ranking for every combinatoric. Unranking is the ability to quickly retrieve any row in the combinatoric's lexicographically ordered table by setting its **Rank** property, or by supplying the rank to a constructor. Ranking is where an array of integers is supplied to a constructor. The Rank property will then contain its place in the ordered table. All combinatorics include a **GetRows()** method that generates all its rows ordered by Rank.

Two key features this library provides are unranking and ranking for every combinatoric. Unranking is the ability to quickly retrieve any row in the combinatoric's lexicographically ordered table by setting its Rank property, or by supplying the rank to a constructor. Ranking is where an array of integers is supplied to a constructor. The Rank property will then contain its place in the ordered table. All combinatorics include a GetRows() method that generates all its rows ordered by Rank.

In addition to lexicographical ordering, the **Permutation** class includes an ordering where adjacent rows differ by only a single swap of adjacent elements. Support for this ordering is provided with the **PlainRank** property and the **GetRowsOfPlainChanges()** iterator.

The default appearance of a combinatoric row is a list of integers (starting at zero) enclosed in braces. This appearance may be tailored three ways:

* Map each element to a different value using the combinatoric's default enumerator or its indexer.
* Use the Permute method and output the rearranged values.
* Subclass of the combinatoric and override ToString().

Below are complete example programs. Most of the techniques shown may be applied to any combinatoric in the library.

Here is an example of the **Multicombination** class:
```cs
using System;
using Kaos.Combinatorics;

namespace ExampleApp
{
    static class McExample01
    {
        static void Main()
        {
            var mc = new Multicombination (choices:4, picks:3);

            Console.WriteLine ("n={0}, k={1}:\n", mc.Choices, mc.Picks);

            foreach (var row in mc.GetRows())
                Console.WriteLine ("{0,2}:  {1}", row.Rank, row);
        }

        /* Output:

        n=4, k=3:

         0:  { 0, 0, 0 }
         1:  { 0, 0, 1 }
         2:  { 0, 0, 2 }
         3:  { 0, 0, 3 }
         4:  { 0, 1, 1 }
         5:  { 0, 1, 2 }
         6:  { 0, 1, 3 }
         7:  { 0, 2, 2 }
         8:  { 0, 2, 3 }
         9:  { 0, 3, 3 }
        10:  { 1, 1, 1 }
        11:  { 1, 1, 2 }
        12:  { 1, 1, 3 }
        13:  { 1, 2, 2 }
        14:  { 1, 2, 3 }
        15:  { 1, 3, 3 }
        16:  { 2, 2, 2 }
        17:  { 2, 2, 3 }
        18:  { 2, 3, 3 }
        19:  { 3, 3, 3 }

        */
    }
}
```
