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
        // as a fallback to prevent deadlock, skip move as default
        me.setNextMove(new SkipMove());
    }
}
