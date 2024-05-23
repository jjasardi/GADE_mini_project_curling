using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinnerScreen : MonoBehaviour
{
    public TextMeshProUGUI WinnerText;
    public void ShowWinnerScreen(int[] playerScores)
    {
        gameObject.SetActive(true);
        string winner;
        if (playerScores[0] > playerScores[1])
        {
            winner = "red";
            WinnerText.color = new Color32(231, 4, 2, 255);
        }
        else if (playerScores[1] > playerScores[0])
        {
            winner = "blue";
            WinnerText.color = new Color32(59, 92, 255, 255);
        }
        else
        {
            winner = "sportsmanship";
            WinnerText.color = new Color32(254, 168, 47, 255);
        }
        WinnerText.text = "winner is " + winner;
    }

    public void MainMenuButton()
    {
        GameManager.Instance.LoadTitleScene();
    }
}
