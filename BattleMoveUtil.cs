public static class AbilityUtil
{
    public static Ability getAbility(string name)
    {
        switch (name)
        {
            case "attack":
                return new AttackAbility();
            default:
                return null;
        }
    }
}
