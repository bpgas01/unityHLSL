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
        public int amount;
    }

    public float coolDownTime;
    public Transform endPoint;
    [SerializeField] List<enemy> enemies = new List<enemy>();

    List<GameObject> spawned = new List<GameObject>();
    List<GameObject> pooled = new List<GameObject>();


    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

        foreach (var enem in enemies)
        {
            for (int i = 0; i < enem.amount; i++)
            {
                GameObject temp = Instantiate(enem.prefab) as GameObject;
                temp.transform.position = transform.position;
                temp.name = enem.name;
                temp.SetActive(false);
                pooled.Add(temp);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;
        
        if (timer >= coolDownTime)
        {
            GameObject temp = pooled[0];
            temp.SetActive(true);
            spawned.Add(temp);
            pooled.RemoveAt(0);
            pooled.Add(temp);
            timer = 0;
           
        }

        if (spawned.Count > 0)
        {
            foreach (var obj in spawned)
            {
                obj.transform.position += obj.transform.forward * Time.deltaTime * 20;
            }

            if (spawned[0].transform.position.z >= endPoint.position.z)
            {
                spawned[0].transform.position = transform.position;
                spawned[0].SetActive(false);
                spawned.RemoveAt(0);
            }
        }
    }
}
