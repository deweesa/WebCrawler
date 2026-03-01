using System;

namespace WebCrawler;

public class WorkerOrchestrator
{
    private Queue<Worker> workerQueue = new Queue<Worker>();
    private HashSet<string> seenPages = new HashSet<string>(); 

    public WorkerOrchestrator()
    {
        // todo: nice to have, fun to have this start off some page from the main page
        Worker worker = new Worker("abraham_lincoln", 0, this);
        seenPages.Add("abraham_lincoln");
        workerQueue.Enqueue(worker);
    }

    public WorkerOrchestrator(string startingTitle)
    {
            Worker worker = new Worker(startingTitle, 0, this);
            seenPages.Add(startingTitle);
            workerQueue.Enqueue(worker);
    }

    public async Task Crawl()
    {
        Console.WriteLine("Starting crawl...");
        while (workerQueue.Count > 0)
        {
            Worker worker = workerQueue.Dequeue();
            await worker.DoWork(); 
        }
        Console.WriteLine("Crawl finished.");
    }

    public void Enqueue(string linkedPage, int depth)
    {
        if (seenPages.Contains(linkedPage))
            return;
        seenPages.Add(linkedPage);

        if (depth > 5) //todo: configurable max depth, not hard coded
            return;

        Worker worker = new Worker(linkedPage, depth, this);
        workerQueue.Enqueue(worker);
    }
}
