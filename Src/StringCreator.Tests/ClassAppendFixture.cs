using NUnit.Framework;
using System;
using System.Text;

namespace StringCreatorTests
{
    [TestFixture]
    public class ClassAppendFixture
    {
        public static object[] LengthCases = { 5, 7, 10, 50, 100, 1000, 2500 };

        [Test]
        [TestCaseSource(nameof(LengthCases))]
        public void AppendTest(int length)
        {
#if NET6_0_OR_GREATER
            var resultLength = length * 2;
#else
            var resultLength = length;
#endif
            var sb = new StringBuilder(resultLength);
            var sc = new StringCreator.Class.StringCreator(resultLength);
            for (int i = 0; i < length; i++)
            {
                sb.Append((i % 10).ToString());
                sc.Append((i % 10).ToString());
#if NET6_0_OR_GREATER
                sb.Append($"{i % 10}");
                sc.Append($"{i % 10}");
#endif
            }

            Assert.That(sc.ToString(), Is.EqualTo(sb.ToString()));
        }

#if NET6_0_OR_GREATER
        [Test]
        [TestCaseSource(nameof(LengthCases))]
        public void AppendFromSpanTest(int length)
        {
            var sb = new StringBuilder(length);
            var sc = new StringCreator.Class.StringCreator(length);
            for (int i = 0; i < length; i++)
            {
                var arr = new char[1] { (char)(i % 10) };
                sb.Append(arr.AsSpan());
                sc.Append(arr.AsSpan());
            }

            Assert.That(sc.ToString(), Is.EqualTo(sb.ToString()));
        }
#endif

        [Test]
        [TestCaseSource(nameof(LengthCases))]
        public void AppendFromArrayTest(int length)
        {
            var sb = new StringBuilder(length);
            var sc = new StringCreator.Class.StringCreator(length);
            for (int i = 0; i < length; i++)
            {
                var arr = new char[1] { (char)(i % 10) };
                sb.Append(arr);
                sc.Append(arr);
            }

            Assert.That(sc.ToString(), Is.EqualTo(sb.ToString()));
        }
    }
}