using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newSpawner : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    [SerializeField] Transform despawnPoint;
    [SerializeField] int amount;
    float CoolDown = 5;
    [SerializeField] float speed;
    private List<GameObject> pooledObjects = new List<GameObject>();
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private float speedMultiplier = 0;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject temp = Instantiate(Enemy) as GameObject;
            temp.transform.position = transform.position;
            temp.SetActive(false);
            pooledObjects.Add(temp);

        }    
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;
        speedMultiplier += 0.5f * Time.deltaTime;

        if (speedMultiplier >= 3) CoolDown = 2;
        if (speedMultiplier >= 4) CoolDown = 1.5f;
        if (speedMultiplier >= 5) CoolDown = 1f;
        if (speedMultiplier >= 6) CoolDown = 0.5f;
        if (speedMultiplier >= 10) { speedMultiplier = 10; }

        Debug.Log(speedMultiplier);
        if(timer >= CoolDown)
        {
            GameObject temp = pooledObjects[0];
            temp.transform.position = transform.position;
            temp.SetActive(true);
            spawnedObjects.Add(temp);
            pooledObjects.RemoveAt(0);
            pooledObjects.Add(temp);
            timer = 0;
        }

        if (spawnedObjects.Count > 0)
        {

            for (int i = 0; i < spawnedObjects.Count; i++)
            {
                spawnedObjects[i].transform.position += transform.forward * Time.deltaTime 
                    * speed * speedMultiplier;
            }

            if(spawnedObjects[0].transform.position.z >= despawnPoint.position.z)
            {
                spawnedObjects[0].SetActive(false);
                spawnedObjects.RemoveAt(0);
            }
        }

    }
}
