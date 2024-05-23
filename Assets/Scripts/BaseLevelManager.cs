using System.Collections;
using TMPro;
using UnityEngine;

public abstract class BaseLevelManager : MonoBehaviour
{
    protected GameManager gameManager;
    public CameraController cameraController;
    public TextMeshProUGUI scoreTextRed;
    public TextMeshProUGUI scoreTextBlue;
    public TextMeshProUGUI redStonesLeft;
    public TextMeshProUGUI blueStonesLeft;
    protected GameObject currentPlayerStone;

    public GameObject player1StonePrefab;
    public GameObject player2StonePrefab;
    public GameObject player1BroomPrefab;
    public GameObject player2BroomPrefab;

    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
    }

    protected abstract void StartRound();

    protected void SpawnStone()
    {
        GameObject stonePrefab = gameManager.currentPlayerIndex == 0 ? player1StonePrefab : player2StonePrefab;
        currentPlayerStone = Instantiate(stonePrefab);
        gameManager.players[gameManager.currentPlayerIndex].AddStone(currentPlayerStone);
        cameraController.target = currentPlayerStone;
        StartCoroutine(CheckStoneStopped());
    }

    protected void UpdateStonesLeftUI()
    {
        redStonesLeft.text = (gameManager.maxRounds - gameManager.players[0].stones.Count) + "x";
        blueStonesLeft.text = (gameManager.maxRounds - gameManager.players[1].stones.Count) + "x";
    }

    protected void UpdateScore()
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

    protected IEnumerator CheckStoneStopped()
    {
        yield return new WaitUntil(() => currentPlayerStone.GetComponent<StoneThrower>().stoppedMoving);
        UpdateStonesLeftUI();
        Invoke(nameof(NextPlayerTurn), 2.5f);
    }

    protected virtual void NextPlayerTurn()
    {
        gameManager.players[gameManager.currentPlayerIndex].Broom.SetActive(false);
        if (gameManager.currentPlayerIndex == 0)
        {
            gameManager.currentPlayerIndex = 1;
            SpawnStone();
        }
        else
        {
            gameManager.currentPlayerIndex = 0;
            StartRound();
        }
    }
}
