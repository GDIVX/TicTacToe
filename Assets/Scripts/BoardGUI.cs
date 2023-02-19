using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGUI : MonoBehaviour
{
    [SerializeField] BoardController _controller;
    [SerializeField] GameObject _cellPrefab;
    [SerializeField] Vector2 _offset;
    [SerializeField] Sprite _xSprite, _oSprite, _emptySprite;


    CellGUI[,] _cells = new CellGUI[3, 3];

    private void Start()
    {
        _controller ??= GetComponent<BoardController>();

        _controller.OnSetSymbol += OnSetSymbol;

        //Generate the grid
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                GameObject obj = GameObject.Instantiate(_cellPrefab, new Vector3(x * _offset.x, y * _offset.y, 0), Quaternion.identity, transform);
                CellGUI cell = obj.GetComponent<CellGUI>();
                cell.Position = new(x, y);
                _cells[x, y] = cell;
                cell.OnClicked += OnCellClicked;

                OnSetSymbol(new Vector2Int(x, y), Symobl.EMPTY);
            }
        }
    }

    void OnSetSymbol(Vector2Int position, Symobl symbol)
    {
        //Get the sprite corospanding to the symbol
        Sprite sprite = symbol switch
        {
            Symobl.X => _xSprite,
            Symobl.O => _oSprite,
            Symobl.EMPTY => _emptySprite,
            _ => throw new NotImplementedException(),
        };

        //Set the sprite
        _cells[position.x, position.y].SetSprite(sprite);
    }


    public void OnCellClicked(Vector2Int position)
    {
        _controller.SetSymbolForCurrentPlayer(position);
    }
}
