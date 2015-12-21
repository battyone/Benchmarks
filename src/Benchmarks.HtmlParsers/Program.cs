using BenchmarkDotNet;
using Benchmarks.HtmlParsers.Benchmarks;

namespace Benchmarks.HtmlParsers
{
    class Program
    {
        static void Main(string[] args)
        {
            new BenchmarkRunner().Run<TableBenchmark>();
        }
    }
}
