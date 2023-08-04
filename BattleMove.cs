public abstract class BattleMove
{
    public abstract bool isValidTarget(Character target, Character caster);

    public abstract void performAction();

    public abstract bool animationFinished();

    public abstract string moveName();
}
