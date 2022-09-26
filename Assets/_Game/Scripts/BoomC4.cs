using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomC4 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other && other.tag == "Enemy")
        {
            other.GetComponent<Character>().OnHit(10f);
            Destroy(gameObject);
        }
    }
}
