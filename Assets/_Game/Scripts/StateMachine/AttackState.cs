using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer;

    public void OnEnter(Enemy enemy)
    {
        // Check target
        if (enemy.Target != null)
        {
            // Change direction enemy to player
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
            enemy.StopMoving();
            // Attack target
            enemy.Attack();
        }

        timer = 0;
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        // Check timer attack
        if (timer >= 1.5f)
        {
            // End attack and continue moving
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy) { }
}
