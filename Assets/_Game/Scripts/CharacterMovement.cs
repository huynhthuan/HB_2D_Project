using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller2D;
    public Animator animatior;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    bool jump = false;
    bool isAttack = false;
    bool isJumping = false;
    private string currentAnimName;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.C) && controller2D.grounded)
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.V) && controller2D.grounded)
        {
            Throw();
        }
    }

    private void FixedUpdate()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        // Move character
        controller2D.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;

        // If grounded and user input move character
        if (Mathf.Abs(horizontalMove) > 0.1f && controller2D.grounded)
        {
            ChangeAnim("run");
        }
        // If grounded and not attack
        else if (controller2D.grounded)
        {
            if (!isAttack)
            {
                isJumping = false;
                Debug.Log("Idle");
                ChangeAnim("idle");
            }
        }
        // If jumping
        else if (!controller2D.grounded && controller2D.characterRigidbody2D.velocity.y > 0)
        {
            ChangeAnim("jump");
            isJumping = true;
        }
        else if (!controller2D.grounded && controller2D.characterRigidbody2D.velocity.y < 0 && isJumping)
        {
            Debug.Log("Fall");
            ChangeAnim("fall");
        }
    }

    private void Attack()
    {
        if (isAttack)
            return;
        ChangeAnim("attack");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
    }

    private void Throw()
    {
        if (isAttack)
            return;
        ChangeAnim("throw");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
    }

    private void Jump()
    {
        jump = true;
    }

    private void ResetAttack()
    {
        isAttack = false;
    }

    private void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            animatior.ResetTrigger(animName);
            currentAnimName = animName;
            animatior.SetTrigger(currentAnimName);
        }
    }
}
