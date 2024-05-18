using logic.BranchManager;
using System.Text;

namespace display.Drawer
{

    public class Drawer : IDrawer
    {

        private readonly IBranchManager _branchManager;

        public Drawer(IBranchManager branchManager)
        {
            _branchManager = branchManager;
        }

        public void Draw(List<string> branches)
        {
            int selectedIndex = 0;

            // Draw loop
            while (true)
            {
                Draw(branches, selectedIndex);
                var input = Console.ReadKey().Key;

                switch(input)
                {
                    case ConsoleKey.Enter:

                        Console.Clear();

                        var b = branches[selectedIndex].Replace("[X] ", "").Replace("[ ] ", "");
                        _branchManager.SwitchBranch(b);
                        return;
                    case ConsoleKey.DownArrow:
                        if(selectedIndex < (branches.Count - 1))
                        {
                            selectedIndex++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if(selectedIndex > 0)
                        {
                            selectedIndex--;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void Draw(List<string> branches, int selectedIndex)
        {
            Console.Clear();
            var defaultColor = Console.ForegroundColor;
            for(int i = 0; i < branches.Count; i++)
            {
                var sb = new StringBuilder();
                if (branches[i].StartsWith("*"))
                {
                    sb.Append(branches[i].Replace("* ", "[X] "));
                }
                else
                {
                    sb.Append("[ ] ");
                    sb.Append(branches[i].Trim());
                }

                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine(sb.ToString());

                Console.ForegroundColor = defaultColor;
            }
        }
    }

}

