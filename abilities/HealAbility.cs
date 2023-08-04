public class HealAbility : Ability
{
    public override string abilityName() => "Heal";

    public override bool isValidTarget(Character target, Character caster) =>
        target.team == caster.team && target != caster;

    public override BattleMove intoMove(Character caster, List<Character> targets)
    {
        if (targets.Count == 0)
        {
            return new SkipMove();
        }

        return new HealMove(caster, targets[0]);
    }

    public override string shortName() => "heal";
}