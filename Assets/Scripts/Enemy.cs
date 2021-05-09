using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Private Variables
    private string m_name;
    private GameObject m_gameobject;
    private int m_amount;
    private bool m_left;
    private bool m_right;
    private bool m_up;
    private bool m_down;

    // Public functions for spawner
    public void SetName(string a_name) { m_name = a_name; }
    public void SetGameobject(GameObject a_gameObject) { m_gameobject = a_gameObject; }
    public void SetAmount(int a_amount) { m_amount = a_amount; }
    public void SetSpawnPosition(bool left = false, bool right = false,
        bool up = false, bool down = false)
    {
        if (left) m_left = true;
        if (right) m_right = true;
        if (up) m_up = true;
        if (down) m_down = true;

    }
    // Public getters for spawner
    public GameObject GetGameObject() { return m_gameobject; }
    public int EnemyCount() { return m_amount; }
    public string GetName() { return m_name; }
    // Spawn location of each enemy
    public string GetSpawnPos()
    {
        if (m_left) return "left";
        if (m_right) return "right";
        if (m_up) return "up";
        if (m_down) return "down";
        return null;
    }
    // Updates once per frame
    private void Update()
    {
        // Check if collided with bullet using overlapping spheres
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 1.5f);

        foreach (var col in colliders)
        {
            if (col.gameObject.CompareTag("Bullet"))
            {
                gameObject.SetActive(false);
                col.gameObject.SetActive(false);
            }
        }

    }

}
