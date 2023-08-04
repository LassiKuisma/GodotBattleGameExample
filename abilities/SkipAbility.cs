public class SkipAbility : Ability
{
    public override bool isValidTarget(Character target, Character caster) => true;

    public override string abilityName() => "Skip move";

    public override BattleMove intoMove(Character caster, List<Character> targets) =>
        new SkipMove();

    public override string shortName() => "skip";
}