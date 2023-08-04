using System.Collections.Generic;
using Godot;

public class AttackMove : BattleMove
{
    public Character caster;
    public List<Character> targets;

    public AttackMove(Character caster, List<Character> targets)
    {
        this.caster = caster;
        this.targets = targets;
    }

    public override void performAction()
    {
        GD.Print("Attack!");
    }

    public override bool animationFinished()
    {
        return false;
    }
}
