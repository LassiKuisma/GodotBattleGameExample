using Godot;
using System;
using System.Collections.Generic;

public partial class BattleController : Node
{
    private List<Character> characters = new();

    private List<BattleMove> moveQueue = new();
    private BattleMove currentMove = null;

    private Timer moveTimer = new Timer();
    private bool forceSkipMove = false;

    private double maxTimePerMove = 5.0;

    private BattleState state = BattleState.Idle;

    public override void _Ready()
    {
        this.characters = GetCharactersInBattle();
        moveTimer.Autostart = false;
        moveTimer.OneShot = true;
        moveTimer.WaitTime = maxTimePerMove;
        moveTimer.Ready += timerReady;
    }

    private void timerReady()
    {
        forceSkipMove = true;
    }

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
        this.moveTimer.Start();
        this.forceSkipMove = false;

        if (this.moveQueue.Count == 0)
        {
            // out of moves, get next set from characters
            this.currentMove = null;
            this.state = BattleState.Idle;

            return;
        }

        this.currentMove = this.moveQueue[0];
        this.moveQueue.RemoveAt(0);

        this.currentMove.performAction();
        this.state = BattleState.PerformingMove;
    }

    private bool isCurrentMoveFinised()
    {
        if (this.currentMove == null)
        {
            return true;
        }

        return this.forceSkipMove || currentMove.animationFinished();
    }

    private enum BattleState
    {
        Idle,
        WaitingForMoves,
        ReadyToStartMove,
        PerformingMove,
    }
}
