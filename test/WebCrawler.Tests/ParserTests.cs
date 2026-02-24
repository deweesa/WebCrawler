
namespace WebCrawler.Tests;

public class UnitTest1
{
    private string filePath = "./assets/AbrahamLincoln.html";
    private string title = "Abraham Lincoln";
    private string fileCouldNotBeRead = "File could not be read:";
    [Fact]
    public void Test1()
    {
        Assert.True(true);
    }

    [Fact]
    public void ShouldGetTitleFromPage()
    {
        string html = null;
        try
        {
            using StreamReader streamReader = new(filePath);
            html = streamReader.ReadToEnd();
        }
        catch(IOException e)
        {
            Console.WriteLine(fileCouldNotBeRead);
            Console.WriteLine(e.Message);
            Assert.Fail(e.Message);
        }

        Parser parser = new Parser(html);
        Assert.Equal(title, parser.ExtractTitle());
    }
}