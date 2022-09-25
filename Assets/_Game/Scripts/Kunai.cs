using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject hitVfx;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        rb.velocity = transform.right * speed;
        // Auto destroy after 4s
        Invoke(nameof(OnDespawn), 4f);
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If other collider tag is Enemy
        if (other.tag == "Enemy")
        {
            // Hit character of other collider
            other.GetComponent<Character>().OnHit(30f);
            Instantiate(hitVfx, transform.position, transform.rotation);
            // Destroy this gameObject
            OnDespawn();
        }
    }
}
