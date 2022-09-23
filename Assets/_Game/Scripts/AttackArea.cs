using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        // Check other collider trigger tag is Player or Enemy
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            // Hit other character of collider trigger
            other.GetComponent<Character>().OnHit(30f);
        }
    }
}
