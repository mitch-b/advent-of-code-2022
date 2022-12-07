namespace AdventOfCode.Solutions.Year2022.Day05;

class Solution : SolutionBase
{
    public Solution() : base(05, 2022, "", true)
    {
        var inputComponents =
            Input.SplitByParagraph();
        var inputContainers = inputComponents[0].SplitByNewline();
        var inputInstructions = inputComponents[1].SplitByNewline();
        BuildContainers(inputContainers);
        BuildInstructions(inputInstructions);
    }

    private List<List<char>> _containers = new List<List<char>>();
    private List<Instruction> _instructions = new List<Instruction>();

    protected override string SolvePartOne()
    {
        
        return "";
    }

    protected override string SolvePartTwo()
    {
        return "";
    }

    private void BuildContainers(IEnumerable<string> inputContainers)
    {
        var height = inputContainers.Count(l => l[1] != '1');
        var numContainers = inputContainers.Last()
            .Split(' ').Where(c => !string.IsNullOrWhiteSpace(c))
            .Count();
        for (var i = 0; i < numContainers; i++) _containers.Add(new List<char>());
        var regexBuilder = @"(\s\s\s|\[\w\])\s?";
        foreach (var inputLine in inputContainers.Take(height))
        {
            var containers = inputLine
                .ExtractMatchesFromString<string>(regexBuilder);
            for (var i = 0; i < numContainers; i++)
            {
                if (string.IsNullOrWhiteSpace(containers.ElementAt(i)))
                    continue;
                _containers.ElementAt(i)
                    .Insert(0, containers.ElementAt(i)[1]);
            }
        }
    }

    private void BuildInstructions(IEnumerable<string> inputInstructions)
    {
        var regex = @"(\d+).*?(\d+).*?(\d+).*?";
        var parsedInstructions = inputInstructions.Select(i => i.ExtractFromString<int>(regex));
        _instructions.AddRange(parsedInstructions.Select(p => new Instruction(p.ElementAt(0), p.ElementAt(1), p.ElementAt(2))));
    }
        
}

record Instruction(int fromContainer, int take, int destinationContainer);
