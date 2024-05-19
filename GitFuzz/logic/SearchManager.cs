using System.Text;
using System.Text.RegularExpressions;

namespace logic.SearchManager
{
    public class SearchManager : ISearchManager
    {
        public SearchManager() {}

        public List<string> Search(string term, List<string> branches)
        {
            if(branches.Count == 0 || term == null)
            {
                return branches;
            }
            if(String.IsNullOrWhiteSpace(term))
            {
                return new List<string>();
            }

            var selectedIndex = GetSelectedIndex(branches);

            var distances = new Dictionary<string, int>();
            distances.Add(branches[selectedIndex], 0);

            // Find matches
            var matchString = GetRegexString(term.Trim().ToLower());
            var matchingBranches = branches.Except(new List<string> { branches.ElementAt(selectedIndex) }).Where(b => Regex.IsMatch(b.Trim().ToLower(), matchString)).ToList();
            selectedIndex = 0;

            foreach(var branch in matchingBranches.Where(b => b.Length >= term.Length))
            {
                distances.Add(branch, LevDistance(term.Trim().ToLower(), branch.Trim().ToLower()));
            }

            return distances.OrderBy(d => d.Value).Select(o => o.Key).ToList();
        }

        private int GetSelectedIndex(List<string> branches)
        {
            for(int i = 0; i < branches.Count; i++)
            {
                if (branches[i].StartsWith("*"))
                {
                    return i;
                }
            }
            return 0;
        }

        private int LevDistance(string a, string b)
        {
            if(b.Length == 0)
            {
                return a.Length;
            }
            if(a.Length == 0)
            {
                return b.Length;
            }

            if(a[0] == b[0])
            {
                return LevDistance(a.Substring(1), b.Substring(1));
            }

            return (1 + Math.Min(Math.Min(LevDistance(a.Substring(1), b), LevDistance(a, b.Substring(1))), LevDistance(a.Substring(1), b.Substring(1))));
        }

        private string GetRegexString(string searchTerm)
        {
            var sb = new StringBuilder();
            foreach(var c in searchTerm)
            {
                sb.Append(".*");
                sb.Append(c);
            }
            sb.Append(".*");
            return sb.ToString();
        }
    }
}
