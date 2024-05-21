using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum ApplicationState { startingUp, titleScreen, Level1, Level2 }
    public ApplicationState currentState = ApplicationState.startingUp;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "GameManager";
                    _instance = obj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadTitleScene();
    }

    public void LoadTitleScene()
    {
        LoadScene("Scenes", "StartScreen");
        currentState = ApplicationState.titleScreen;
    }

    public void LoadLevelOne()
    {
        LoadScene("Scenes", "LevelOne");
        currentState = ApplicationState.Level1;
    }

    public void LoadLevelTwo()
    {
        LoadScene("Scenes", "LevelTwo");
        currentState = ApplicationState.Level2;
        DisableOldStones();
    }

    /// Loads a scene if it's not already loaded
    /// Only works if no two scenes are called the same.
    protected void LoadScene(string folder, string name)
    {
        bool alreadyLoaded = false;
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == name)
            {
                alreadyLoaded = true;
            }
        }

        if (!alreadyLoaded)
        {
            //Load One Scene synchronously (=blocking)
            SceneManager.LoadScene(folder + "/" + name, LoadSceneMode.Single);
        }
    }


    public List<Player> players = new List<Player>();
    public int currentRound = 0;
    public readonly int maxRounds = 8;

    public GameObject player1StonePrefab;
    public GameObject player2StonePrefab;
    public GameObject player1BroomPrefab;
    public GameObject player2BroomPrefab;
    public int currentPlayerIndex = 0;

    public int[] getPlayerScores()
    {
        int[] playerScores = new int[players.Count];
        for (int i = 0; i < playerScores.Length; i++)
        {
            playerScores[i] = players[i].GetPlayerScore();
        }
        return playerScores;
    }

    private void DisableOldStones()
    {
        foreach (Player player in players)
        {
            foreach (GameObject stone in player.stones)
            {
                if (stone.TryGetComponent<Renderer>(out var renderer))
                {
                    renderer.enabled = false;
                }

                if (stone.TryGetComponent<Collider>(out var collider))
                {
                    collider.enabled = false;
                }

                if (stone.TryGetComponent<Rigidbody>(out var rigidbody))
                {
                    rigidbody.useGravity = false;
                }
            }
        }
    }
}
