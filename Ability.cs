public abstract class Ability
{
    public abstract bool isValidTarget(Character target, Character caster);

    public abstract void performAction();

    public abstract bool animationFinished();

    public abstract string abilityName();
}
