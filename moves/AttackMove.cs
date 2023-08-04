using System.Collections.Generic;
using Godot;

public class AttackMove : BattleMove
{
    public Character caster;
    public Character target;

    public AttackMove(Character caster, Character target)
    {
        this.caster = caster;
        this.target = target;
    }

    public override void performAction()
    {
        GD.Print("Attack!");
        caster.playAnimation("attack_animation");
        target.playAnimation("take_damage");
    }

    public override bool animationFinished()
    {
        return caster.animationFinished() && target.animationFinished();
    }
}
