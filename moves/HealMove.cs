public class HealMove : BattleMove
{
    public Character caster;
    public Character target;

    private int amount = 40;

    public HealMove(Character caster, Character target)
    {
        this.caster = caster;
        this.target = target;
    }

    public override void performAction()
    {
        caster.playAnimation("cast_heal");
        target.playAnimation("get_healed");

        target.hp += amount;
        if (target.hp > target.maxHp)
        {
            target.hp = target.maxHp;
        }
    }

    public override bool animationFinished()
    {
        return caster.animationFinished() && target.animationFinished();
    }
}
