using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If collider is player
        if (other.tag == "Player")
        {
            // Set new save point
            other.GetComponent<Player>().SavePoint();
        }
    }
}
