using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    Symobl[,] grid = new Symobl[3, 3];



    public void SetSymbol(int x, int y, Symobl symbol)
    {
        grid[x, y] = symbol;
    }

    public Symobl GetSymbol(int x, int y)
    {
        return grid[x, y];
    }
}

public enum Symobl
{
    EMPTY, X, O
}
