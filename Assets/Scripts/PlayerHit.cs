using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public float playerThrust;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("breakable") && collision.GetComponent<BreakableObject>() != null)
        {
            collision.GetComponent<BreakableObject>().Smash();
        }

        /*if (collision.CompareTag("Enemy") || collision.CompareTag("Player"))
        {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                collision.GetComponent<Log>().Stagger();
                Vector2 positionDifference = hit.transform.position - transform.position;
                positionDifference = positionDifference.normalized * playerThrust;
                hit.AddForce(positionDifference, ForceMode2D.Impulse);
            }
        }*/
    }
}
