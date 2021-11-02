namespace BankierScraper
{
    public class Bankier : IBankier
    {
        public IGieldaScraper Gielda { get; }

        public Bankier(IGieldaScraper gielda)
        {
            Gielda = gielda;
        }
    }
}