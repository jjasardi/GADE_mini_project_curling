using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<Player> players = new List<Player>();
    private int currentRound = 0;
    private readonly int maxRounds = 8;

    public GameObject player1StonePrefab;
    public GameObject player2StonePrefab;
    public GameObject player1BroomPrefab;
    public GameObject player2BroomPrefab;
    private GameObject currentPlayerStone;
    private int currentPlayerIndex = 0;

    public CameraController cameraController;

    public TextMeshProUGUI scoreTextRed;
    public TextMeshProUGUI scoreTextBlue;

    private void Start()
    {
        InitializePlayers();
        StartRound();
    }

    private void InitializePlayers()
    {
        GameObject player1Broom = Instantiate(player1BroomPrefab);
        GameObject player2Broom = Instantiate(player2BroomPrefab);
        players.Add(new Player(player1Broom));
        players.Add(new Player(player2Broom));
    }

    private void StartRound()
    {
        if (currentRound <= maxRounds)
        {
            currentRound++;
            SpawnStone();
        }
        else
        {
            EndGame();
        }
    }

    private void SpawnStone()
    {
        GameObject stonePrefab = currentPlayerIndex == 0 ? player1StonePrefab : player2StonePrefab;

        currentPlayerStone = Instantiate(stonePrefab);
        players[currentPlayerIndex].AddStone(currentPlayerStone);
        cameraController.target = currentPlayerStone;
        StartCoroutine(CheckStoneStopped());
    }

    void Update()
    {
        RestartGame();
        UpdateScore();
    }

    private void RestartGame()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void UpdateScore()
    {
        int[] playerScores = new int[players.Count];
        for (int i = 0; i < playerScores.Length; i++)
        {
            playerScores[i] = players[i].GetPlayerScore();
        }

        int maxScore = 700;
        float redTextSize = Mathf.Lerp(60f, 130f, (float)playerScores[0] / maxScore);
        float blueTextSize = Mathf.Lerp(60f, 130f, (float)playerScores[1] / maxScore);

        scoreTextRed.text = "red " + playerScores[0];
        scoreTextBlue.text = "blue " + playerScores[1];

        scoreTextRed.fontSize = redTextSize;
        scoreTextBlue.fontSize = blueTextSize;
    }

    private System.Collections.IEnumerator CheckStoneStopped()
    {
        yield return new WaitUntil(() => currentPlayerStone.GetComponent<StoneThrower>().stoppedMoving);
        Invoke(nameof(NextPlayerTurn), 3);
    }

    private void NextPlayerTurn()
    {
        if (currentPlayerIndex == 0)
        {
            players[currentPlayerIndex].Broom.SetActive(false);
            currentPlayerIndex = 1;
            SpawnStone();
        } else
        {
            players[currentPlayerIndex].Broom.SetActive(false);
            currentPlayerIndex = 0;
            StartRound();
        }
    }

    private void EndGame()
    {
    }
}
