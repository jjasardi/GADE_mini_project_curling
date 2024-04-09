using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StoneThrower : MonoBehaviour
{

    float startTime, endTime, swipeDistance, swipeTime;
    private Vector2 startPos;
    private Vector2 endPos;

    public float MinSwipDist = 0;
    private float StoneVelocity = 0;
    private float StoneSpeed = 0;
    //public float MaxStoneSpeed = 200;
    private Vector3 angle;

    private bool thrown, holding;
    private Vector3 newPosition, resetPos;
    Rigidbody rb;

    // Start is called before the first frame update
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
                holding = true;
            }
        } else if (context.canceled)
        {
            endTime = Time.time;
            endPos = Input.mousePosition;
            swipeDistance = (endPos - startPos).magnitude;
            swipeTime = endTime - startTime;

            if (swipeTime < 0.5f && swipeDistance > 30f)
            {
                throwStone();
            }
        }
    }

    //private void OnMouseDown()
    //{
    //    if (!thrown)
    //    {
    //        startTime = Time.time;
    //        startPos = Input.mousePosition;
    //        holding = true;
    //    }
    //}

    //private void OnMouseDrag()
    //{
    //    //PickupStone();
    //}

    //private void OnMouseUp()
    //{
    //    endTime = Time.time;
    //    endPos = Input.mousePosition;
    //    swipeDistance = (endPos - startPos).magnitude;
    //    swipeTime = endTime - startTime;

    //    if (swipeTime < 0.5f && swipeDistance > 30f)
    //    {
    //        throwStone();            
    //    }
    //}

    void throwStone()
    {
        CalSpeed();
        CalAngle();
        rb.AddForce(new Vector3((angle.x * StoneSpeed), 0, (angle.z * StoneSpeed) * 2));
        holding = false;
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
        thrown = holding = false;
        rb.velocity = Vector3.zero;
        transform.position = resetPos;
    }

    void PickupStone()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 2.57f; // You might need to adjust this value based on your scene setup
        newPosition = Camera.main.ScreenToWorldPoint(mousePos);

        transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition, 80f * Time.deltaTime);
    }

    private void Update()
    {
        //if (holding)
        //{
        //    PickupStone();
        //}
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
}
