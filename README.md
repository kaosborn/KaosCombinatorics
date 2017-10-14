![logo](Images/KaosCombinatorics-248.png)
# KaosCombinatorics

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
Ranking is where the elements of a combinatoric are supplied as an array of integers to a constructor.
The Rank property will then contain its position in the ordered table.

This library is built as a .NET Standard project multitargeted to:

* .NET Standard 1.0.
* .NET Framework 4.0.
* .NET Framework 3.5.

### Installation

To install using Package Manager:

`Install-Package Kaos.Combinatorics -Version 5.0.0`

To install using .NET CLI:

`dotnet add package Kaos.Combinatorics --version 5.0.0`

To install using the Visual Studio gallery:

1. Click **Manage NuGet Packages**.
2. Select package source of **nuget.org**.
3. Click **Browse** and input **Kaos.Combinatorics**.
4. The package should appear. Click **Install**.
As a multitargeted package, the appropriate binary will be selected for your program.

To install using a direct reference to a `.dll` binary:

1. Download the `.nuget` package from either:

https://www.nuget.org/packages/Kaos.Combinatorics/
-or-
https://github.com/kaosborn/KaosCombinatorics/releases/

2. As archives, individual binaries may be extracted from the `.nuget` package for specific platforms.
A project may then reference the extracted platform-specific `.dll` directly.

### Documentation

Installing as a NuGet package will provide IntelliSense and object browser documentation as a `.xml` file.
For complete documentation, see:

https://kaosborn.github.io/help/KaosCombinatorics/

An offline version of this documentation is also provided as a `.chm` file:

https://github.com/kaosborn/KaosCombinatorics/releases/

Examples may also be viewed here:

https://github.com/kaosborn/KaosCombinatorics/wiki/

### Status

This project is stable and code complete.

### Build environment

Complete source code with embedded XML documentation is hosted at GitHub.com.
Building the solution requires Visual Studio 2017 Community Edition or greater.
Building documentation requires Sandcastle Help File Builder.

### Repository layout

This repository is a single Visual Studio solution with additional files in the root.

* The `Bench` folder contains console programs that exercise classes in this library.

* The `Examples` folder contains console programs that provide examples for documentation.

* The `Combinatorics` folder contains all source code and the build.
Building the Release configuration of the project contained in this folder
will produce a `.nuget` file and documentation for distribution.
This library is multitargeted to .NET Standard 1.0, .NET 4.0 and .NET 3.5.

* The `Help` folder contains a [Sandcastle Help File Builder](https://github.com/EWSoftware/SHFB)
project that produces documentation from embedded XML comments.
Output is a Microsoft Help v1 file with a `.chm` extension and a static web site.

* The `Images` folder contains the logo `.svg` file and its `.png` conversions (by Edge).

* The `Test` folder contains unit tests and some short running stress tests.
Test engine is MSTest. Code coverage is 100%.