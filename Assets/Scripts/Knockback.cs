using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockbackDuration = 0.3f;
    public float damage;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
        {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();

            if (hit != null)
            {
                Vector2 positionDifference = hit.transform.position - transform.position;
                positionDifference = positionDifference.normalized * thrust;
                hit.AddForce(positionDifference, ForceMode2D.Impulse);

                if (collision.CompareTag("Enemy") && collision.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    collision.GetComponent<Enemy>().EnemyGetsKnocked(hit, knockbackDuration, damage);
                }

                if (collision.CompareTag("Player"))
                {
                    if (collision.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        collision.GetComponent<PlayerMovement>().PlayerGetsKnocked(knockbackDuration, damage);
                    }
                }
            }
        }

        if (collision.CompareTag("breakable") 
            && collision.GetComponent<BreakableObject>() != null
            && this.CompareTag("Player"))
        {
            collision.GetComponent<BreakableObject>().Smash();
        }
    }

    
}
