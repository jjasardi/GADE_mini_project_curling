using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Transform targetObject;
    public Transform stone;
    public TextMeshProUGUI scoreText;

    void Update()
    {
        if (stone != null && targetObject != null)
        {
            float distanceToTarget = Vector3.Distance(stone.position, targetObject.position);
            int score = CalculateScore(distanceToTarget);
            scoreText.text = "score: " + score.ToString();
        }
    }

    int CalculateScore(float distance)
    {
        // Adjust the scoring function as needed
        // For example, you can use an inverse relationship, where closer distances yield higher scores
        // You can also add a maximum score limit if desired
        int maxScore = 100;
        float maxDistance = 10f; // Adjust based on your scene scale
        float minDistance = 0f; // Adjust based on your scene scale

        float normalizedDistance = Mathf.Clamp01((maxDistance - distance) / (maxDistance - minDistance));
        int score = Mathf.RoundToInt(normalizedDistance * maxScore);

        return score;
    }
}
