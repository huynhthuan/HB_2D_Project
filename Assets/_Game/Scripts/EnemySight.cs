using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public Enemy enemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check other collider tag is Player
        if (other.tag == "Player")
        {
            enemy.setTarget(other.GetComponent<Character>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Target exit trigger, set target to null
            Debug.Log("Exit trigger");
            enemy.setTarget(other.GetComponent<Character>());
            enemy.setTarget(null);
        }
    }
}
