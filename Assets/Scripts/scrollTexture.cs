using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Scrolls the grid texture to give the appearance of an endlessly generated game world

[ExecuteInEditMode]
public class scrollTexture : MonoBehaviour
{
    [SerializeField] Material floorMaterial; // Floor material 
    [SerializeField] Material BuildingMaterial; // material used for each "building"
    [SerializeField] bool enableMovement = true; // if floor is moving
    [SerializeField] float movementSpeed = 1;
    private float speedMultiplier = 0;

    float scaleY = 0;
    // Update is called once per frame
    void Update()
    {
        if (enableMovement) // if floor is moving
        {
            speedMultiplier += 0.5f * Time.deltaTime; // speed up tiling by deltatime

            // Calculate how fast it needs to move
            scaleY = movementSpeed * Time.deltaTime * speedMultiplier;
            // Change texture offset by the new scale value
            floorMaterial.mainTextureOffset += new Vector2(0, scaleY);
            BuildingMaterial.mainTextureOffset += new Vector2(0, scaleY);
            if (speedMultiplier >= 10) { speedMultiplier = 10; } // change speed based on time

        }
    }
}
