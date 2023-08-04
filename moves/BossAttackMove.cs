public class BossAttackMove : BattleMove
{
    public Character caster;
    private List<Character> targets;

    private int damage = 35;

    public BossAttackMove(Character caster, List<Character> targets)
    {
        this.caster = caster;
        this.targets = targets;
    }

    public override void performAction()
    {
        caster.playAnimation("attack_animation");
        foreach (var target in targets)
        {
            target.playAnimation("take_damage");
            target.hp -= damage;
        }
    }

    public override bool animationFinished()
    {
        if (!caster.animationFinished())
        {
            return false;
        }
        foreach (var target in targets)
        {
            if (!target.animationFinished())
            {
                return false;
            }
        }

        return true;
    }
}