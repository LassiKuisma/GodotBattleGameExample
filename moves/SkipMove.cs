public class SkipMove : BattleMove
{
    public SkipMove() { }

    public override void performAction() => GD.Print("Skipping");

    public override bool animationFinished() => true;
}
