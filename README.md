## KaosCombinatorics

### Overview

KaosCombinatorics is a .NET library that contains four combinatoric generating classes and one supporting class.
These generated combinations of numbers may be used to permute (rearrange) other lists of objects.

The primary classes are:

* `Combination`: An ascending sequence of non-repeating picks from a supplied number of choices.
* `Multicombination`: An ascending sequence of repeating picks from a supplied number of choices.
* `Permutation`: An arrangement of all or a subset of values from a supplied range.
* `Product`: A join of values from a supplied array of ranges.

Two key features this library provides are **unranking** and **ranking** for every combinatoric.
Unranking is the ability to quickly retrieve any row in the combinatoric's lexicographically ordered table by setting its Rank property,
or by supplying the rank to a constructor.
Ranking is where an array of integers is supplied to a constructor.
The Rank property will then contain its position in the ordered table.

### Documentation

For complete project documentation:

https://kaosborn.github.io/help/KaosCombinatorics/index.html

An offline version of this documentation is also provided as a `.chm` file:

https://github.com/kaosborn/KaosCombinatorics/releases

### Project status

This project is stable and code complete.

### Project layout

* The `Bench` folder contains console programs that exercise classes in this library.

* The `Examples` folder contains console programs that provide examples for documentation.

* The `Combinatorics` folder contains all source code and the build.
Building the Release configuration of the project contained in this folder
will produce a `.nuget` file and documentation for distribution.
This library is Multi-targetted to .NET Standard 1.0, .NET 4.0 and .NET 3.5.

* The `Help` folder contains a [Sandcastle Help File Builder](https://github.com/EWSoftware/SHFB)
project that produces documentation from embedded XML comments.
Output is a Microsoft Help v1 file with a `.chm` extension and a static web site.

* The `Test` folder contains unit tests and some short running stress tests.
Code coverage is 100%.
