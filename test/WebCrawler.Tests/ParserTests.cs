
namespace WebCrawler.Tests;

public class ParserTests
{
    private string filePath = Path.Combine(AppContext.BaseDirectory, "assets", "AbrahamLincoln.html");
    private string title = "Abraham Lincoln";
    private string fileCouldNotBeRead = "File could not be read:";

    // todo: testing
    // - [ ] testing for bad pages
    // - [ ] testing for pages with no links

    //Unit tests noted with Real in the title are tests ran against html files pulled from the wiki, more as integration testing
    [Fact]
    public void ShouldGetTitleFromRealPage()
    {
        //Arrange
        string html = GetHtmlFromFile(filePath);
        Parser parser = new Parser(html);

        //Act
        parser.ParseTitle();

        //Assert
        Assert.Equal(title, parser.pageTitle);
    }

    [Fact]
    public void ShouldGetAnyLinksFromRealPage()
    {
        //Arrange
        string html = GetHtmlFromFile(filePath);
        Parser parser = new Parser(html);

        //Act
        parser.ParseLinks();

        //Assert
        Assert.NotEmpty(parser.linkedPages);        
    }

    [Fact]
    public void ShouldGetCorrectLinksFromMockedPage()
    {
        //Arrange
        string html = @"<html><body><a href=""/wiki/Link1"">Link1</a><a href=""/wiki/Link2"">Link2</a><a href=""/wiki/Link3"">Link3</a></body></html>";
        Parser parser = new Parser(html);

        //Act
        parser.ParseLinks();

        //Assert
        Assert.Equal(3, parser.linkedPages.Count);
        Assert.Contains("Link1", parser.linkedPages);
        Assert.Contains("Link2", parser.linkedPages);
        Assert.Contains("Link3", parser.linkedPages);
    }

    [Fact]
    public void ShouldNotGetLinksWithColonsFromMockedPage()
    {
        //Arrange
        string html = @"<html><body><a href=""/wiki/Link1"">Link1</a><a href=""/wiki/Link:2"">Link2</a><a href=""/wiki/Link3"">Link3</a></body></html>";
        Parser parser = new Parser(html);

        //Act
        parser.ParseLinks();

        //Assert
        Assert.Equal(2, parser.linkedPages.Count);
        Assert.Contains("Link1", parser.linkedPages);
        Assert.Contains("Link3", parser.linkedPages);
    }
    
    [Fact]
    public void ShouldNotGetMoreThanMaxOutLinksFromMockedPage()
    {
        //Arrange
        string html = @"<html><body><a href=""/wiki/Link1"">Link1</a><a href=""/wiki/Link2"">Link2</a><a href=""/wiki/Link3"">Link3</a></body></html>";
        Parser parser = new Parser(html, 2);

        //Act
        parser.ParseLinks();

        //Assert
        Assert.Equal(2, parser.linkedPages.Count);
    }

    #region Helper Methods
    private string GetHtmlFromFile(string path)
    {
        string html = string.Empty;
        try
        {
            using StreamReader streamReader = new(path);
            html = streamReader.ReadToEnd();
        }
        catch(IOException e)
        {
            Console.WriteLine(fileCouldNotBeRead);
            Console.WriteLine(e.Message);
            Assert.Fail(e.Message);
        }

        return html;
    }
    #endregion
}