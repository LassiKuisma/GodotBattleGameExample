using Godot;
using System;
using System.Collections.Generic;

public partial class Character : Node2D
{
    [Export]
    public int team;

    [Export]
    public string characterName;

    private BattleMove nextMove;

    [Export]
    public Brain brain;

    [Export]
    public Godot.Collections.Array<string> moveNames = new();

    private List<BattleMove> knownMoves = new();

    public bool hasNextMove()
    {
        return this.nextMove != null;
    }

    public BattleMove getNextMove()
    {
        return this.nextMove;
    }

    public void setNextMove(BattleMove move)
    {
        // TODO: show some kind of checkmark "I've picked my move"
        this.nextMove = move;
    }

    public void promptToPickMove(List<Character> characters)
    {
        // TODO: display hourglass to show this one is still deciding
        this.nextMove = null;

        var movesAndTargets = new Dictionary<BattleMove, List<Character>>();

        foreach (var move in this.knownMoves)
        {
            var targets = new List<Character>();
            foreach (var character in characters)
            {
                if (move.isValidTarget(character, this))
                {
                    targets.Add(character);
                }
            }
        }

        this.brain.displayMoveOptions(movesAndTargets, this);
    }

    public override void _Ready()
    {
        if (this.brain == null)
        {
            GD.Print("Error! Brain not found!");
        }

        foreach (var moveName in this.moveNames)
        {
            var move = BattleMoveUtil.getMove(moveName);
            if (move == null)
            {
                GD.PrintErr("Unknown move: " + moveName);
            }
            else
            {
                this.knownMoves.Add(move);
            }
        }
    }
}
