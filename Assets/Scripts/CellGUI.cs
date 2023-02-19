using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGUI : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;

    public Vector2Int Position { get; set; }

    public event Action<Vector2Int> OnClicked;

    internal void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    public void OnMouseDown()
    {
        OnClicked?.Invoke(Position);
    }
}
