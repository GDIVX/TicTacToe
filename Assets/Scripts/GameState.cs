using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameState
{
    private int[] _scoreBoard = new int[2];
    private bool _turn = true;


    public event Action<int[]> OnScoreBoardChanged;
    public event Action<bool> OnTurnChanged;
    public bool Turn
    {
        get => _turn;

        set
        {
            _turn = value;
            OnTurnChanged?.Invoke(_turn);
        }
    }

    public int GetScore(int index)
    {
        if (index < 0 || index >= _scoreBoard.Length) return default;
        return _scoreBoard[index];
    }

    public void SetScore(int score, int index)
    {
        if (index < 0 || index >= _scoreBoard.Length) return;

        _scoreBoard[index] = score;

        OnScoreBoardChanged?.Invoke(_scoreBoard);
    }
}
