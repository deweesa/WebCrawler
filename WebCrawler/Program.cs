internal class Program
{
    private static async Task<int> Main(string[] args)
    {
        if(args.Length == 0)
        {
            Console.WriteLine("Please enter url");
            return -1;
        }
        string url = args[0];

        Console.WriteLine($"Url to scrape: {url}");

        return 1;
    }

    private static async Task OldMain(string[] args)
    {
        var counter = 0;
        var max = args.Length is not 0 ? Convert.ToInt32(args[0]) : -1;

        while (max is -1 || counter < max)
        {
            Console.WriteLine($"Counter: {++counter}");
            await Task.Delay(TimeSpan.FromMilliseconds(1_000));
        }
    }
}