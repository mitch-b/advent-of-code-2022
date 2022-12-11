namespace AdventOfCode.Solutions.Year2022.Day08;

class Solution : SolutionBase
{
    public Solution() : base(08, 2022, "Treetop Tree House", true)
    {
        SetupForest();
    }

    private void SetupForest()
    {
        var inputSize = Input.SplitByNewline()[0].Length;
        _forest = new int[inputSize, inputSize];
        var forest = Input.SplitByNewline();
        for (var i = 0; i < forest.Length; i++)
            for (var j = 0; j < forest.Length; j++)
                _forest[i, j] = int.Parse(forest[i][j].ToString());
    }

    private int[,] _forest;

    protected override string SolvePartOne()
    {
        return "";
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
