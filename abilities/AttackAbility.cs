using Godot;

public class AttackAbility : Ability
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

    public override string abilityName()
    {
        return "Basic attack";
    }
}
