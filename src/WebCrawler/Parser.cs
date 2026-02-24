using HtmlAgilityPack;

namespace WebCrawler;

/*
<summary>
Class responsible for parsing html from the wiki pages. Life cycle will be intiated
by some sort of orchestrator class that will make the calls to the web.
</summary>
*/
public class Parser
{
    private string pageHtml;
    public string pageTitle;
    private HtmlDocument pageDocument;
    public HashSet<string> linkedPages;
    private int maxOutLinks;

    public Parser(string _pageTitle, string _pageHtml, int _maxOutLinks = -1)
    {
        pageTitle = _pageTitle;
        pageHtml = _pageHtml;
        pageDocument = new HtmlDocument();
        pageDocument.LoadHtml(pageHtml);
        linkedPages = new HashSet<string>();
        maxOutLinks = _maxOutLinks;
    }

    public Parser(string _pageHtml)
    {
        pageHtml = _pageHtml;
        pageTitle = ExtractTitle();
        pageDocument = new HtmlDocument();
        pageDocument.LoadHtml(pageHtml);
        linkedPages = new HashSet<string>();
        maxOutLinks = -1;
    }

    public void Parse()
    {
        ExtractTitle();
        ExtractLinks();
    }

    public void ExtractLinks()
    {
        throw new NotImplementedException();
    } 

    public string ExtractTitle()
    {
        throw new NotImplementedException();
    }
}
