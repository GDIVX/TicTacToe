using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //singlton
    public static GameManager Instance;


    public event Action<int> PlayerWin;

    #region SERVICES
    [SerializeField] BoardController board;
    GameState gameState = new();

    public GameState GameState
    {
        get => gameState; private set
        {
            gameState = value;
        }
    }

    public BoardController Board => board;

    #endregion

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void OnPlayerWin(int playerIndex)
    {

        int currentScore = GameState.GetScore(playerIndex);
        GameState.SetScore(currentScore + 1, playerIndex);
        PlayerWin?.Invoke(playerIndex);

    }

    #region VICTORY_CONDITIONS

    private void CheckIfGameWon(Vector2Int cell, Symobl symobl)
    {
        //if the symbol is empty return
        if (symobl == Symobl.EMPTY)
        {
            return;
        }

        if (CheckIfWon(symobl))
        {
            if (symobl == Symobl.X)
            {
                OnPlayerWin(0);
            }
            else
            {
                OnPlayerWin(1);
            }
        }


        if (CheckIfDraw())
        {
            OnPlayerWin(2);
        }
    }

    private bool CheckIfDraw()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Board.GetSymbol(i, j) == Symobl.EMPTY)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool CheckIfWon(Symobl player)
    {
        //if the symbol is empty return
        if (player == Symobl.EMPTY)
        {
            return false;
        }

        // Check rows
        for (int i = 0; i < 3; i++)
        {
            if (Board.GetSymbol(i, 0) == player && Board.GetSymbol(i, 1) == player && Board.GetSymbol(i, 2) == player)
            {
                return true;
            }
        }

        // Check columns
        for (int j = 0; j < 3; j++)
        {
            if (Board.GetSymbol(0, j) == player && Board.GetSymbol(1, j) == player && Board.GetSymbol(2, j) == player)
            {
                return true;
            }
        }

        // Check diagonals
        if (Board.GetSymbol(0, 0) == player && Board.GetSymbol(1, 1) == player && Board.GetSymbol(2, 2) == player)
        {
            return true;
        }
        if (Board.GetSymbol(2, 0) == player && Board.GetSymbol(1, 1) == player && Board.GetSymbol(0, 2) == player)
        {
            return true;
        }

        return false;
    }
    #endregion

    #region GAME_STATE

    public void NextTurn()
    {
        GameState.IsXTurn = !GameState.IsXTurn;
    }


    public bool GetCurrentTurn()
    {
        return GameState.IsXTurn;
    }
    internal void ResetGame()
    {
        Board.ResetBoard();
        //set the turn to X
        GameState.IsXTurn = true;
    }

    void SaveGameState(Vector2Int lastMove, Symobl symbol)
    {

        Command command = new(symbol == Symobl.X, lastMove);
    }

    public void Undo()
    {
        Command.Undo();
    }

    public void Redo()
    {
        Command.Redo();
    }

    internal void OnSetSymbol(Vector2Int position, Symobl symbol)
    {
        CheckIfGameWon(position, symbol);
        SaveGameState(position, symbol);

    }

    #endregion


}


