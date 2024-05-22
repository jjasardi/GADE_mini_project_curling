using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTwoManager : MonoBehaviour
{
    GameManager gameManager;

    public GameObject player1StonePrefab;
    public GameObject player2StonePrefab;
    public GameObject player1BroomPrefab;
    public GameObject player2BroomPrefab;
    private GameObject currentPlayerStone;

    public CameraController cameraController;

    public TextMeshProUGUI scoreTextRed;
    public TextMeshProUGUI scoreTextBlue;
    public TextMeshProUGUI redStonesLeft;
    public TextMeshProUGUI blueStonesLeft;

    public WinnerScreen winnerScreen;

    private void Start()
    {
        gameManager = GameManager.Instance;
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        UpdateStonesLeftUI();
        StartRound();
    }

    private void StartRound()
    {
        Debug.Log(gameManager.currentRound);
        if (gameManager.currentRound <= gameManager.maxRounds)
        {
            SpawnStone();
        }
        else
        {
            EndGame();
        }
        gameManager.currentRound++;
    }

    private void SpawnStone()
    {
        GameObject stonePrefab = gameManager.currentPlayerIndex == 0 ? player1StonePrefab : player2StonePrefab;

        currentPlayerStone = Instantiate(stonePrefab);
        gameManager.players[gameManager.currentPlayerIndex].AddStone(currentPlayerStone);
        cameraController.target = currentPlayerStone;
        StartCoroutine(CheckStoneStopped());
    }

    void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        int[] playerScores = gameManager.getPlayerScores();

        int maxScore = 700;
        float redTextSize = Mathf.Lerp(60f, 130f, (float)playerScores[0] / maxScore);
        float blueTextSize = Mathf.Lerp(60f, 130f, (float)playerScores[1] / maxScore);

        scoreTextRed.text = "red " + playerScores[0];
        scoreTextBlue.text = "blue " + playerScores[1];

        scoreTextRed.fontSize = redTextSize;
        scoreTextBlue.fontSize = blueTextSize;
    }

    private void UpdateStonesLeftUI()
    {
        redStonesLeft.text = gameManager.maxRounds - gameManager.players[0].stones.Count + "x";
        blueStonesLeft.text = gameManager.maxRounds - gameManager.players[1].stones.Count + "x";

    }

    private System.Collections.IEnumerator CheckStoneStopped()
    {
        yield return new WaitUntil(() => currentPlayerStone.GetComponent<StoneThrower>().stoppedMoving);
        UpdateStonesLeftUI();
        Invoke(nameof(NextPlayerTurn), 3);
    }

    private void NextPlayerTurn()
    {
        if (gameManager.currentPlayerIndex == 0)
        {
            gameManager.players[gameManager.currentPlayerIndex].Broom.SetActive(false);
            gameManager.currentPlayerIndex = 1;
            SpawnStone();
        }
        else
        {
            gameManager.players[gameManager.currentPlayerIndex].Broom.SetActive(false);
            gameManager.currentPlayerIndex = 0;
            StartRound();
        }
    }

    private void EndGame()
    {
        int[] playerScores = gameManager.getPlayerScores();
        if (playerScores[0] > playerScores[1])
        {
            winnerScreen.ShowWinnerScreen("red");
        } else if (playerScores[1] > playerScores[0])
        {
            winnerScreen.ShowWinnerScreen("blue");
        } else
        {
            winnerScreen.ShowWinnerScreen("sportsmanship");
        }
    }
}
