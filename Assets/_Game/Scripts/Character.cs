using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    protected HealthBar healthBar;

    [SerializeField]
    private Animator animatior;

    [SerializeField]
    protected CombatText CombatTextPrefab;
    public float hp = 100f;

    public bool IsDead => hp <= 0f;

    private string currentAnimName;

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        healthBar.OnInit(hp, transform);
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
                hp = 0;
                // Is dead true, call OnDeath
                OnDeath();
            }

            healthBar.SetNewHP(hp);
            Instantiate(CombatTextPrefab, transform.position + Vector3.up, Quaternion.identity)
                .OnInit(damage);
        }
    }
}
