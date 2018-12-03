using UnityEngine;
using System.Collections;

public class WayPoint : MonoBehaviour
{

    // When the Way point is collide with player, Transport to a new vector3 (0,10,6);
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("TP");
            col.transform.position += new Vector3(0, 10, 6);
        }
    }

}
