using HtmlAgilityPack;

namespace WebCrawler;

public class Parser
{
    private string pageHtml;
    private string pageTitle;
    private HtmlDocument pageDocument;
    public HashSet<string> linkedPages;

    public Parser(string _pageTitle, string _pageHtml)
    {
        pageTitle = _pageTitle;
        pageHtml = _pageHtml;
        pageDocument = new HtmlDocument();
        pageDocument.LoadHtml(pageHtml);
        linkedPages = new HashSet<string>();
    }

    public Parser(string _pageHtml)
    {
        pageHtml = _pageHtml;
        pageTitle = ExtractTitle();
        pageDocument = new HtmlDocument();
        pageDocument.LoadHtml(pageHtml);
        linkedPages = new HashSet<string>();
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
