namespace logic.BranchManager
{
    public interface IBranchManager
    {
        List<string> GetBranches();
        void SwitchBranch(string branch);
    }
}
