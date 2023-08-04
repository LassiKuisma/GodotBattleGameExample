using Godot;
using System;
using System.Collections.Generic;

public partial class Character : Node2D
{
    [Export]
    public int team;

    [Export]
    public string characterName;

    private Ability nextMove;

    [Export]
    public Brain brain;

    [Export]
    public Godot.Collections.Array<string> abilityNames = new();

    private List<Ability> knownAbilities = new();

    private Line2D border;

    public float energy;

    public bool hasNextMove()
    {
        return this.nextMove != null;
    }

    public Ability getNextMove()
    {
        return this.nextMove;
    }

    public void setNextMove(Ability move)
    {
        this.nextMove = move;
        displayGreenBorder();
    }

    public void promptToPickMove(List<Character> characters)
    {
        this.nextMove = null;
        displayRedBorder();

        var movesAndTargets = new Dictionary<Ability, List<Character>>();

        foreach (var move in this.knownAbilities)
        {
            var targets = new List<Character>();
            foreach (var character in characters)
            {
                if (move.isValidTarget(character, this))
                {
                    targets.Add(character);
                }
            }

            movesAndTargets.Add(move, targets);
        }

        if (movesAndTargets.Count == 0)
        {
            movesAndTargets.Add(new SkipAbility(), new List<Character> { this });
        }

        this.brain.displayMoveOptions(movesAndTargets, this);
    }

    public override void _Ready()
    {
        if (this.brain == null)
        {
            GD.Print("Error! Brain not found!");
        }

        this.border = GetNode<Line2D>("Border");
        this.border.Visible = false;

        foreach (var abilityName in this.abilityNames)
        {
            var ability = AbilityUtil.getAbility(abilityName);
            if (ability == null)
            {
                GD.PrintErr("Unknown ability: " + abilityName);
            }
            else
            {
                this.knownAbilities.Add(ability);
            }
        }
    }

    private void displayRedBorder()
    {
        this.border.DefaultColor = Colors.Red;
        this.border.Visible = true;
    }

    private void displayGreenBorder()
    {
        this.border.DefaultColor = Colors.Green;
        this.border.Visible = true;
    }

    public void hideBorder()
    {
        this.border.Visible = false;
    }
}
