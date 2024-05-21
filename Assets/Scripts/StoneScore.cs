using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoneScore : MonoBehaviour
{
    public Transform targetObject;
    private int score = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        targetObject = GameObject.FindWithTag("Target").transform;
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, targetObject.position);
        score = CalculateScore(distanceToTarget);
    }

    private int CalculateScore(float distanceToTarget)
    {
        int maxScore = 100;
        float maxDistance = 10f; // Adjust based on your scene scale
        float minDistance = 0f; // Adjust based on your scene scale

        float normalizedDistance = Mathf.Clamp01((maxDistance - distanceToTarget) / (maxDistance - minDistance));
        int score = Mathf.RoundToInt(normalizedDistance * maxScore);

        return score;
    }

    public int GetScore()
    {
        return score; 
    }
}
