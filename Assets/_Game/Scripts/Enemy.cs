using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField]
    private float attackRange;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject attackArea;

    bool isRight = true;

    private Character target;
    public Character Target => target;

    private void Update()
    {
        if (currentState != null)
        {
            // Loop execute
            currentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();

        // Reset to idle state
        DeactiveAttack();
        ChangeState(new IdleState());
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(gameObject);
    }

    public override void OnDeath()
    {
        base.OnDeath();

        ChangeState(null);
        // Change state to null resolve confict anim
        ChangeAnim("die");
        OnDespawn();
    }

    internal void setTarget(Character character)
    {
        this.target = character;

        // Check target in attack range
        if (IsTargetInRange())
        {
            // Target in attack range, change attack state
            ChangeState(new AttackState());
        }
        else if (Target != null)
        {
            // Target not in attack range, but in sight, change partrol state
            ChangeState(new PatrolState());
        }
        else
        {
            // Not has target, change idle state
            ChangeState(new IdleState());
        }
    }

    private IState currentState;

    public void ChangeState(IState newState)
    {
        // Check has current state
        if (currentState != null)
        {
            // Exit current state
            currentState.OnExit(this);
        }

        // Set new state
        currentState = newState;

        // Check set new state success
        if (currentState != null)
        {
            // Enter new state
            currentState.OnEnter(this);
        }
    }

    public void Moving()
    {
        ChangeAnim("run");
        rb.velocity = transform.right * moveSpeed;
    }

    public void StopMoving()
    {
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }

    public void Attack()
    {
        ChangeAnim("attack");
        // Attack target
        ActiveAttack();
        // Deactive attack after .5s
        Invoke(nameof(DeactiveAttack), .5f);
    }

    public bool IsTargetInRange()
    {
        return target != null
            && Vector2.Distance(target.transform.position, transform.position) <= attackRange;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check trigger wall collider
        if (other.tag == "EnemyWall")
        {
            // Flip current dirrection
            ChangeDirection(!isRight);
        }
    }

    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;

        transform.rotation = isRight
            ? Quaternion.Euler(Vector3.zero)
            : Quaternion.Euler(Vector3.up * 180);
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
