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


var searchTerm = args[0];
var distances = new Dictionary<string, int>();
distances.Add(branches[selectedIndex], 0);

foreach(var branch in branches.Except(new List<string> { branches.ElementAt(selectedIndex) }).Where(b => b.Length >= searchTerm.Length))
{
    distances.Add(branch, LevDistance(searchTerm.Trim().ToLower(), branch.Trim().ToLower()));
}
selectedIndex = 0;

// TODO: REMOVE
foreach(var d in distances)
{
    Console.WriteLine($"{d.Key} - {d.Value}");
}

branches = distances.Where(d => (d.Value / searchTerm.Length) < .5).OrderBy(d => d.Value).Select(o => o.Key).ToList();

// Draw loop
while (false)
{
    Draw(branches, selectedIndex);
    var input = Console.ReadKey().Key;

    switch(input)
    {
        case ConsoleKey.Enter:
            SwitchBranch(selectedIndex, branches);
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

void SwitchBranch(int selectedIndex, List<string> branches)
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

List<string> ParseBranches(string output)
{
    var branches = output.Split('\n');
    return branches.Where(b => !String.IsNullOrEmpty(b)).ToList();
}

int GetSelectedIndex(List<string> branches)
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

void Draw(List<string> branches, int selectedIndex)
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

int LevDistance(string a, string b)
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
