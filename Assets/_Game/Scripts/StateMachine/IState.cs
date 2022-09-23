using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    // Enter state
    void OnEnter(Enemy enemy) { }

    // Stay state
    void OnExecute(Enemy enemy) { }

    // Exit state
    void OnExit(Enemy enemy) { }
}
