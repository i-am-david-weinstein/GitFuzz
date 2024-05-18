using display.Drawer;
using logic.BranchManager;
using logic.SearchManager;

public class GitFuzz
{

    private readonly ISearchManager _searchManager;
    private readonly IBranchManager _branchManager;
    private readonly IDrawer _drawer;

    public GitFuzz(ISearchManager searchManager, IBranchManager branchManager, IDrawer drawer)
    {
        _searchManager = searchManager;
        _branchManager = branchManager;
        _drawer = drawer;
    }

    public void Run(string[] args)
    {
        var searchTerm = args[0];
        var matchingBranches = _searchManager.Search(searchTerm, _branchManager.GetBranches());

        _drawer.Draw(matchingBranches);
    }
}
