using System.Collections.Generic;
using Godot;

public class BossAttackAbility : Ability
{
    public override bool isValidTarget(Character target, Character caster)
    {
        return caster.team != target.team;
    }

    public override string abilityName()
    {
        return "Boss attack";
    }

    public override BattleMove intoMove(Character caster, List<Character> targets)
    {
        return new BossAttackMove(caster, targets);
    }

    public override string shortName()
    {
        return "boss_attack";
    }
}
