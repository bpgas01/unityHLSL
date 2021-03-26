using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlatformTile : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;

    public GameObject[] obstacles;
    public GameObject[] enemies;



    public void ActivateRandomObstacle()
    {
        DeactivateAllObstacles();

        System.Random random = new System.Random();
        int randNumber = random.Next(0, obstacles.Length);
        obstacles[randNumber].SetActive(true);

    }
    public void DeactivateAllObstacles()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(false);
        }
    }
   
}
