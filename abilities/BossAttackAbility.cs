public class BossAttackAbility : Ability
{
    public override bool isValidTarget(Character target, Character caster) =>
        caster.team != target.team;

    public override string abilityName() => "Boss attack";

    public override BattleMove intoMove(Character caster, List<Character> targets) =>
        new BossAttackMove(caster, targets);

    public override string shortName() => "boss_attack";
}