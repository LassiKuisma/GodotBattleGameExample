public partial class Character : Node2D
{
    [Export]
    public int team;

    [Export]
    public string characterName;

    private BattleMove nextMove;

    [Export]
    public Brain brain;

    [Export]
    public Godot.Collections.Array<string> abilityNames = new();

    private List<Ability> knownAbilities = new();

    private Line2D border;

    public float energy;

    [Export]
    public int maxHp = 100;

    public int hp;

    private AnimationPlayer animation;
    private bool isAnimationFinished = false;

    public bool hasNextMove()
    {
        return this.nextMove != null;
    }

    public BattleMove getNextMove()
    {
        return this.nextMove;
    }

    public void setNextMove(BattleMove move)
    {
        this.nextMove = move;
        displayGreenBorder();
    }

    public void promptToPickMove(List<Character> characters)
    {
        this.nextMove = null;
        displayRedBorder();

        var movesAndTargets = new Dictionary<string, (Ability, List<Character>)>();

        foreach (var ability in this.knownAbilities)
        {
            var targets = new List<Character>();
            foreach (var character in characters)
            {
                if (ability.isValidTarget(character, this))
                {
                    targets.Add(character);
                }
            }

            movesAndTargets.Add(ability.shortName(), (ability, targets));
        }

        if (movesAndTargets.Count == 0)
        {
            movesAndTargets.Add("skip", (new SkipAbility(), new List<Character> { this }));
        }

        this.brain.displayMoveOptions(movesAndTargets, this);
    }

    public override void _Ready()
    {
        this.hp = maxHp;

        if (this.brain == null)
        {
            GD.Print("Error! Brain not found!");
        }

        this.border = GetNode<Line2D>("Border");
        this.border.Visible = false;

        this.animation = GetNode<AnimationPlayer>("AnimationPlayer");
        this.animation.AnimationFinished += (_) =>
        {
            this.isAnimationFinished = true;
        };

        foreach (var abilityName in this.abilityNames)
        {
            var ability = AbilityUtil.getAbility(abilityName);
            if (ability == null)
            {
                GD.PrintErr("Unknown ability: " + abilityName);
            }
            else
            {
                this.knownAbilities.Add(ability);
            }
        }
    }

    private void displayRedBorder()
    {
        this.border.DefaultColor = Colors.Red;
        this.border.Visible = true;
    }

    private void displayGreenBorder()
    {
        this.border.DefaultColor = Colors.Green;
        this.border.Visible = true;
    }

    public void hideBorder()
    {
        this.border.Visible = false;
    }

    public void playAnimation(string animationName)
    {
        this.animation.Stop();
        this.isAnimationFinished = false;
        this.animation.Play(animationName);
    }

    public bool animationFinished()
    {
        return this.isAnimationFinished;
    }
}
