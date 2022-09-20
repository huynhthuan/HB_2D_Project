using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform aPoint;

    [SerializeField]
    private Transform bPoint;

    [SerializeField]
    private float speed = 20f;

    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = aPoint.position;
        target = bPoint.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, bPoint.position) < 0.01f)
        {
            target = aPoint.position;
        }

        if (Vector2.Distance(transform.position, aPoint.position) < 0.01f)
        {
            target = bPoint.position;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.SetParent(null);
        }
    }
}
