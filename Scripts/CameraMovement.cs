using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    //Player Transform
    private Transform player;

    private Vector3 offset = Vector3.zero;

    private Vector3 animationOffset = new Vector3(0, 20, 0);

    private float transition = 0.0f;


    void Start()
    {
        //Find Player with tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //Set the destance bwteen player and camera.
        offset = transform.position - player.position;
    }


    void LateUpdate()
    {
        //Set a new Vector3 to save varible
        Vector3 moveVector = player.position + offset;

        if (transition > 1.0f)
        {
            //If transition >1, camera follow the player
            transform.position = moveVector;
        }
        else
        {
            //The camera movement starts from the height of 20 to the player in transition time
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);

            //Increase transition by the time
            transition += Time.deltaTime / 2;

            //Camera always looks at player
            transform.LookAt(player.position + Vector3.up);
        }
    }
}
