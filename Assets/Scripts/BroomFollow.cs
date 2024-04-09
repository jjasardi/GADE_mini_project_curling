using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BroomFollow : MonoBehaviour
{
    public Transform playerStone;
    public float xOffset, yOffset, zOffset;

    void Update()
    {
        transform.position = playerStone.position + new Vector3(xOffset, yOffset, zOffset);
    }
}
