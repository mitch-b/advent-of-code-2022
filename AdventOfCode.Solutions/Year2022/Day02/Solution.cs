namespace AdventOfCode.Solutions.Year2022.Day02;

class Solution : SolutionBase
{
    public Solution() : base(02, 2022, "")
    {
        rounds = Input.SplitByNewline()
            .Select(instructions => new Round(ParseMove(instructions[0]), ParseMove(instructions[2])));
    }

    private IEnumerable<Round> rounds;

    protected override string SolvePartOne()
    {
        return rounds.Sum(r => GetScore(r)).ToString();
    }

    protected override string SolvePartTwo()
    {
        return rounds.Sum(r => GetScore(r, partTwo: true)).ToString();
    }

    static Move ParseMove(char move) => move switch
    {
        _ when move is 'A' || move is 'X' => Move.Rock,
        _ when move is 'B' || move is 'Y' => Move.Paper,
        _ when move is 'C' || move is 'Z' => Move.Scissors,
        _ => throw new ArgumentException()
    };

    static int GetScore(Round round, bool partTwo = false)
    {
        if (partTwo)
        {
            round.Me = UpdateMove(round);
        }
        return (IWon(round) ? 6 : (Tied(round) ? 3 : 0)) + (round.Me switch
        {
            Move.Rock => 1,
            Move.Paper => 2,
            Move.Scissors => 3
        });
    }

    static bool IWon(Round round) =>
           round.Opponent == Move.Rock && round.Me == Move.Paper
        || round.Opponent == Move.Paper && round.Me == Move.Scissors
        || round.Opponent == Move.Scissors && round.Me == Move.Rock;

    static bool Tied(Round round) => round.Opponent == round.Me;

    static Move GetWinMove(Move opponent) => opponent switch
    {
        Move.Rock => Move.Paper,
        Move.Paper => Move.Scissors,
        Move.Scissors => Move.Rock
    };
    static Move GetLoseMove(Move opponent) => opponent switch
    {
        Move.Rock => Move.Scissors,
        Move.Paper => Move.Rock,
        Move.Scissors => Move.Paper
    };

    static Move UpdateMove(Round round) => round switch
    {
        _ when round.Me == Move.Rock => GetLoseMove(round.Opponent), // lose
        _ when round.Me == Move.Paper => round.Opponent, // tie
        _ when round.Me == Move.Scissors => GetWinMove(round.Opponent)
    };
}


class Round
{
    public Move Opponent { get; set; }
    public Move Me { get; set; }
    public Round(Move opponent, Move me)
    {
        Opponent = opponent;
        Me = me;
    }
}

enum Move
{
    Rock, Paper, Scissors
}