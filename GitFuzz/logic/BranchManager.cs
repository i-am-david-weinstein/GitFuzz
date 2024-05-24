using System.Diagnostics;

namespace logic.BranchManager
{
    public class BranchManager : IBranchManager
    {
        
        private List<string> _branches;

        public BranchManager()
        {
            var branchString = RetrieveBranches();
            _branches = ParseBranches(branchString);
        }

        public List<string> GetBranches()
        {
            return _branches;
        }

        public void SwitchBranch(string branch)
        {

            if (branch.Length == 0 || branch[0] == '*')
            {
                return;
            }

            Process proc = new Process();

            proc.StartInfo.FileName = "git";
            proc.StartInfo.Arguments = $"checkout {branch}";


            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;

            proc.Start();

            string output = proc.StandardOutput.ReadToEnd();

            proc.WaitForExit();

            Console.WriteLine(output);
        }

        private string RetrieveBranches()
        {
            Process proc = new Process();

            proc.StartInfo.FileName = "git";
            proc.StartInfo.Arguments = "branch";


            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;

            proc.Start();

            string output = proc.StandardOutput.ReadToEnd();

            proc.WaitForExit();

            return output;
        }

        private List<string> ParseBranches(string output)
        {
            var branches = output.Split('\n');
            return branches.Where(b => !String.IsNullOrEmpty(b)).ToList();
        }
    }
}
