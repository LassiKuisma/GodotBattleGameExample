using Godot;
using System;
using System.Collections.Generic;

public partial class Brain : Node
{
    public virtual void displayMoveOptions(
        Dictionary<BattleMove, List<Character>> availableMovesAndTargets,
        Character me
    )
    {
        me.setNextMove(new EmptyMove());
    }
}
