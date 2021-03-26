using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SC_GroundGenerator : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform StartPoint;
    [SerializeField] SC_PlatformTile tilePrefab;
    [SerializeField] float movingSpeed = 12;
    [SerializeField] int tilesToPreSpawn = 15;
    [SerializeField] int tilesNoObstacles = 2;

    List<SC_PlatformTile> spawnedTiles = new List<SC_PlatformTile>();
    int nextTileToActivate = -1;

    public bool gameOver = true;
    static bool gamestarted = true;
    float score = 0;

    public static SC_GroundGenerator instance;


    // Start is called before the first frame update
    private void Start()
    {
        instance = this;
        
        Vector3 spawnPosition = StartPoint.position;
        int tilesNOobsTEMP = tilesNoObstacles;
        for (int i = 0; i < tilesToPreSpawn; i++)
        {
            spawnPosition -= tilePrefab.StartPoint.localPosition;
            SC_PlatformTile spawnedTile = Instantiate(tilePrefab, spawnPosition, Quaternion.identity) as SC_PlatformTile;
            if(tilesNOobsTEMP > 0)
            {
                spawnedTile.DeactivateAllObstacles();
                tilesNOobsTEMP--;
            }
            else
            {
                spawnedTile.ActivateRandomObstacle();
            }
            spawnPosition = spawnedTile.EndPoint.position;
            spawnedTile.transform.SetParent(transform);
            spawnedTiles.Add(spawnedTile);
        }


    }

    // Update is called once per frame
    private void Update()
    {
        if (!gameOver && gamestarted)
        {
            transform.Translate(-spawnedTiles[0].transform.forward * Time.deltaTime * (movingSpeed + score / 500), Space.World);
            score += Time.deltaTime * movingSpeed;
        }

        if (mainCamera.WorldToViewportPoint(spawnedTiles[0].EndPoint.position).z < 0)
        {
            //Move the tile to the front if it's behind the Camera
            SC_PlatformTile tileTmp = spawnedTiles[0];
            spawnedTiles.RemoveAt(0);
            tileTmp.transform.position = spawnedTiles[spawnedTiles.Count - 1].EndPoint.position - tileTmp.StartPoint.localPosition;
            tileTmp.ActivateRandomObstacle();
            spawnedTiles.Add(tileTmp);
        }



    }
   



}