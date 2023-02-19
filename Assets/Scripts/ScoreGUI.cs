using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreGUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        GameManager.Instance.GameState.OnScoreBoardChanged += SetScore;
        SetScore(new[] { 0, 0 });
    }


    public void SetScore(int[] score)
    {
        text.text = $"SCORE \n X:{score[0]} | O:{score[1]}";
    }
}
