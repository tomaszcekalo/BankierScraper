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

        public async Task Scrape()
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
            ParseHotNews(document);
            ParseDailyNews(document);
            ParseHomeQuote(document);
            Console.ReadKey();
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
                    a=x.QuerySelector("a"),
                    qiValue=x.QuerySelector("span.-value"),
                    qipercentageChange=x.QuerySelector("span.-percentage-change")
                })
                .Select(x => new
                {
                    Href=x.a.GetAttribute("href"),
                    x.a.TextContent,
                    Value=x.qiValue.TextContent,
                    PercentageChange=x.qipercentageChange.TextContent
                });
            return items;
        }
    }
}