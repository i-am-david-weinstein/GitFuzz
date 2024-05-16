namespace logic.SearchManager
{
    public interface ISearchManager
    {
        List<string> Search(string term, List<string> branches);
    }
}
