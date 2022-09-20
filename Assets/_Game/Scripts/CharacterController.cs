using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // A position marking where to check if the player is grounded.
    [SerializeField]
    private Transform groundCheck;

    // Amount of force added when the player jumps.
    [SerializeField]
    private float jumpForce = 400f;

    // A mask determining what is ground to the character
    [SerializeField]
    private LayerMask whatIsGround;

    [Range(0, .3f)]
    [SerializeField]
    private float movementSmoothing = .05f;

    // Radius of the overlap circle to determine if grounded
    const float groundedRadius = 0.2f;

    // Whether can movement on jumping
    public bool airControl = true;

    // Whether or not the player is grounded.
    public bool grounded;
    private Vector2 current_velocity = Vector2.zero;

    public Rigidbody2D characterRigidbody2D;
    private bool facingRight = true;

    private void Awake()
    {
        characterRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            groundCheck.position,
            groundedRadius,
            whatIsGround
        );

        for (int i = 0; i < colliders.Length; i++)
        {
            // If gameObject of collider not equal this gameObject
            if (colliders[i] && (colliders[i].gameObject != gameObject))
            {
                // Set character grounded
                grounded = true;
            }
        }
    }

    public void Move(float move, bool jump)
    {
        // If character grounded
        if (grounded || airControl)
        {
            // Get target velocity
            Vector2 targetVelocity = new Vector2(move, characterRigidbody2D.velocity.y);

            // Move character by target velocity
            characterRigidbody2D.velocity = Vector2.SmoothDamp(
                characterRigidbody2D.velocity,
                targetVelocity,
                ref current_velocity,
                movementSmoothing
            );

            if (move > 0 && !facingRight)
            {
                Flip();
            }

            if (move < 0 && facingRight)
            {
                Flip();
            }
        }

        // If chacter shoud jump
        if (grounded && jump)
        {
            grounded = false;
            characterRigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 characterScale = transform.localScale;
        characterScale.x *= -1;
        transform.localScale = characterScale;
    }
}
