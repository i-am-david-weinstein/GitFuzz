using System.Diagnostics;
using System.Text;

Process proc = new Process();

proc.StartInfo.FileName = "git";
proc.StartInfo.Arguments = "branch";


proc.StartInfo.UseShellExecute = false;
proc.StartInfo.RedirectStandardOutput = true;

proc.Start();

string output = proc.StandardOutput.ReadToEnd();

proc.WaitForExit();

var branches = ParseBranches(output);
var selectedIndex = GetSelectedIndex(branches);

// Draw loop
while (true)
{
    Draw(branches, selectedIndex);
    var input = Console.ReadKey().Key;

    switch(input)
    {
        case ConsoleKey.Enter:
            SwitchBranch(selectedIndex, branches);
            return;
        case ConsoleKey.DownArrow:
            if(selectedIndex < (branches.Length - 1))
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

void SwitchBranch(int selectedIndex, string[] branches)
{
    Console.Clear();
    var b = branches[selectedIndex].Replace("[X] ", "").Replace("[ ] ", "");

    Process proc = new Process();

    proc.StartInfo.FileName = "git";
    proc.StartInfo.Arguments = $"checkout {b}";


    proc.StartInfo.UseShellExecute = false;
    proc.StartInfo.RedirectStandardOutput = true;

    proc.Start();

    string output = proc.StandardOutput.ReadToEnd();

    proc.WaitForExit();

    Console.WriteLine(output);
}

string[] ParseBranches(string output)
{
    var branches = output.Split('\n');
    return branches.Where(b => !String.IsNullOrEmpty(b)).ToArray();
}

int GetSelectedIndex(string[] branches)
{
    for(int i = 0; i < branches.Length; i++)
    {
        if (branches[i].StartsWith("*"))
        {
            return i;
        }
    }
    return 0;
}

void Draw(string[] branches, int selectedIndex)
{
    Console.Clear();
    var defaultColor = Console.ForegroundColor;
    for(int i = 0; i < branches.Length; i++)
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
