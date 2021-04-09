using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    [SerializeField] int Range;
    [SerializeField] TextMeshProUGUI AmmoText;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform despawnPoint;
    private int AmmoAmount = 50;
    private List<GameObject> pooledBullets = new List<GameObject>();
    private List<GameObject> spawnedBullets = new List<GameObject>();
         
    private void Start()
    {
        for (int i = 0; i < AmmoAmount; i++)
        {

            GameObject temp = Instantiate(bullet) as GameObject;
            temp.transform.position = transform.position;
            temp.SetActive(false);
            pooledBullets.Add(temp);


        }
    }

    // Update is called once per frame
    void Update()
    {
        string text = AmmoAmount.ToString();
        AmmoText.text = "Ammo: " + text;
        if(AmmoAmount <= 0) { AmmoAmount = 0; }

        if(spawnedBullets.Count > 0)
        {
            for (int i = 0; i < spawnedBullets.Count; i++)
            {
             spawnedBullets[i].transform.position += transform.forward * Time.deltaTime * 25;

            }

            if(spawnedBullets[0].transform.position.z <= despawnPoint.position.z)
            {
                spawnedBullets[0].SetActive(false);
                spawnedBullets.RemoveAt(0);
            }


        }

    }


    public void Shoot() 
    {
        AmmoAmount -= 1;

        GameObject temp = pooledBullets[0];
        temp.transform.position = transform.position;
        temp.SetActive(true);
        spawnedBullets.Add(temp);
        pooledBullets.RemoveAt(0);
        pooledBullets.Add(temp);

    }

}
