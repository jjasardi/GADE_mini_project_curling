using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public List<GameObject> stones;
    public GameObject Broom;

    public Player(GameObject broom)
    {
        stones = new List<GameObject>();
        Broom = broom;
        broom.SetActive(false);
    }

    public void AddStone(GameObject stone)
    {
        stones.Add(stone);
        Broom.GetComponent<BroomFollow>().playerStone = stone.transform;
        Broom.GetComponent<BroomController>().stoneRigidbody = stone.GetComponent<Rigidbody>();
        Broom.SetActive(true);
    }

    public int GetPlayerScore()
    {
        int playerScore = 0;
        foreach (GameObject stone in stones)
        {
            playerScore += stone.GetComponent<StoneScore>().GetScore();
        }
        return playerScore;
    }
}
