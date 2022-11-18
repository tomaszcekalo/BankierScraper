using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BankierScraper
{
    public class MainPageScraper : IMainPageScraper
    {
        //HotNews
        //DailyNews
        //HomeQuote
        //QuotationOfTheDay
        //HomeSpecialTopic
        //HomePBNews #home-pb-news-box
        //Markets #markets-box
        // #home-link-box

        //Finances #finances-box
        //Business #business-box
        //Satellites #home-satellites-box
        //

        public async Task<object> Scrape()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var address = "https://www.bankier.pl/";
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(address);
            var cellSelector = "#hotnews-box a";
            var cells = document.QuerySelectorAll(cellSelector);
            //var titles = cells.Select(m => m);
            var articles = cells.OfType<IUrlUtilities>();
            //AngleSharp.Html.Dom.HtmlAnchorElement;
            //AngleSharp.Html.Dom.HtmlUrlBaseElement
            var result = new object[]
            {
                ParseHotNews(document),
                ParseDailyNews(document),
                ParseHomeQuote(document),
                ParsePBNews(document),
                ParseMarkets(document),
            };
            return result;
        }

        private object ParseMarkets(IDocument document)
        {
            var cellSeceltor = "#markets-box ul li a";
            var cells = document.QuerySelectorAll(cellSeceltor);
            var items = cells
                .Select(x => new
                {
                    x.TextContent,
                    Href = x.GetAttribute("href")
                });
            return items;
        }

        private object ParsePBNews(IDocument document)
        {
            var cellSeceltor = "#home-pb-news-box ul.m-home-pb-news-list li.m-home-pb-news-list__item a";
            var cells = document.QuerySelectorAll(cellSeceltor);
            var items = cells
                .Select(x => new
                {
                    x.TextContent,
                    Href = x.GetAttribute("href")
                });
            return items;
        }

        public object ParseHotNews(IDocument document)
        {
            var cellSelector = "#hotnews-box a";
            var cells = document.QuerySelectorAll(cellSelector);
            var items = cells
                .Select(x => new
                {
                    x.TextContent,
                    Href = x.GetAttribute("href")
                });
            return items;
        }

        public object ParseDailyNews(IDocument document)
        {
            var cellSelector = "#home-dailynews-box .m-title-with-label-item:not(.-orange)";
            var cells = document.QuerySelectorAll(cellSelector);
            var items = cells
                .Select(x => new
                {
                    x.QuerySelector(".m-title-with-label-item__title").TextContent,
                    Href = x.GetAttribute("href")
                });
            return items;
        }

        public object ParseHomeQuote(IDocument document)
        {
            var cellSelector = "#home-quote-box li";
            var cells = document.QuerySelectorAll(cellSelector);
            var items = cells
                .Select(x => new
                {
                    a = x.QuerySelector("a"),
                    qiValue = x.QuerySelector("span.-value"),
                    qipercentageChange = x.QuerySelector("span.-percentage-change")
                })
                .Select(x => new
                {
                    Href = x.a.GetAttribute("href"),
                    x.a.TextContent,
                    Value = x.qiValue.TextContent,
                    PercentageChange = x.qipercentageChange.TextContent
                });
            return items;
        }
    }
}