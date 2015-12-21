using System.Collections.Generic;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using AngleSharp.Parser.Html;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;
using Benchmarks.HtmlParsers.Benchmarks.Base;
using CsQuery;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace Benchmarks.HtmlParsers.Benchmarks
{
    [BenchmarkTask]
    public class AHrefBenchmark : HtmlBenchmarkBase
    {
        protected override string ResourcePath => "Benchmarks.HtmlParsers.Examples.01.Hrefs.html";

        /// <summary>
        /// Extract all anchor tags using HtmlAgilityPack
        /// </summary>
        [Benchmark]
        public IEnumerable<string> HtmlAgilityPack()
        {
            HtmlDocument htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(Html);
            List<string> hrefTags = new List<string>();

            foreach (HtmlNode link in htmlSnippet.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute att = link.Attributes["href"];
                hrefTags.Add(att.Value);
            }

            return hrefTags;
        }

        /// <summary>
        /// Extract all anchor tags using Fizzler
        /// </summary>
        [Benchmark]
        public IEnumerable<string> Fizzler()
        {
            HtmlDocument htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(Html);
            List<string> hrefTags = new List<string>();

            foreach (HtmlNode node in htmlSnippet.DocumentNode.QuerySelectorAll("a"))
            {
                hrefTags.Add(node.GetAttributeValue("href", null));
            }

            return hrefTags;
        }

        /// <summary>
        /// Extract all anchor tags using CsQuery
        /// </summary>
        [Benchmark]
        public IEnumerable<string> CsQuery()
        {
            List<string> hrefTags = new List<string>();

            CQ cq = CQ.Create(Html);
            foreach (IDomObject obj in cq.Find("a"))
            {
                hrefTags.Add(obj.GetAttribute("href"));
            }

            return hrefTags;
        }

        /// <summary>
        /// Extract all anchor tags using AngleSharp
        /// </summary>
        [Benchmark]
        public IEnumerable<string> AngleSharp()
        {
            List<string> hrefTags = new List<string>();

            var parser = new HtmlParser();
            var document = parser.Parse(Html);
            foreach (IElement element in document.QuerySelectorAll("a"))
            {
                hrefTags.Add(element.GetAttribute("href"));
            }

            return hrefTags;
        }

        /// <summary>
        /// Extract all anchor tags using Regex
        /// </summary>
        [Benchmark]
        public IEnumerable<string> Regex()
        {
            List<string> hrefTags = new List<string>();

            Regex reHref = new Regex(@"(?inx)
        <a \s [^>]*
            href \s* = \s*
                (?<q> ['""] )
                    (?<url> [^""]+ )
                \k<q>
        [^>]* >");
            foreach (Match match in reHref.Matches(Html))
            {
                hrefTags.Add(match.Groups["url"].ToString());
            }

            return hrefTags;
        }
    }
}