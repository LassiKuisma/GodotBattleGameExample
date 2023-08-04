public class AttackAbility : Ability
{
    public override bool isValidTarget(Character target, Character caster) => 
        caster.team != target.team;

    public override string abilityName() => "Basic attack";

    public override BattleMove intoMove(Character caster, List<Character> targets)
    {
        if (targets.Count == 0)
        {
            return new SkipMove();
        }

        return new AttackMove(caster, targets[0]);
    }

    public override string shortName() => "attack";
}
