using UnityEngine;
using System.Collections;

public class ChickenMove : MonoBehaviour
{
    private float moveSpeed;


    void Update()
    {
        // Chinken move randomly
        moveSpeed = Random.Range(0.1f, 1f);
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }


}
