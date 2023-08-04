using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerBrain : Brain
{
    [Export]
    public Hud hud;

    public override void displayMoveOptions(
        Dictionary<Ability, List<Character>> availableAbilitiesAndTargets,
        Character me
    )
    {
        string energy = ((int)me.energy).ToString();
        this.hud.setEnergyLabelText(energy);

        var buttons = new List<(string, string, List<(string, Action)>)>();
        foreach (var entry in availableAbilitiesAndTargets)
        {
            var ability = entry.Key;
            var targets = entry.Value;
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
                        selectMove(me, ability, target);
                    }
                );
            });
            buttons.Add((ability.abilityName(), labelText, targetButtons));
        }

        this.hud.setButtons(buttons);
    }

    private void selectMove(Character me, Ability ability, Character target)
    {
        GD.Print("Casting " + ability.abilityName() + " on " + target.characterName);
        me.setNextMove(ability);
        this.hud.clear();
    }
}
