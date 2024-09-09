using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;

    void Start()
    {
        //the obstacles array will automatically include all of the children of the GameObject that this script is attached to.
        //that way, if we want to add more types of obstacles in the future, all we have to do is make them children of this object.
        obstacles = new GameObject[transform.childCount];
        for(int s = 0; s < transform.childCount; s++)
        {
            obstacles[s] = transform.GetChild(s).gameObject;
        }
    }

    public void Shuffle(int amount, float boundaries)
    {
        //spawns a specified number of obstacles in random locations. The boundaries parameter ensures that they'll be at least partially inside the arena.
        for (int s = 0; s < amount; s++)
        {
            Instantiate(obstacles[Random.Range(0, obstacles.Length)], new Vector3(Random.Range(-boundaries,boundaries), Random.Range(-boundaries, boundaries), 0), transform.rotation).SetActive(true);
        }
    }
}
