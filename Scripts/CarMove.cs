using UnityEngine;
using System.Collections;

public class CarMove : MonoBehaviour
{
    // Set a rigidbody for car, so that it can go up the slope
    private Rigidbody rb;


    void Start()
    {
        //StartCoroutine("MoveTimer");
        rb = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        // Let car move and get gravity
        rb.velocity = new Vector3(0, -10, -14); 
    }
}
