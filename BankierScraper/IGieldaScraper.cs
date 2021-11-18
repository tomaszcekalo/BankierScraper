namespace BankierScraper
{
    public interface IGieldaScraper
    {
        public object Scrape();
        public object ParseExchangeHotNews();//#boxExchangeHotNews
        //https://www.bankier.pl/new-charts/get-data?symbol=WIG30&intraday=true&today=true&type=area
        // #last-trade-*
    }
}