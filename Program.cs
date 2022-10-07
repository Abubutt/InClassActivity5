using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPIClient;

class Game
{
    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("short_description")]
    public string Description { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("genre")]
    public string Genre { get; set; }

}


internal class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    { 
        await ProcessRepositories();
    }

    private static async Task ProcessRepositories()
    {
 
        try
        {
            while (true)
            {
                Console.WriteLine("Enter Id number between 1-511 to get details on a random mmo game. Press Enter without writing an id to quit the program.");
                var idNum = Console.ReadLine();
                if (string.IsNullOrEmpty(idNum))
                {
                    break;
                }

                if((Int64.Parse(idNum) < 1 || Int64.Parse(idNum) > 511))
                {
                    throw new Exception();
                }
                var result = await client.GetAsync("https://www.mmobomb.com/api1/game?id=" + idNum);
                var resultRead = await result.Content.ReadAsStringAsync();
                var game = JsonConvert.DeserializeObject<Game>(resultRead);

                Console.WriteLine("-----Game Details-----");
                Console.WriteLine("");
                Console.WriteLine("Title: " + game.Title);
                Console.WriteLine("");
                Console.WriteLine("Status: " + game.Status);
                Console.WriteLine("");
                Console.WriteLine("Genre: " + game.Genre);
                Console.WriteLine("");
                Console.WriteLine("Description: " + game.Description);
                Console.WriteLine("");
                Console.WriteLine("-----Game Details-----");
                Console.WriteLine("");
            }
        }
        catch (Exception)
        {
            Console.WriteLine("ERROR. Please enter a valid id!");
        }
    }
}