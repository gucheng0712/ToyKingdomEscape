using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
   
    void Update()
    {
        // Make the shield rotate around the player
        transform.Rotate(0, 10, 0);
    }

    // When the shield collide with coin and diamonds, also can get points
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Coin")
        {
            Destroy(col.gameObject);
            Debug.Log("Get 10 Points");
            Globals.GetPoint(10);
        }
        if (col.gameObject.tag == "Diamond")
        {
            Destroy(col.gameObject);
            Debug.Log("Get 50 points");
            Globals.GetPoint(50);
        }
    }

}
