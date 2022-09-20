using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float speed = 20f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(
            target.position,
            target.position + offset,
            Time.fixedDeltaTime * speed
        );
    }
}
