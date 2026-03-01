using System;

namespace WebCrawler;

// ? I don't know if this is a good name
public class Worker
{
    private string linkTitle;
    private static string baseUrl = "https://en.wikipedia.org/wiki/";
    private readonly int currDepth;
    private static HttpClient httpClient = new HttpClient(); //todo: not ideal, fix this. Okay for now
    private readonly WorkerOrchestrator workerPool;

    public Worker(string _linkTitle, int _depth, WorkerOrchestrator _workerPool)
    {
        linkTitle = _linkTitle;
        currDepth = _depth;
        workerPool = _workerPool;
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

        //todo: check for if we've seen this linkTitle before

        //! this is actually super hacky and we need to ensure we respect the wiki's robots.txt file timeout rulees
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; WebCrawler/1.0; +http://www.yourdomain.com/crawler)"); 
        HttpResponseMessage response = await httpClient.GetAsync($"{baseUrl}{linkTitle}");
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Failed to fetch page: {linkTitle}, Status Code: {response.StatusCode}");
            return;
        }

        string html = await response.Content.ReadAsStringAsync();
        Parser parser = new Parser(html);
        parser.Parse(); 
        Console.WriteLine($"Depth: {currDepth}, Title: {parser.pageTitle}, Out Links: {parser.linkedPages.Count}"); //! this is just for testing

        //todo: if we haven't seen this page, add title at least

        List<string> outLinks = parser.linkedPages.ToList();
        foreach (string linkedPage in outLinks)
        {
            workerPool.Enqueue(linkedPage, currDepth + 1);
        }
    }
}
