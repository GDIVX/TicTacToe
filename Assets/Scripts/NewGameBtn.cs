using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewGameBtn : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Button button;

    private void Start()
    {
        GameManager.Instance.PlayerWin += SetText;
        button.onClick.AddListener(OnClick);
        gameObject.SetActive(false);
    }

    void SetText(int playerIndex)
    {
        text.text = playerIndex switch
        {
            0 => "X WON",
            1 => "O WON",
            _ => throw new System.NotImplementedException(),
        };

        gameObject.SetActive(true);

    }

    public void OnClick()
    {
        GameManager.Instance.ResetGame();
        gameObject.SetActive(false);

    }
}
