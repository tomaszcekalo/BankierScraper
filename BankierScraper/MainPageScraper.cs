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
        //DilyNews
        //HomeQuote
        //QuotationOfTheDay
        //HomeSpecialTopic
        //HomePBNews
        //Markets
        //Finances
        //Business
        //Satellites
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
            var items = cells
                .Select(x => new
                {
                    x.TextContent,
                    Href = x.GetAttribute("href")
                });
            //var items = articles.Select(x => new
            //{
            //    x.Href,
            //    //x.
            //});
            Console.ReadKey();
        }
    }
}