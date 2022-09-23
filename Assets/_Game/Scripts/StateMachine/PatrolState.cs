using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float timer;
    float randomTime;

    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        // Regenrate time to change state
        randomTime = Random.Range(3f, 6f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        // Check enemy has target
        if (enemy.Target != null)
        {
            // Rotation direction character to enemy
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);

            // Check enemy in attack ranger
            if (enemy.IsTargetInRange())
            {
                // Enemy in target change attack state
                enemy.ChangeState(new AttackState());
            }
            else
            {
                // Has target but not in attack range, character keep moving
                enemy.Moving();
            }
        }
        else
        {
            // Check time to change idle state
            if (timer < randomTime)
            {
                // Timer not pass random time, keep moving
                enemy.Moving();
            }
            else
            {
                // Timer pass random time, change idle state
                enemy.ChangeState(new IdleState());
            }
        }
    }

    public void OnExit(Enemy enemy) { }
}
