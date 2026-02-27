using System;

namespace WebCrawler;

// ? I don't know if this is a good name
public class Worker
{
    private Parser parser;
    private string linkTitle;
    private static string baseUrl = "https://en.wikipedia.org/wiki/";
    int depth;

    public Worker(string _linkTitle, int _depth)
    {
        linkTitle = _linkTitle;
        depth = _depth;
    }

     public async Task DoWork()
    {
        /* 
        1. Build Link
        2. Get page
        3. Parse page, with parser class
        4. Get real title from parser, this'll go into our trie tree thingy guy way later
        5. Get the links that we want
        6. Check if we've seen the links before, if not add them to the queue and mark them as seen
        6.5. If we've reached our depth, don't add
        */

        HttpClient httpClient= new HttpClient();
        HttpResponseMessage response = await httpClient.GetAsync($"{baseUrl}{linkTitle}");
        Parser parser = new Parser(response.Content.ToString());
    }
}
