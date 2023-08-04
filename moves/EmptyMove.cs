public class EmptyMove : BattleMove
{
    public override void performAction() { }

    public override bool animationFinished()
    {
        return true;
    }

    public override bool isValidTarget(Character target, Character caster)
    {
        return true;
    }

    public override string moveName()
    {
        return "Skip move";
    }
}
