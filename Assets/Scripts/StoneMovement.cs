using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMovement : MonoBehaviour
{
    public Rigidbody stoneRigidbody;
    public float maxForce = 1000f; // Adjust this value as needed
    private Vector3 startPosition;
    private Vector3 endPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = Vector3.zero;
        endPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            endPosition = Input.mousePosition;
            ThrowStone();
        }

        void ThrowStone()
        {
            Vector3 dragVector = endPosition - startPosition;
            float forceMagnitude = Mathf.Min(dragVector.magnitude, maxForce);
            Vector3 force = dragVector.normalized * forceMagnitude;

            stoneRigidbody.AddForce(force);
        }
    }
}
