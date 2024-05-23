using TMPro;
using UnityEngine;

public class LevelTwoManager : BaseLevelManager
{
    public WinnerScreen winnerScreen;

    protected override void Start()
    {
        base.Start();
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        base.UpdateStonesLeftUI();
        StartRound();
    }

    protected override void StartRound()
    {
        if (gameManager.currentRound < gameManager.maxRounds)
        {
            gameManager.currentRound++;
            SpawnStone();
        }
        else
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        int[] playerScores = gameManager.getPlayerScores();
        winnerScreen.ShowWinnerScreen(playerScores);
    }

    void Update()
    {
        UpdateScore();
    }
}
