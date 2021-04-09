using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class scrollTexture : MonoBehaviour
{
    [SerializeField] Material floorMaterial;
    [SerializeField] Material BuildingMaterial;
    [SerializeField] bool enableMovement = true;
    [SerializeField] float movementSpeed = 1;
    private float speedMultiplier = 0;

    float scaleY = 0;
    // Update is called once per frame
    void Update()
    {
        if (enableMovement)
        {
            speedMultiplier += 0.5f * Time.deltaTime;


            scaleY = movementSpeed * Time.deltaTime * speedMultiplier;
            floorMaterial.mainTextureOffset += new Vector2(0, scaleY);
            BuildingMaterial.mainTextureOffset += new Vector2(0, scaleY);
            if (speedMultiplier >= 10) { speedMultiplier = 10; }

        }
    }
}
