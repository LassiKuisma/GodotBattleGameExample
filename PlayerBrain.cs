using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerBrain : Brain
{
    [Export]
    public Hud hud;

    public override void displayMoveOptions(
        Dictionary<BattleMove, List<Character>> availableMovesAndTargets,
        Character me
    )
    {
        var buttons = new List<(string, string, List<(string, Action)>)>();
        foreach (var entry in availableMovesAndTargets)
        {
            var move = entry.Key;
            var targets = entry.Value;
            if (targets.Count == 0)
            {
                continue;
            }

            var labelText = "Cast " + move.moveName();
            var targetButtons = targets.ConvertAll<(string, Action)>(target =>
            {
                return (
                    target.characterName,
                    () =>
                    {
                        selectMove(move, target);
                    }
                );
            });
            buttons.Add((move.moveName(), labelText, targetButtons));
        }

        this.hud.setButtons(buttons);
    }

    private void selectMove(BattleMove move, Character target)
    {
        GD.Print("Casting " + move.moveName() + " on " + target.characterName);
        this.hud.clear();
    }
}
