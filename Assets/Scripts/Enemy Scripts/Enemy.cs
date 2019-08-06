using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}


public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public float health;
    public string enemyName;
    public float moveSpeed;
    public int baseAttack;
    public FloatValue maxHealth;

    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    public void EnemyGetsKnocked(Rigidbody2D myRigidbody, float knockbackDuration, float damage)
    {
        StartCoroutine(KnockbackRoutine(myRigidbody, knockbackDuration));
        TakeDamage(damage);
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockbackRoutine(Rigidbody2D myRigidbody, float knockbackDuration)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockbackDuration);
            myRigidbody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
        }
    }

}
