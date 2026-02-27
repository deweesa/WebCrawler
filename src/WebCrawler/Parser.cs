using HtmlAgilityPack;

namespace WebCrawler;

/*
<summary>
Class responsible for parsing html from the wiki pages. Life cycle will be intiated
by some sort of orchestrator class that will make the calls to the web.
</summary>
*/
//todo: what does a link node look like?
// - obv they're a tags
// - they all have /wiki/ in the href
public class Parser
{
    private string pageHtml;
    public string pageTitle;
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
        maxOutLinks = -1;
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

        if (maxOutLinks != -1 && linkedPages.Count > maxOutLinks)
        {
            linkedPages = linkedPages.Take(maxOutLinks).ToHashSet(); //todo: this is also a bit hacky, maybe we should be doing something more robust here?
        }
    } 

    public void ParseTitle()
    {
        HtmlNode titleElement  = pageDocument.DocumentNode.SelectSingleNode("//span[@class='mw-page-title-main']");
        pageTitle = titleElement?.InnerText; //todo: handle null case, this only works for well formed title nodes
    }
}