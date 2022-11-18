using Newtonsoft.Json;
using System.Threading.Tasks;
using BankierScraper;
using System;

var result = await new MainPageScraper().Scrape();
Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));