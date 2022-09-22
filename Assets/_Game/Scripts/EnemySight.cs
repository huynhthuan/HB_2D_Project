using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public Enemy enemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger enemy sight");

        if (other.tag == "Player")
        {
            enemy.setTarget(other.GetComponent<Character>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        enemy.setTarget(null);
    }
}
