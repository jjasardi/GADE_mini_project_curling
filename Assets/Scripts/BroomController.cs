using UnityEngine;
using UnityEngine.InputSystem;

public class BroomController : MonoBehaviour
{
    public float forceAmount = 10f;
    public Rigidbody stoneRigidbody;
    public Animator animator;

    public void Move(InputAction.CallbackContext context)
    {
        Animate(context);
        if (context.performed && stoneRigidbody.velocity.magnitude > 0.1f)
        {         
            Vector2 inputVector = context.ReadValue<Vector2>();
            Vector2 force = inputVector * forceAmount;
            stoneRigidbody.AddForce(force);
        }
    }

    private void Animate(InputAction.CallbackContext context)
    {
        if (context.control == Keyboard.current.leftArrowKey)
        {
            animator.SetTrigger("LeftSweepingTrigger");
        } else
        {
            animator.SetTrigger("RightSweepingTrigger");
        }
    }
}