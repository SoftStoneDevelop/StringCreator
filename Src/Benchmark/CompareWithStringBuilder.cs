using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Text;

namespace Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net70)]
    [HideColumns("Error", "StdDev", "Median", "RatioSD", "Gen0", "Gen1", "Gen2")]
    public partial class CompareWithStringBuilder
    {
        [Params(5, 7, 10, 50, 100, 1000, 2500, 5_000, 10_000, 100_000, 500_000, 1_071_741)]
        public int StrLength;

        [Benchmark(Baseline = true, Description = "StringBuilder")]
        public void StringBuilder()
        {
            var stringBuilder = new StringBuilder(StrLength);
            for (int i = 0; i < StrLength; i++)
            {
                stringBuilder.Append($"{i % 10}");
            }

            var result = stringBuilder.ToString();
        }

        [Benchmark(Description = "Class.StringCreator")]
        public void ClassStringCreator()
        {
            var stringCreator = new StringCreator.Class.StringCreator(StrLength);
            for (int i = 0; i < StrLength; i++)
            {
                stringCreator.Append($"{i % 10}");
            }

            var result = stringCreator.ToString();
        }
    }
}