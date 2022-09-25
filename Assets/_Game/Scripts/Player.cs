using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
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

    [SerializeField]
    private Kunai kunaiPrefab;

    [SerializeField]
    private GameObject attackArea;

    public Transform throwPoint;

    // Radius of the overlap circle to determine if grounded
    const float groundedRadius = 0.2f;

    // Whether can movement on jumping
    public bool airControl = true;

    // Whether or not the player is grounded.
    private bool grounded;
    private Vector2 current_velocity = Vector2.zero;

    public Rigidbody2D characterRigidbody2D;
    private bool facingRight = true;
    float horizontalMove = 0f;
    public float runSpeed = 40f;

    bool jump = false;
    bool isAttack = false;
    bool isJumping = false;
    bool isDeath = false;
    private int coin = 0;
    private Vector3 savePoint;

    private void Awake()
    {
        characterRigidbody2D = GetComponent<Rigidbody2D>();
        coin = PlayerPrefs.GetInt("coin", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.C) && grounded)
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.V) && grounded)
        {
            Throw();
        }
    }

    private void FixedUpdate()
    {
        if (isDeath)
        {
            return;
        }

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

        // horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        // Move character
        Move(horizontalMove, jump);

        jump = false;

        // If grounded and user input move character
        if (Mathf.Abs(horizontalMove) > 0.1f && grounded)
        {
            isJumping = false;
            ChangeAnim("run");
            return;
        }

        // If grounded and not attack
        if (grounded)
        {
            if (!isAttack)
            {
                isJumping = false;
                ChangeAnim("idle");
                return;
            }
        }
        // If jumping
        if (!grounded && characterRigidbody2D.velocity.y > 0)
        {
            ChangeAnim("jump");
            isJumping = true;
            return;
        }

        if (!grounded && characterRigidbody2D.velocity.y < 0 && isJumping)
        {
            ChangeAnim("fall");
            return;
        }
    }

    override public void OnInit()
    {
        base.OnInit();

        UiManager.instance.SetCoin(coin);

        DeactiveAttack();

        if (isDeath)
        {
            // Spawn player at save point
            transform.position = savePoint;
        }

        isDeath = false;
        isAttack = false;
        isJumping = false;

        // Re save point
        SavePoint();

        ChangeAnim("idle");
    }

    public override void OnDeath()
    {
        base.OnDeath();
        isDeath = true;
        ChangeAnim("die");
        Invoke(nameof(OnInit), 1f);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public void Attack()
    {
        if (isAttack)
            return;
        ChangeAnim("attack");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
        ActiveAttack();
        Invoke(nameof(DeactiveAttack), .5f);
    }

    public void Throw()
    {
        if (isAttack)
            return;
        ChangeAnim("throw");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
        Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);
    }

    public void Jump()
    {
        jump = true;
    }

    private void ResetAttack()
    {
        isAttack = false;
    }

    public void Move(float move, bool jump)
    {
        // Debug.Log("Move speed: " + move);
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

    public void HandleMove(float move)
    {
        this.horizontalMove = move * runSpeed;
    }

    private void Flip()
    {
        facingRight = !facingRight;

        if (facingRight)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If tag equal Coin
        if (other.tag == "Coin")
        {
            // Increase coin
            coin++;
            PlayerPrefs.SetInt("coin", coin);
            UiManager.instance.SetCoin(coin);
            // Destroy coin prefab
            Destroy(other.gameObject);
        }

        // If tag equal DeathZone
        if (other.tag == "DeathZone")
        {
            isDeath = true;
            ChangeAnim("die");

            // Re init game
            Invoke(nameof(OnInit), 1f);
        }
    }

    internal void SavePoint()
    {
        Debug.Log("Save point");
        savePoint = transform.position;
    }

    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeactiveAttack()
    {
        attackArea.SetActive(false);
    }
}
