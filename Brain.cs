using Godot;
using System;
using System.Collections.Generic;

public partial class Brain : Node
{
    public virtual void displayMoveOptions(
        Dictionary<Ability, List<Character>> availableAbilitiesAndTargets,
        Character me
    )
    {
        me.setNextMove(new SkipAbility());
    }
}
