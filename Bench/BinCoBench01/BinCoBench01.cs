//
// Exercise 3 different plans to obtain Int64 binomial coefficients:
//
//            Max.
//          Complete   Time
//   Plan     Row      (ms)      Description
//   ----   --------   ----   ----------------------------------------------------------
//     A       38      2588   Factorial formula with common factors cancelled
//     B       61      4371   Multiplicative formula
//     C       66       418   Prebuilt table with fallback to plan B for incomplete rows
//
// And the winner is ... Plan C!
//

using System;
using System.Diagnostics;

namespace BenchApp
{
    partial class BinCoBench01
    {
        public delegate long BinCoRoutine (int n, int k);

        static void Main()
        {
            var maxFullRowA = GetMaxFullRow (BinomialCoefficientPlanA);
            var maxFullRowB = GetMaxFullRow (BinomialCoefficientPlanB);
            var maxFullRowC = GetMaxFullRow (BinomialCoefficientPlanC);

            IsConsistent (BinomialCoefficientPlanA, BinomialCoefficientPlanC);
            IsConsistent (BinomialCoefficientPlanB, BinomialCoefficientPlanC);

            var timeA = GetExecTime (BinomialCoefficientPlanA);
            var timeB = GetExecTime (BinomialCoefficientPlanB);
            var timeC = GetExecTime (BinomialCoefficientPlanC);

            Console.WriteLine ("Plan A: maxFullRow={0}, time={1}ms", maxFullRowA, timeA);
            Console.WriteLine ("Plan B: maxFullRow={0}, time={1}ms", maxFullRowB, timeB);
            Console.WriteLine ("Plan C: maxFullRow={0}, time={1}ms", maxFullRowC, timeC);
        }


        // Test for first failure of the supplied routine.
        static public int GetMaxFullRow (BinCoRoutine routine)
        {
            for (int n = 0; n < 99; ++n)
                for (int k = 0; k <= n; ++k)
                    try
                    {
                        long bc = routine (n, k);
                    }
                    catch (OverflowException)
                    {
                        return n - 1;
                    }

            return -1;
        }


        // Return number of milliseconds to perform repetitive calculations.
        static public long GetExecTime (BinCoRoutine routine)
        {
            var reps = 10000;
            var nMax = 38;
            var watch = new Stopwatch();

            watch.Start();

            for (int r = 0; r < reps; ++r)
                for (int n = 0; n < nMax; ++n)
                    for (int k = 0; k < n; ++k)
                    {
                        long result = routine (n, k);
                    }

            watch.Stop();
            return watch.ElapsedMilliseconds;
        }


        // Return true if two routines return the same values (when no overflow).
        static public bool IsConsistent (BinCoRoutine routine1, BinCoRoutine routine2)
        {
            long result1, result2;

            for (int n = 0; n < 40; ++n)
                for (int k = 0; k <= n; ++k)
                {
                    try
                    {
                        result1 = routine1 (n, k);
                        result2 = routine2 (n, k);
                    }
                    catch (OverflowException)
                    {
                        continue;
                    }

                    if (result1 != result2)
                    {
                        Console.WriteLine ("Conflicting values at n={0}, k={1}", n, k);
                        return false;
                    }
                }

            return true;
        }
    }
}
