public static class AbilityUtil
{
    public static Ability getAbility(string name) =>
        name switch
        {
            "attack" => new AttackAbility(),
            "boss_attack" => new BossAttackAbility(),
            "heal" => new HealAbility(),
            _ => null
        };
}