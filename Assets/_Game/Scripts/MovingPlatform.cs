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
        // If near b point
        if (Vector2.Distance(transform.position, bPoint.position) < 0.01f)
        {
            // Change target to a point
            target = aPoint.position;
        }

        // If near a point
        if (Vector2.Distance(transform.position, aPoint.position) < 0.01f)
        {
            // Change target to b point
            target = bPoint.position;
        }

        // Move to target point
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // If tag equal Player
        if (other.gameObject.tag == "Player")
        {
            // Set parent player
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // If tag equal Player
        if (other.gameObject.tag == "Player")
        {
            // Reset parent player
            other.transform.SetParent(null);
        }
    }
}
