public partial class BattleController : Node
{
    private List<Character> characters = new();

    private List<BattleMove> moveQueue = new();
    private BattleMove currentMove = null;

    private Timer moveTimer = new Timer();
    private bool forceSkipMove = false;

    private double maxTimePerMove = 5.0;

    private BattleState state = BattleState.Idle;

    [Export]
    public float energyPerTurn = 15.0f;

    [Export]
    public Hud hud;

    private List<(Character, Label)> hpLabels = new();

    public override void _Ready()
    {
        this.characters = GetCharactersInBattle();

        moveTimer.Autostart = false;
        moveTimer.OneShot = true;
        moveTimer.WaitTime = maxTimePerMove;
        moveTimer.Timeout += timerReady;

        AddChild(moveTimer);

        createHpLabels();
        updateHpLabels();
    }

    private void createHpLabels()
    {
        foreach (var character in this.characters)
        {
            var label = this.hud.createHpDisplay(character.characterName);
            this.hpLabels.Add((character, label));
        }
    }

    private void updateHpLabels()
    {
        foreach (var (character, label) in this.hpLabels)
        {
            label.Text = character.hp.ToString();
        }
    }

    private void timerReady() => forceSkipMove = true;

    private List<Character> GetCharactersInBattle()
    {
        var children = this.GetChildren();

        var characters = new List<Character>();

        foreach (var child in children)
        {
            if (child is Character)
            {
                characters.Add((Character)child);
            }
        }

        return characters;
    }

    public override void _Process(double delta)
    {
        if (this.state == BattleState.Idle)
        {
            startNewTurn();
            promptCharactersToPickMove();
            this.state = BattleState.WaitingForMoves;
            return;
        }
        else if (this.state == BattleState.WaitingForMoves)
        {
            getNewMovesIfEveryoneReady();
        }
        else if (this.state == BattleState.ReadyToStartMove)
        {
            hideBorderAroundCharacters();
            startNextMove();
        }
        else if (this.state == BattleState.PerformingMove)
        {
            if (isCurrentMoveFinised())
            {
                startNextMove();
            }
        }
    }

    private void startNewTurn()
    {
        foreach (var character in this.characters)
        {
            character.energy += this.energyPerTurn;
        }
    }

    private void promptCharactersToPickMove()
    {
        foreach (var character in this.characters)
        {
            character.promptToPickMove(characters);
        }
    }

    private void getNewMovesIfEveryoneReady()
    {
        bool everyoneReady = characters.TrueForAll(c => c.hasNextMove());
        if (!everyoneReady)
        {
            return;
        }

        this.state = BattleState.ReadyToStartMove;
        this.moveQueue = characters.ConvertAll(character => character.getNextMove());
    }

    private void startNextMove()
    {
        if (this.moveQueue.Count == 0)
        {
            // out of moves, get next set from characters
            this.currentMove = null;
            this.state = BattleState.Idle;

            this.forceSkipMove = false;
            this.moveTimer.Stop();

            return;
        }

        this.currentMove = this.moveQueue[0];
        this.moveQueue.RemoveAt(0);

        this.currentMove.performAction();
        this.state = BattleState.PerformingMove;

        this.forceSkipMove = false;
        this.moveTimer.Start();

        updateHpLabels();
    }

    private bool isCurrentMoveFinised()
    {
        if (this.currentMove == null)
        {
            return true;
        }

        return this.forceSkipMove || currentMove.animationFinished();
    }

    private void hideBorderAroundCharacters()
    {
        foreach (var character in characters)
        {
            character.hideBorder();
        }
    }

    private enum BattleState
    {
        Idle,
        WaitingForMoves,
        ReadyToStartMove,
        PerformingMove,
    }
}
