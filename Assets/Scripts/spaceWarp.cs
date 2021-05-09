using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceWarp : MonoBehaviour
{

    public Material Warp;

    private float amount = 0.1f;

    private void Update()
    {

        if(Input.GetKey(KeyCode.UpArrow))
        {
            Warp.SetFloat("strength", amount+0.1f);
        }    
    }
}
