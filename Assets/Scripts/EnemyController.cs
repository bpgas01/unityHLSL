using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [System.Serializable]
    struct enemy
    {
        public string name;
        public GameObject prefab;
        p
    }

    [SerializeField] List<enemy> enemies = new List<enemy>();

    List<GameObject> spawned = new List<GameObject>();
    List<GameObject> pooled = new List<GameObject>();


    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject temp = Instantiate(enemies[i].prefab) as GameObject;
            temp.transform.position = this.transform.position;
            temp.name = enemies[i].name;
            temp.SetActive(false);
            pooled.Add(temp);

        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;

        if (timer >= 2)
        {
            GameObject temp = pooled[0];
            temp.SetActive(true);
            spawned.Add(temp);
            pooled.RemoveAt(0);
            pooled.Add(temp);
            timer = 0;
           
        }

        foreach(var obj in spawned)
        {
            obj.transform.position += obj.transform.forward * Time.deltaTime * 20;
        }


    }
}
