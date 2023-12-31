public static class AbilityUtil
{
    public static Ability getAbility(string name)
    {
        switch (name)
        {
            case "attack":
                return new AttackAbility();
            case "boss_attack":
                return new BossAttackAbility();
            case "heal":
                return new HealAbility();
            default:
                return null;
        }
    }
}
