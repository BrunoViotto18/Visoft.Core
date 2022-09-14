using BenchmarkDotNet.Running;

using static System.Console;

namespace Visoft.Core.Runner;

using Visoft.Core.Benchmarks;

internal static class Program
{
    static void Main()
    {
        int[] arrayA = new int[10];
        int[] arrayB = new int[5];

        Array.Copy(arrayA, 0, arrayB, 0, 0);

        // ArgumentException
        // ArgumentOutOfRangeException
    }

    public static int Test(params int[] indexes)
    {
        return (int)indexes
            .Reverse()
            .Select((val, index) => Math.Pow(10, index) * val)
            .Sum();
    }
}
