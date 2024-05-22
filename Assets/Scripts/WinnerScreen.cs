using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinnerScreen : MonoBehaviour
{
    public TextMeshProUGUI WinnerText;
    public void ShowWinnerScreen(string winner)
    {
        gameObject.SetActive(true);
        WinnerText.text = "winner is " + winner;
    }

    public void MainMenuButton()
    {
        GameManager.Instance.LoadTitleScene();
    }
}
