using System.Collections.Generic;
using Godot;

public class SkipAbility : Ability
{
    public override bool isValidTarget(Character target, Character caster)
    {
        return true;
    }

    public override string abilityName()
    {
        return "Skip move";
    }

    public override BattleMove intoMove(Character caster, List<Character> targets)
    {
        return new SkipMove();
    }
}
