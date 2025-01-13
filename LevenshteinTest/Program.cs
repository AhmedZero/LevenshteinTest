using BenchmarkDotNet.Running;

namespace LevenshteinTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner
              .Run<Levenshtein>();
        }
    }
}
