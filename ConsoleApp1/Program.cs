using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Json;

namespace ConsoleApp1
{
    class Program
    {
        async static Task<List<Record>> FetchFlats()
        {
            var flats = new List<Record>();
            using (var httpClient = new HttpClient())
            {
                using var response = await httpClient.GetAsync(@"http://net-poland-interview-stretto.us-east-2.elasticbeanstalk.com/api/flats/csv");
                {
                    response.EnsureSuccessStatusCode();
                    using var streamReader = new StreamReader(await response.Content.ReadAsStreamAsync());
                    {
                        while (!streamReader.EndOfStream)
                        {
                            Console.WriteLine("1. Flats: ");
                            var line = await streamReader.ReadLineAsync();
                            Console.WriteLine(line);
                            flats.Add(new Record(line));
                        }
                    }
                }
            }
            return flats;
        }



        async static Task Main(string[] args)
        {
            var listOfFlats = new List<Record>();
            try
            {
                listOfFlats = await FetchFlats();
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an error with fetching flats.");
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Closing program");
            }

            if (!listOfFlats.Any())
            {
                Console.WriteLine("Flats are empty. Closing program.");
                return;
            }
            listOfFlats.RemoveAt(0); // removing headers
            WaitForKeyAndClearScreen();
            Console.WriteLine("2. Objects of flats: ");

            listOfFlats.ForEach(c => Console.WriteLine(JsonSerializer.Serialize(c, typeof(Record))));

            WaitForKeyAndClearScreen();
            Console.WriteLine("3. Biggest flats by City: ");
            var GroupByCity = listOfFlats.Where(c => c.Type == "Residential").GroupBy(c => c.City);
            foreach (var group in GroupByCity)
            {
                Console.WriteLine(group.Aggregate((a, b) => a.SqFt > b.SqFt ? a : b).ToString());
            }

            WaitForKeyAndClearScreen();
            Console.WriteLine("4. Biggest cheapest flats: ");

            Console.WriteLine(listOfFlats.OrderByDescending(c => c.Baths + c.Beds).ThenBy(c => c.Price).First().ToString());

            WaitForKeyAndClearScreen();
            Console.WriteLine("5. Most expensive flat with tax by City: ");
            List<Record> mostExpensiveFlatsWithTaxByCities = new List<Record>();
            foreach (var group in GroupByCity)
            {
                var mostExpensiveFlatInCity = group.Aggregate((a, b) => a.Price > b.Price ? a : b);
            }

            WaitForKeyAndClearScreen();
        }

        private static void WaitForKeyAndClearScreen()
        {
            Console.ReadLine();
            Console.Clear();
        }
    }
}
