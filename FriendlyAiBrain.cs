public partial class FriendlyAiBrain : Brain
{
    private float healWhenHpBelowPercent = 0.5f;

    public override void displayMoveOptions(
        Dictionary<string, (Ability, List<Character>)> availableAbilitiesAndTargets,
        Character me
    )
    {
        // TODO: maybe rework so that these are just set to a variable
        // and I go through the options in process
        if (availableAbilitiesAndTargets.ContainsKey("heal"))
        {
            var (healAbility, targets) = availableAbilitiesAndTargets["heal"];
            foreach (var target in targets)
            {
                var hpPercent = (float)target.hp / (float)target.maxHp;
                if (hpPercent <= healWhenHpBelowPercent)
                {
                    me.setNextMove(healAbility.intoMove(me, new List<Character> { target }));
                    return;
                }
            }
        }

        // nobody needed heal

        if (availableAbilitiesAndTargets.ContainsKey("attack"))
        {
            var (attackAbility, targets) = availableAbilitiesAndTargets["attack"];
            if (targets.Count != 0)
            {
                // pick first, could also choose lowest hp, or at random
                var target = targets[0];
                me.setNextMove(attackAbility.intoMove(me, new List<Character> { target }));
                return;
            }
        }

        me.setNextMove(new SkipMove());
    }
}