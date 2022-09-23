using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    float timer;
    float randomTime;

    public void OnEnter(Enemy enemy)
    {
        // Stop moving when enter idle
        enemy.StopMoving();
        timer = 0;
        // Regenrate random time to change patrol state
        randomTime = Random.Range(2f, 4f);
    }

    public void OnExecute(Enemy enemy)
    {

        timer += Time.deltaTime;

        // Check timer current and random time
        if (timer > randomTime)
        {
            // Idle state -> patrol state
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy) { }
}
