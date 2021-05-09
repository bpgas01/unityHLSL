// NO LONGER USED -- KEPT FOR REFERENCE


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    [SerializeField] float coolDown = 5.0f;
    [SerializeField] int objectPoolAmount = 10;
    [SerializeField] Transform camera;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (objectPoolAmount <= 0)
        {
            objectPoolAmount = 1;
        }

        for (int i = 0; i < objectPoolAmount; i++)
        {
            GameObject temp = Instantiate(gameObject) as GameObject;
            temp.SetActive(false);
           // temp.transform.position = this.transform.position;

            spawnedObjects.Add(temp);

        }

    }

    // Update is called once per frame
    void Update()
    {
        spawnedObjects[0].gameObject.SetActive(true);
        spawnedObjects[0].transform.position -= spawnedObjects[0].transform.up * Time.deltaTime * 20;
        if (spawnedObjects[0].transform.position.z >= camera.position.z)
        {
            GameObject temp = spawnedObjects[0];
            temp.transform.position = this.transform.position;
            spawnedObjects.RemoveAt(0);
            temp.SetActive(false);
            spawnedObjects.Add(temp);

        }
    }
}
