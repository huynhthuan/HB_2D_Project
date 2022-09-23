using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private Animator animatior;
    private float hp;

    public bool IsDead => hp <= 0;

    private string currentAnimName;

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        hp = 100;
    }

    public virtual void OnDespawn() { }

    public virtual void OnDeath() { }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            animatior.ResetTrigger(animName);
            currentAnimName = animName;
            animatior.SetTrigger(currentAnimName);
        }
    }

    public void OnHit(float damage)
    {
        if (!IsDead)
        {
            // Is dead false, continue take damage
            hp -= damage;

            if (IsDead)
            {
                // Is dead true, call OnDeath
                OnDeath();
            }
        }
    }
}
