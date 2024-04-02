using UnityEngine;

public class BroomingController : MonoBehaviour
{
    public float forceAmount = 0.1f;
    public Rigidbody stoneRigidbody;
    private bool leftArrowPressed = false;
    private bool rightArrowPressed = false;

    void Start()
    {
        stoneRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Detect arrow key presses
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            leftArrowPressed = true;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            rightArrowPressed = true;
            

        // Apply force based on arrow key presses
        if (leftArrowPressed && stoneRigidbody.velocity.magnitude > 0.1f)
        {
            Vector3 broomForce = Vector3.left * forceAmount;
            stoneRigidbody.AddForce(broomForce);
            leftArrowPressed = false; // Reset for next frame
        }
        if (rightArrowPressed && stoneRigidbody.velocity.magnitude > 0.1f)
        {
            Vector3 broomForce = Vector3.right * forceAmount;
            stoneRigidbody.AddForce(broomForce);
            rightArrowPressed = false; // Reset for next frame
        }
    }
}