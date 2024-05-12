using UnityEngine;
using UnityEngine.InputSystem;

public class BroomController : MonoBehaviour
{
    private float forceAmount = 50f;
    public Rigidbody stoneRigidbody;
    public Animator animator;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void Move(InputAction.CallbackContext context)
    {
        Animate(context);
        audioManager.PlaySFX(audioManager.broomSweeping);
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
        }
        else
        {
            animator.SetTrigger("RightSweepingTrigger");
        }
    }
}