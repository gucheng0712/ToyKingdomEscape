using UnityEngine;
using System.Collections;

public class SheepMove : MonoBehaviour
{
    private float moveSpeed;


    // Update is called once per frame
    void Update()
    {
        //Set the sheep movespeed randomly between 0.1f to 1f
        moveSpeed = Random.Range(0.1f, 1f);

        //Move right
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
