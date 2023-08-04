using System.Collections.Generic;
using Godot;

public class AttackAbility : Ability
{
    public override bool isValidTarget(Character target, Character caster)
    {
        return caster.team != target.team;
    }

    public override string abilityName()
    {
        return "Basic attack";
    }

    public override BattleMove intoMove(Character caster, List<Character> targets)
    {
        return new AttackMove(caster, targets);
    }
}
