public static class BattleMoveUtil
{
    public static BattleMove getMove(string name)
    {
        switch (name)
        {
            case "attack":
                return new AttackMove();
            default:
                return null;
        }
    }
}
