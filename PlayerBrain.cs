using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerBrain : Brain
{
    [Export]
    public Hud hud;

    public override void displayMoveOptions(
        Dictionary<string, (Ability, List<Character>)> availableAbilitiesAndTargets,
        Character me
    )
    {
        string energy = ((int)me.energy).ToString();
        this.hud.setEnergyLabelText(energy);

        var buttons = new List<(string, string, List<(string, Action)>)>();
        foreach (var (_, (ability, targets)) in availableAbilitiesAndTargets)
        {
            if (targets.Count == 0)
            {
                continue;
            }

            var labelText = "Cast " + ability.abilityName();
            var targetButtons = targets.ConvertAll<(string, Action)>(target =>
            {
                return (
                    target.characterName,
                    () =>
                    {
                        var move = ability.intoMove(me, new List<Character> { target });
                        selectMove(me, move, target);
                    }
                );
            });
            buttons.Add((ability.abilityName(), labelText, targetButtons));
        }

        this.hud.setButtons(buttons);
    }

    private void selectMove(Character me, BattleMove move, Character target)
    {
        me.setNextMove(move);
        this.hud.clear();
    }
}
