using AdventOfCode.Solutions.Models;

namespace AdventOfCode.Solutions.Year2022.Day07;

class Solution : SolutionBase
{
    public Solution() : base(07, 2022, "No Space Left On Device", false)
    {

    }

    private void Setup()
    {
        _tree = new Node<long>(0, "");
    }

    private Node<long> _tree = new Node<long>(0, "");

    protected override string SolvePartOne()
    {
        Setup();

        var lines = Input.SplitByNewline().Skip(1).ToArray(); // skip '$ cd /'
        var currentNodePath = "";
        var activeNode = _tree.TryGetNode("");
        for (var i = 0; i < lines.Count(); i++)
        {
            var line = lines[i];
            var parts = line.Split(' ');
            if (parts[0] == "$")
            {
                // command
                if (parts[1] == "ls") // we are capturing info!
                    continue;
                if (parts[1] == "cd")
                {
                    if (parts[2] == "..")
                    {
                        // pop up tree
                        currentNodePath = string.Join("/", currentNodePath.Split('/')[..^1]);
                        activeNode = _tree.TryGetNode(currentNodePath);
                        continue;
                    }
                    currentNodePath += $"/{parts[2]}";
                    activeNode = activeNode!.TryGetNode(parts[2]);
                    continue;
                }
            }
            if (parts[0] == "dir")
            {
                // add a node (that we likely haven't traversed yet)
                activeNode!.Children.Add(new Node<long>(default, parts[1]));
                continue;
            }

            var fileSize = long.Parse(parts[0]);
            var fileName = parts[1];
            activeNode.Children.Add(new Node<long>(fileSize, fileName));
        }

        // now I have my Node<long> built out!

        return "";
    }

    // to-do .. thinking what i'm doing with this.
    private long GetNodeTotalSize(Node<long> node, long totalSize = 0)
    {
        foreach (var child in node.Children)
        {
            totalSize += child.Value;
            if (child.Children.Any()) totalSize += GetNodeTotalSize(child);
        }
        return totalSize;
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}