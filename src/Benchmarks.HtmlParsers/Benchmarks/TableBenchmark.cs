using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;
using Benchmarks.HtmlParsers.Benchmarks.Base;

namespace Benchmarks.HtmlParsers.Benchmarks
{
    [BenchmarkTask]
    public class TableBenchmark : HtmlBenchmarkBase
    {
        protected override string ResourcePath => "Benchmarks.HtmlParsers.Examples.02.Table.html";

        /// <summary>
        /// Extract exchange currency table using AngleSharp
        /// </summary>
        [Benchmark]
        public List<BranchBankCurrency> AngleSharp()
        {
            var parser = new HtmlParser();
            var document = parser.Parse(Html);

            var currencyTable = document.QuerySelector<IHtmlTableElement>("table");

            string currentBankName = null;
            var currencies = new List<BranchBankCurrency>();
            var factory = new RateFactory();
            foreach (var row in currencyTable.Rows.Skip(1))
            {
                if (!row.ClassList.Contains("tablesorter-childRow"))
                {
                    var currentBankCell = row.Cells[1];
                    currentBankName = currentBankCell.Text();
                    continue;
                }

                var cells = row.Cells;
                var branchBankAddressParts = cells.ElementAtOrDefault(0)
                    .Text()
                    .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

                var rates = new List<Rate>
                {
                    factory.CreateUsdBuyRate(cells.ElementAtOrDefault(1).Text()),
                    factory.CreateUsdSellRate(cells.ElementAtOrDefault(2).Text()),
                    factory.CreateEurBuyRate(cells.ElementAtOrDefault(3).Text()),
                    factory.CreateEurSellRate(cells.ElementAtOrDefault(4).Text()),
                    factory.CreateRubBuyRate(cells.ElementAtOrDefault(5).Text()),
                    factory.CreateRubSellRate(cells.ElementAtOrDefault(6).Text())
                };

                var currency = new BranchBankCurrency
                {
                    Bank = currentBankName,
                    Name = branchBankAddressParts[0].Trim(' ', '-'),
                    FullAddress = string.Join(string.Empty, branchBankAddressParts.Skip(1)).Trim(),
                    Rates = rates

                };

                currencies.Add(currency);
            }

            return currencies;
        }

        #region Models

        public class RateFactory
        {
            public Rate CreateRate(string rate, string exchangeCode, ExchangeDirection direction)
            {
                return new Rate
                {
                    CurrencyRate = double.Parse(rate),
                    ExchangeCode = exchangeCode,
                    Direction = direction
                };
            }

            public Rate CreateUsdBuyRate(string rate)
            {
                return CreateRate(rate, "USD", ExchangeDirection.Buy);
            }

            public Rate CreateUsdSellRate(string rate)
            {
                return CreateRate(rate, "USD", ExchangeDirection.Sell);
            }

            public Rate CreateEurBuyRate(string rate)
            {
                return CreateRate(rate, "EUR", ExchangeDirection.Buy);
            }

            public Rate CreateEurSellRate(string rate)
            {
                return CreateRate(rate, "EUR", ExchangeDirection.Sell);
            }

            public Rate CreateRubBuyRate(string rate)
            {
                return CreateRate(rate, "RUB", ExchangeDirection.Buy);
            }

            public Rate CreateRubSellRate(string rate)
            {
                return CreateRate(rate, "RUB", ExchangeDirection.Sell);
            }
        }

        public class Rate
        {
            public ExchangeDirection Direction { get; set; }
            public string ExchangeCode { get; set; }
            public double CurrencyRate { get; set; }
        }

        public class BranchBankCurrency
        {
            public string Bank { get; set; }

            public string Name { get; set; }

            public string FullAddress { get; set; }

            public IEnumerable<Rate> Rates { get; set; }
        }

        public enum ExchangeDirection
        {
            Sell,
            Buy
        }

        #endregion
    }
}