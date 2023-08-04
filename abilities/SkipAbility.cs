using Godot;

public class SkipAbility : Ability
{
    public override void performAction()
    {
        GD.Print("Skipping");
    }

    public override bool animationFinished()
    {
        return true;
    }

    public override bool isValidTarget(Character target, Character caster)
    {
        return true;
    }

    public override string abilityName()
    {
        return "Skip move";
    }
}
