using logic.SearchManager;

namespace GitFuzz.Tests;

public class SearchManagerTests
{
    [Fact]
    public void Search_ReturnEmptyListWhenNoBranches()
    {
        var searchManager = new SearchManager();

        var term = "test";
        var branches = new List<string>();

        var result = searchManager.Search(term, branches);

        Assert.True(result.Count == 0);
    }

    [Fact]
    public void Search_ReturnEmptyListWhenTermNull()
    {
        var searchManager = new SearchManager();

        string term = null;
        var branches = new List<string>();

        var result = searchManager.Search(term, branches);

        Assert.True(result.Count == 0);
    }

    [Fact]
    public void Search_ReturnEmptyListWhenTermEmpty()
    {
        var searchManager = new SearchManager();

        var term = string.Empty;
        var branches = new List<string>();

        var result = searchManager.Search(term, branches);

        Assert.True(result.Count == 0);
    }

    [Fact]
    public void Search_ReturnsMatchingResultsAndSelected()
    {
        var searchManager = new SearchManager();

        var term = "test";
        var branches = new List<string>()
        {
            "atesta",
            "test",
            "*main",
            "notreturned",
            "__t__e&&s0abtgg",
            "12356tes77t"
        };

        var expected = new List<string>()
        {
            "*main",
            "test",
            "atesta",
            "12356tes77t",
            "__t__e&&s0abtgg"
        };

        var result = searchManager.Search(term, branches);

        Assert.True(result.Count == expected.Count);
        for(int i = 0; i < expected.Count; i++)
        {
            Assert.True(expected[i].Equals(result[i]));
        }
    }
}
