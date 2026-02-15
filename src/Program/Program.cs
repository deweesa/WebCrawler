using System.CommandLine;

class Program
{
    private static int Main(string[] args)
    {
        Option<int> depthOption = new("depth", ["-d", "--depth"])
        {
            Description = "Maximum depth for crawler to search",
            DefaultValueFactory = parseResult => 5
        };

        Option<int> breadthOption = new("breadth", ["-b", "--breadth"])
        {
            Description = "Maximum breadth for crawler to search",
            DefaultValueFactory = parseResult => 5
        };

        Option<string> pageOption = new("page", ["-p", "--page"])
        {
            Description = "Starting page"
        };

        RootCommand rootCommand = new("Console App for running WebCrawler");

        rootCommand.Options.Add(depthOption);
        rootCommand.Options.Add(breadthOption);
        rootCommand.Options.Add(pageOption);

        rootCommand.SetAction(parseResult =>
        {
            int depth = parseResult.GetValue(depthOption);
            int breadth = parseResult.GetValue(breadthOption);
            string? page = parseResult.GetValue(pageOption);
        });

        ParseResult parseResult = rootCommand.Parse(args);
        return parseResult.Invoke();
    }
}