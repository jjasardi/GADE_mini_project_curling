using UnityEngine;

public class LevelOneManager : BaseLevelManager
{

    protected override void Start()
    {
        base.Start();
        InitializePlayers();
        base.UpdateStonesLeftUI();
        StartRound();
    }

    private void InitializePlayers()
    {
        GameObject player1Broom = Instantiate(player1BroomPrefab);
        GameObject player2Broom = Instantiate(player2BroomPrefab);
        gameManager.players.Clear();
        gameManager.players.Add(new Player(player1Broom));
        gameManager.players.Add(new Player(player2Broom));
    }

    protected override void StartRound()
    {
        if (gameManager.currentRound < 4)
        {
            gameManager.currentRound++;
            SpawnStone();
        }
        else
        {
            GoToLevelTwo();
        }
    }

    private void GoToLevelTwo()
    {
        gameManager.LoadLevelTwo();
    }

    void Update()
    {
        UpdateScore();
    }
}
