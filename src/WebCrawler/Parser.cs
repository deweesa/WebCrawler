using HtmlAgilityPack;

namespace WebCrawler;

/*
<summary>
Class responsible for parsing html from the wiki pages. Life cycle will be intiated
by some sort of orchestrator class that will make the calls to the web.
</summary>
*/
//todo: instantiating class should give what it thinks the title should be, for logging purposes.
public class Parser
{
    //! Should we be so attribute happy?
    private string pageHtml = string.Empty;
    public string pageTitle = string.Empty;
    private int prefixLength = "/wiki/".Length; 
    private HtmlDocument pageDocument;
    public HashSet<string> linkedPages;
    private int maxOutLinks;

    public Parser(string _pageHtml, int _maxOutLinks)
    {
        pageHtml = _pageHtml;
        pageDocument = new HtmlDocument();
        pageDocument.LoadHtml(pageHtml);
        linkedPages = new HashSet<string>();
        maxOutLinks = _maxOutLinks;
    }

    public Parser(string _pageHtml)
    {
        pageHtml = _pageHtml;
        pageDocument = new HtmlDocument();
        pageDocument.LoadHtml(pageHtml);
        linkedPages = new HashSet<string>();
        maxOutLinks = 5;
    }

    public void Parse()
    {
        ParseTitle();
        ParseLinks();
    }

    public void ParseLinks()
    {
        pageDocument.DocumentNode.SelectNodes("//a[@href]")?.ToList().ForEach(node =>
        {
            string hrefValue = node.GetAttributeValue("href", string.Empty);
            if (hrefValue.StartsWith("/wiki/") && !hrefValue.Contains(":"))
            {
                hrefValue = hrefValue[prefixLength..]; //todo: this is a bit hacky, maybe we should be doing something more robust here? also this only works for well formed links
                linkedPages.Add(hrefValue);
            }
        });

        //? Should we select random links here if we have a max out links? This would be more robust than just taking the first n links, which could be biased in some way.
        if (maxOutLinks != -1 && linkedPages.Count > maxOutLinks)
        {
            linkedPages = linkedPages.Take(maxOutLinks).ToHashSet(); //todo: this is also a bit hacky, maybe we should be doing something more robust here?
        }
    } 

    public void ParseTitle()
    {
        HtmlNode titleElement  = pageDocument.DocumentNode.SelectSingleNode("//span[@class='mw-page-title-main']");
        if (titleElement == null)
        {
            Console.WriteLine("Title element not found in page html"); //todo: instantiating a logger would be better than writing to console, but this is fine for now
            return;
        }
        pageTitle = titleElement.InnerText;
    }
}