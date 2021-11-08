using System.Threading.Tasks;

namespace BankierScraper.Console
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await new MainPageScraper().Scrape();
        }
    }
}