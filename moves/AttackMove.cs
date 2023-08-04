public class AttackMove : BattleMove
{
    public Character caster;
    public Character target;

    private int damage = 50;

    public AttackMove(Character caster, Character target)
    {
        this.caster = caster;
        this.target = target;
    }

    public override void performAction()
    {
        caster.playAnimation("attack_animation");
        target.playAnimation("take_damage");
        target.hp -= damage;
    }

    public override bool animationFinished() =>
        caster.animationFinished() && target.animationFinished();
}