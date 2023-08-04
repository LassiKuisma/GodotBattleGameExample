public partial class BossBrain : Brain
{
    public override void displayMoveOptions(
        Dictionary<string, (Ability, List<Character>)> availableAbilitiesAndTargets,
        Character me
    )
    {
        if (availableAbilitiesAndTargets.ContainsKey("boss_attack"))
        {
            var (attackAbility, targets) = availableAbilitiesAndTargets["boss_attack"];
            me.setNextMove(attackAbility.intoMove(me, targets));
            return;
        }

        me.setNextMove(new SkipMove());
    }
}