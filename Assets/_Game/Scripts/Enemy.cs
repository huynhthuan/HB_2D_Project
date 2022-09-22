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
            currentState.OnExecute(this);
        }
    }

    public override void OnInit()
    {
        base.OnInit();

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
        ChangeState(null);
        base.OnDeath();
    }

    internal void setTarget(Character character)
    {
        this.target = character;

        if (IsTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else if (Target != null)
        {
            ChangeState(new PatrolState());
        }
        else
        {
            ChangeState(new IdleState());
        }
    }

    private IState currentState;

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
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
        ActiveAttack();
        Invoke(nameof(DeactiveAttack), .5f);
    }

    public bool IsTargetInRange()
    {
        return target != null
            && Vector2.Distance(target.transform.position, transform.position) <= attackRange;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyWall")
        {
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
