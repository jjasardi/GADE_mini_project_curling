using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class StoneThrower : MonoBehaviour
{
    float startTime, endTime, swipeDistance, swipeTime;
    private Vector2 startPos;
    private Vector2 endPos;

    public float MinSwipDist = 0f;
    private float StoneVelocity = 0;
    private float StoneSpeed = 0;
    private Vector3 angle;

    private bool thrown;
    public bool stoppedMoving;
    private Vector3 resetPos;
    Rigidbody rb;

    // shit variable because shit bug.
    private bool isFirstCheck = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        resetPos = transform.position;
        ResetStone();
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!thrown)
            {
                startTime = Time.time;
                startPos = Input.mousePosition;
            }
        } else if (context.canceled)
        {
            endTime = Time.time;
            endPos = Input.mousePosition;
            swipeDistance = (endPos - startPos).magnitude;
            swipeTime = endTime - startTime;

            if (swipeTime < 0.5f && swipeDistance > MinSwipDist)
            {
                ThrowStone();
            }
        }
    }

    void ThrowStone()
    {
        CalSpeed();
        CalAngle();
        rb.AddForce(new Vector3((angle.x * StoneSpeed), 0, (angle.z * StoneSpeed) * 2));
        thrown = true;
    }

    void ResetStone()
    {
        angle = Vector3.zero;
        endPos = Vector2.zero;
        startPos = Vector2.zero;
        StoneSpeed = 0;
        startTime = 0;
        endTime = 0;
        swipeDistance = 0;
        swipeTime = 0;
        thrown = false;
        rb.velocity = Vector3.zero;
        transform.position = resetPos;
    }


    private void CalAngle()
    {
        angle = Camera.main.ScreenToWorldPoint(new Vector3(endPos.x, endPos.y + 50f, (Camera.main.nearClipPlane + 5)));
    }

    void CalSpeed()
    {
        if (swipeTime > 0)
            StoneVelocity = swipeDistance / (swipeDistance - swipeTime);

        StoneSpeed = StoneVelocity * 40;

        swipeTime = 0;
    }

    private void FixedUpdate()
    {
        if (thrown && rb.velocity.magnitude < 0.1f)
        {
            if (isFirstCheck)
            {
                isFirstCheck = false;
            } else
            {
                stoppedMoving = true;
            }
        }
    }
}
