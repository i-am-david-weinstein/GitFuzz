using display.Drawer;
using logic.BranchManager;
using logic.SearchManager;

var branchManager = new BranchManager();
var searchManager = new SearchManager();
var drawer = new Drawer(branchManager);

var searchTerm = args[0];

var matchingBranches = searchManager.Search(searchTerm, branchManager.GetBranches());

drawer.Draw(matchingBranches);
