using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [System.Serializable]
    struct enemy
    {
        [Header("Object Settings")]
        [SerializeField]
        [Tooltip("Object name to show in inspector")]
        public string name;

        [SerializeField]
        [Tooltip("Prefab object to add to pool")]
        public GameObject prefab;

        [SerializeField]
        [Tooltip("Amount of this object to pool")]
        public int amount;

        [Header("Spawn Points - Enable first")]
        [SerializeField]
        [Tooltip("Use Spawn points")]
        public bool left;
        public bool right;
        public bool up;
        public bool down;



    }
    [Header("Spawner Settings")]
    [SerializeField]
    [Tooltip("Delay (in seconds) before the next object spawn")]
    float coolDownTime;

    [SerializeField]
    [Tooltip("Where objects are set in-active (usually behind the camera)")]
    Transform endPoint;

    [Header("Spawn Points")]
    [SerializeField] bool enabled;
    [SerializeField] Transform right;
    [SerializeField] Transform left;
    [SerializeField] Transform up;
    [SerializeField] Transform down;


    [Header("Spawnable Objects")]
    [SerializeField]
    [Tooltip("Objects for the pooler to spawn and manage")]
    List<enemy> enemies = new List<enemy>();

    List<enemy> spawned = new List<enemy>();
    List<enemy> pooled = new List<enemy>();

    private Transform defaultPosition;
    private float timer = 0;
    // Start is called before the first frame update


    Transform GetTransform(enemy enem)
    {

        if (enem.left)
        {
            return left;
        }
        if (enem.right)
        {
            return right;

        }
        if (enem.up)
        {
            return up;
        }
        if (enem.down)
        {
            return down;

        }

        return null;
    }



    void Start()
    {
      //  List<enemy> tempList = new List<enemy>();
        foreach (var enem in enemies)
        {
            Instantiate(enem.prefab);
            for (int i = 0; i < enem.amount; i++)
            {

                if (enem.left)
                {
                    enem.prefab.transform.position = left.position;
                }
                if (enem.right)
                {
                    enem.prefab.transform.position = right.position;

                }
                if (enem.up)
                {
                    enem.prefab.transform.position = up.position;
                }
                if (enem.down)
                {
                    enem.prefab.transform.position = down.position;

                }

                enem.prefab.name = enem.name;
                enem.prefab.SetActive(false);
                pooled.Add(enem);          
            } 
        }
    }
    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;

        if (timer >= coolDownTime)
        {
            enemy temp = pooled[0];
            temp.prefab.SetActive(true);
            spawned.Add(temp);
            pooled.RemoveAt(0);
            pooled.Add(temp);
            timer = 0;

        }

        if (spawned.Count > 0)
        {
            foreach (var obj in spawned)
            {
                obj.prefab.transform.position += obj.prefab.transform.forward * Time.deltaTime * 20;
            }

            Collider[] colliders = Physics.OverlapSphere(spawned[0].prefab.transform.position, 5);

            foreach (var col in colliders)
            {
                if (col.gameObject.CompareTag("EndPoint"))
                {
                    #region Check Spawn
                    if (spawned[0].left)
                    {
                        spawned[0].prefab.transform.position = left.position;
                    }
                    if (spawned[0].right)
                    {
                        spawned[0].prefab.transform.position = right.position;

                    }
                    if (spawned[0].up)
                    {
                        spawned[0].prefab.transform.position = up.position;
                    }

                    if (spawned[0].down)
                    {
                        spawned[0].prefab.transform.position = down.position;

                    }
                    #endregion

                    spawned[0].prefab.SetActive(false);
                    spawned.RemoveAt(0);

                }
            }



        }
    }

}
