using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class EnemyController : MonoBehaviour
{
    // Enemy Struct to store infomation
    #region Data Entry Struct 
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

        public Enemy Enemy;

    }
    #endregion
    // Variables for inspector to modify spawn settings
    #region Inspector Settings
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

    #endregion
    List<Enemy> spawned = new List<Enemy>();
    List<Enemy> pooled = new List<Enemy>();

    List<Enemy> EnemiesObjects = new List<Enemy>();

    private Transform defaultPosition;
    private float timer = 0;
    // Start is called before the first frame update
    
    // Initilise settings
    void Start()
    {
      foreach (var enem in enemies)
        {
            for (int i = 0; i < enem.amount; i++)
            {

            Enemy tempEnemy = enem.Enemy;
            GameObject gameObject = Instantiate(enem.prefab) as GameObject;
            gameObject.SetActive(false);
            tempEnemy.SetGameobject(gameObject);
            tempEnemy.SetAmount(enemies.Count);
            tempEnemy.SetName(enem.name);
            tempEnemy.SetSpawnPosition(false,false,true);
            pooled.Add(tempEnemy);
            }
        }
      
      
    }

   

    private void Update()
    {
        timer += 1 * Time.deltaTime;

        if (timer >= coolDownTime)
        {
            // spawn new enemy from object pool
            Enemy temp = pooled[0];
            temp.GetGameObject().SetActive(true);
            spawned.Add(temp);
            pooled.RemoveAt(0);
            pooled.Add(temp);
            timer = 0;
        }


        if(spawned.Count > 0)
        {
            foreach (var a_object in spawned)
            {
                // move spawned enemies
                a_object.GetGameObject().transform.position += 
                    a_object.GetGameObject().transform.forward 
                    * Time.deltaTime * 15;
                
            }


            Collider[] colliders = Physics.OverlapSphere(spawned[0].GetGameObject().transform.position, 5);

            foreach(var col in colliders)
            {
                // Check for despawn point
                if (col.gameObject.CompareTag("EndPoint"))
                {
                    #region Check Spawn Location
                    // Remove from spawned list and return to object pool
                    // Check Spawn location of each enemy
                    if (spawned[0].GetSpawnPos() == "left") spawned[0].GetGameObject().transform.position = left.position;
                    if(spawned[0].GetSpawnPos() == "right") spawned[0].GetGameObject().transform.position = right.position;
                    if (spawned[0].GetSpawnPos() == "up") spawned[0].GetGameObject().transform.position = up.position;
                    if (spawned[0].GetSpawnPos() == "down") spawned[0].GetGameObject().transform.position = down.position;

                    #endregion
                    spawned[0].gameObject.SetActive(false);
                    spawned.RemoveAt(0); 
                }
            }
        }

    }


   

}
