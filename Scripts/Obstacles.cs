using UnityEngine;
using System.Collections;

public class Obstacles : MonoBehaviour
{
    // when the specified obstacles collide with Shield and SwordLight, it will be destroy
    void OnTriggerEnter(Collider col)
    {
        if ((col.gameObject.tag == "Shield") || (col.gameObject.tag == "SwordLight"))
        {

            Destroy(gameObject);
        }
      
    }
}
