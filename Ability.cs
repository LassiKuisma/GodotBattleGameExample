public abstract class Ability
{
    public abstract bool isValidTarget(Character target, Character caster);

    public abstract string abilityName();

    public abstract string shortName();

    public abstract BattleMove intoMove(Character caster, List<Character> targets);
}