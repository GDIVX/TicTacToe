using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoardController : MonoBehaviour
{
    public event Action<Vector2Int, Symobl> OnSetSymbol;

    Board board = new Board();



    public void SetSymbol(Vector2Int position, Symobl symbol)
    {
        Symobl currentSymbol = board.GetSymbol(position.x, position.y);

        if (currentSymbol != Symobl.EMPTY)
        {
            return;
        }

        board.SetSymbol(position.x, position.y, symbol);
        OnSetSymbol?.Invoke(position, symbol);
    }

    internal Symobl GetSymbol(Vector2Int cell)
    {
        return board.GetSymbol(cell.x, cell.y);
    }

    internal Symobl GetSymbol(int x, int y)
    {
        return board.GetSymbol(x, y);
    }
}
