using BenchmarkDotNet.Running;

using static System.Console;

namespace Visoft.Core.Runner;

using Visoft.Core.Benchmarks;

internal static class Program
{
    static void Main()
    {
        WriteLine(Test(4, 5));
    }

    public static int Test(params int[] indexes)
    {
        return (int)indexes
            .Reverse()
            .Select((val, index) => Math.Pow(10, index) * val)
            .Sum();
    }
}
