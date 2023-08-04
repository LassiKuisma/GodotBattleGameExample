using Godot;

public class AttackMove : BattleMove
{
    public override void performAction()
    {
        GD.Print("Attack!");
    }

    public override bool animationFinished()
    {
        return false;
    }

    public override bool isValidTarget(Character target, Character caster)
    {
        return caster.team != target.team;
    }

    public override string moveName()
    {
        return "Basic attack";
    }
}
