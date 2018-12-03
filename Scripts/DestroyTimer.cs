using UnityEngine;
using System.Collections;

public class DestroyTimer : MonoBehaviour
{

    void Update()
    {
        //Destroy stuff after 28 seconds
        Destroy(gameObject, 28f);
    }
}
