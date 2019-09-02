![logo](Images/KaosCombinatorics-248.png)
# KaosCombinatorics

KaosCombinatorics is a .NET library that provides classes
for generating combinations, k-combinations, multicombinations, k-multicombinations, permutations, k-permutations, and products
that are ordered and ranked.
These sequences of integers may be used to permute (rearrange) other lists of objects.
Features ranking, unranking, backtracking, plain changes, and more.

Primary types provided are:

* `Combination` - an ascending sequence of distinct picks from a supplied range.
* `Multicombination` - an ascending sequence of repeating picks from a supplied range.
* `Permutation` - a specific arrangement of distinct picks from a supplied range.
* `Product` - a join of values from a supplied array of ranges.

Two key features this library provides are **unranking** and **ranking** for every combinatoric.
Unranking is the ability to quickly retrieve any row in the combinatoric's lexicographically ordered table by setting its Rank property,
or by supplying the rank to a constructor.
Ranking is where the elements of a combinatoric are supplied as an array of integers to a constructor.
The Rank property will then contain its position in the ordered table.

This library is built as a .NET Standard project multitargeted to:

* .NET Standard 1.0.
* .NET Framework 4.0.
* .NET Framework 3.5.

### Roadmap

The next version of this product will be available via GitHub Package Registry.
The current build is a release candidate for vNext in 2019.

### Library installation

To install v5 using Package Manager:

* **`Install-Package Kaos.Combinatorics -Version 5.0.0`**

To install v5 using the .NET CLI:

* **`dotnet add package Kaos.Combinatorics --version 5.0.0`**

To install v5 using the Visual Studio gallery:

1. Click **Manage NuGet Packages**.
2. Select package source of **nuget.org**.
3. Click **Browse** and input **Kaos.Combinatorics**.
4. The package should appear. Click **Install**.
As a multitargeted package, the appropriate binary will be installed for your program.

To install using a direct reference to a `.dll` binary:

1. Download the `.nuget` package from either:

   * https://www.nuget.org/packages/Kaos.Combinatorics/
   * https://github.com/kaosborn/KaosCombinatorics/releases/

2. As archives, individual binaries may be extracted from the `.nuget` package for specific platforms.
A project may then reference the extracted platform-specific `.dll` directly.

### Documentation

Installing the NuGet package will provide IntelliSense and object browser documentation as a `.xml` file.
Below are other sources of help.

#### https://kaosborn.github.io/help/KaosCombinatorics/

Complete documentation is hosted at GitHub.

#### https://github.com/kaosborn/KaosCombinatorics/releases/

Offline documentation is provided for each release.
This downloaded `.chm` file may need to be unblocked by using its file properties dialog.

#### https://github.com/kaosborn/KaosCombinatorics/wiki/

Examples are also shown in the site wiki.

### Repository top-level folders

This repository is a single Visual Studio solution with additional files in the root.

* `Bench` - Console programs that exercise this library.

* `Combinatorics` - The NuGet package.
Building the Release configuration of the project contained in this folder
will produce a `.nuget` file and documentation for distribution.
This library is multitargeted to .NET Standard 1.0, .NET 4.0 and .NET 3.5.

* `Examples` - Console programs for documentation embedding.

* `Help` - [Sandcastle Help File Builder](https://github.com/EWSoftware/SHFB)
project that produces documentation from embedded XML comments.
Output is a Microsoft Help v1 file with a `.chm` extension and (optionally) a static web site.

* `Images` - SVG files with renderings.

* `Install` - Builds a GAC installer.

* `Source` - All source code in a shared project.

* `Test450` - MSTest unit tests and limited stress tests.
Line and branch coverage is 100%.

### Build requirements

#### https://github.com/kaosborn/KaosCombinatorics/

Complete source is hosted at GitHub.
The following linked downloads are free.

#### https://www.visualstudio.com/downloads/

Building the solution requires Visual Studio 2017 Community Edition or greater.

#### https://marketplace.visualstudio.com/items?itemName=visualstudioclient.MicrosoftVisualStudio2017InstallerProjects

Building the `.msi` GAC installer requires the Microsoft Visual Studio Installer Projects extension.

#### https://github.com/EWSoftware/SHFB/releases/

Building `.chm` or web documentation requires Sandcastle Help File Builder.
