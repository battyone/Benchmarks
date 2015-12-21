using BenchmarkDotNet;
using Benchmarks.HtmlParsers.Helpers;

namespace Benchmarks.HtmlParsers.Benchmarks.Base
{
    public abstract class HtmlBenchmarkBase
    {
        protected abstract string ResourcePath { get; }

        protected virtual string Html { get; private set; }

        [Setup]
        public virtual void SetupData()
        {
            Html = Loader.LoadResourceAsText(ResourcePath);
        }
    }
}