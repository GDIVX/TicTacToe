using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //singlton
    public static GameManager instance;


    #region SERVICES
    [SerializeField] BoardController board;
    GameState gameState = new();

    #endregion

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
    }

    private bool CheckIfWon(Symobl player)
    {
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
        gameState.Turn = !gameState.Turn;
    }

    public void OnPlayerWin(int playerIndex)
    {
        int currentScore = gameState.GetScore(playerIndex);
        gameState.SetScore(currentScore++, playerIndex);
    }

    #endregion


    #region GAME_STATE_EVENTS

    public void Subscribe_OnScoreBoardChanged(Action<int[]> action)
    {
        gameState.OnScoreBoardChanged += action;
    }

    public void Subscribe_OnTurnChanged(Action<bool> action)
    {
        gameState.OnTurnChanged += action;
    }

    #endregion
}
