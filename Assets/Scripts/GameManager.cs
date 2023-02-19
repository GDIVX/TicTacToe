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

    private void Start()
    {
        board.OnSetSymbol += CheckIfGameWon;
    }

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
                if (board.GetSymbol(i, j) == Symobl.EMPTY)
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
            if (board.GetSymbol(i, 0) == player && board.GetSymbol(i, 1) == player && board.GetSymbol(i, 2) == player)
            {
                return true;
            }
        }

        // Check columns
        for (int j = 0; j < 3; j++)
        {
            if (board.GetSymbol(0, j) == player && board.GetSymbol(1, j) == player && board.GetSymbol(2, j) == player)
            {
                return true;
            }
        }

        // Check diagonals
        if (board.GetSymbol(0, 0) == player && board.GetSymbol(1, 1) == player && board.GetSymbol(2, 2) == player)
        {
            return true;
        }
        if (board.GetSymbol(2, 0) == player && board.GetSymbol(1, 1) == player && board.GetSymbol(0, 2) == player)
        {
            return true;
        }

        return false;
    }

    #region GAME_STATE

    public void NextTurn()
    {
        GameState.Turn = !GameState.Turn;
    }

    public void OnPlayerWin(int playerIndex)
    {


        int currentScore = GameState.GetScore(playerIndex);
        GameState.SetScore(currentScore + 1, playerIndex);
        PlayerWin?.Invoke(playerIndex);

    }

    public bool GetCurrentTurn()
    {
        return GameState.Turn;
    }

    #endregion


    #region GAME_STATE_EVENTS

    public void Subscribe_OnScoreBoardChanged(Action<int[]> action)
    {
        GameState.OnScoreBoardChanged += action;
    }

    public void Subscribe_OnTurnChanged(Action<bool> action)
    {
        GameState.OnTurnChanged += action;
    }

    internal void ResetGame()
    {
        board.ResetBoard();
        //set the turn to X
        GameState.Turn = true;
    }

    #endregion
}


