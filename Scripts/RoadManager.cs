using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class RoadManager : MonoBehaviour
{
    /// <summary>
    /// The road array.
    /// </summary>
    public GameObject[] roadArr;

    /// <summary>
    /// The active roads list.
    /// </summary>
    private List<GameObject> activeRoads = new List<GameObject>();

    /// <summary>
    /// The player.
    /// </summary>
    private Transform player;

    /// <summary>
    /// The spawn z.
    /// </summary>
    private float spawnZ = 0;

    /// <summary>
    /// The road lenth.
    /// </summary>
    private float roadLenth = 88.0f;

    /// <summary>
    /// The last index of the road.
    /// </summary>
    private int lastRoadIndex = 0;

    /// <summary>
    /// The road on screen.
    /// </summary>
    private int roadOnScreen = 3;


    void Start()
    {
        // Find player with tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // traversal the roads when game start
        for (int i = 0; i < roadOnScreen; i++)
        {
            if (i == 0)
            {
                SpawnRoads(0);
            }
            else
            {
                SpawnRoads();
            }
        }
    }


    void Update()
    {
        // Spawn and destroy road when player arrive certain position.
        if ((player.position.z - 90) > (spawnZ - roadOnScreen * roadLenth))
        {
            SpawnRoads();
            DestroyRoad();
        }
	
    }


    // Parametric variable is for the start function, when travelsal the roads, it always spawn the first roads at the beginning
    // This function is for spawn roads
    private void SpawnRoads(int prefabIndex = -1)
    {
        GameObject go;

        //Randomly spawn roads if the array number does not use prefabIndex parametric
        if (prefabIndex == -1)
        {
            go = Instantiate(roadArr[RandomIndex()])as GameObject;
        }
        else
        {
            go = Instantiate(roadArr[prefabIndex])as GameObject;
        }

        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += roadLenth;

        // Add the current roads to the activeroads list
        activeRoads.Add(go);
    }


    // Destroy roads the first index, and remove it from the active roads list, when it is called
    private void DestroyRoad()
    {
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }


    // A function that ramdom the road index, and let the same kind of road not appear continuely
    private int RandomIndex()
    {
        int roadIndex = lastRoadIndex;

        while (roadIndex == lastRoadIndex)
        {
            roadIndex = Random.Range(0, roadArr.Length);
        }

        lastRoadIndex = roadIndex;
        return roadIndex;
    }
}
