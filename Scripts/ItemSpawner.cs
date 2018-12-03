using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour
{
    //Set array for the objects spawn position
    public Transform[] objSpawnPos;
    // Set array for Obstacles
    public GameObject[] obstacles;
    /// <summary>
    /// The bunus.
    /// </summary>
    public GameObject bunus;
    /// <summary>
    /// The diamond.
    /// </summary>
    public GameObject Diamond;
    /// <summary>
    /// The obs prefab.
    /// </summary>
    private GameObject obsPrefab;



    // Use this for initialization
    void Start()
    {
        // traversal traversal objects in the objSpawnPos array
        for (int i = 0; i < objSpawnPos.Length; i++)
        {
            //random range between 0 and 10(because int, so 10 is not included)
            int isPalceBunus = Random.Range(0, 10);

            // When random range is 0, spawn coin
            if (isPalceBunus == 0)
            {
                CreateStuff(objSpawnPos[i].position, bunus);
            }

            // When random range is 1, spawn coin
            if (isPalceBunus == 1)
            {
                CreateStuff(objSpawnPos[i].position, Diamond);
            }

            // when random ranges are not 0 or 1, spawn obstacles
            if ((isPalceBunus != 0) && (isPalceBunus != 1))
            {
                obsPrefab = obstacles[Random.Range(0, obstacles.Length)];
                CreateStuff(objSpawnPos[i].position, obsPrefab);
            }
        }

    }



    // Instantiate stuff in three line randomly
    void CreateStuff(Vector3 pos, GameObject prefab)
    {
        int BornPos = Random.Range(0, 3);

        if (BornPos == 0)
        {
            pos += new Vector3(4, 0, 0);
        }

        if (BornPos == 1)
        {
            pos += new Vector3(0, 0, 0);
        }

        if (BornPos == 2)
        {
            pos += new Vector3(-4, 0, 0);
        }

        Instantiate(prefab, pos, Quaternion.identity);
    }




}
