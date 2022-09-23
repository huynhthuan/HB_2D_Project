using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public Enemy enemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger enemy sight");

        // Check other collider tag is Player
        if (other.tag == "Player")
        {
            enemy.setTarget(other.GetComponent<Character>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Target exit trigger, set target to null
        enemy.setTarget(null);
    }
}
