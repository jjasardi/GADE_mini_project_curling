using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoneScore : MonoBehaviour
{
    public Vector3 targetObjectPosition;
    private int score = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        targetObjectPosition = GameObject.FindWithTag("Target").transform.position;
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, targetObjectPosition);
        score = CalculateScore(distanceToTarget);
    }

    private int CalculateScore(float distanceToTarget)
    {
        int maxScore = 100;
        float maxDistance = 10f;
        float minDistance = 0f;

        float normalizedDistance = Mathf.Clamp01((maxDistance - distanceToTarget) / (maxDistance - minDistance));
        int score = Mathf.RoundToInt(normalizedDistance * maxScore);

        return score;
    }

    public int GetScore()
    {
        return score; 
    }
}
