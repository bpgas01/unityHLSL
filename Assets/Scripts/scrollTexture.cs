using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class scrollTexture : MonoBehaviour
{
    [SerializeField] Material floorMaterial;
    [SerializeField] bool enableMovement = true;
    [SerializeField] float movementSpeed = 1;

    float scaleY = 0;
    // Update is called once per frame
    void Update()
    {
        if (enableMovement)
        {
            scaleY = movementSpeed * Time.deltaTime;
            floorMaterial.mainTextureOffset += new Vector2(0, scaleY);
        }
    }
}
