using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Runtime.CompilerServices;
using System.Text;

namespace Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net10_0)]
    [SimpleJob(RuntimeMoniker.NativeAot10_0)]
    [HideColumns("Error", "StdDev", "Median", "RatioSD", "Gen0", "Gen1", "Gen2")]
    public partial class CompareWithStringBuilder
    {
        [Params(5, 25, 100, 1000)] // 1071741 is max
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

        [Benchmark(Description = "DefaultInterpolatedStringHandler")]
        public void DefaultInterpolatedStringHandler()
        {
            var builder = new DefaultInterpolatedStringHandler();
            for (int i = 0; i < StrLength; i++)
            {
                builder.AppendLiteral($"{i % 10}");
            }

            var result = builder.ToStringAndClear();
        }

        [Benchmark(Description = "Concatenation")]
        public void Concatenation()
        {
            string result = string.Empty;
            for (int i = 0; i < StrLength; i++)
            {
                result += $"{i % 10}";
            }
        }

        [Benchmark(Description = "StringCreator")]
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