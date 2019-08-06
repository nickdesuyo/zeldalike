using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    attack,
    interact,
    stagger
}


public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float moveSpeed;
    public FloatValue currentHealth;
    public static bool isFacingUp; //used by ReadableObjects to check if player is facing towards the Sign/Character/etc
    private Rigidbody2D myRigidbody;
    private Vector3 movementInput;
    private Animator animator;
    public SignalObject playerHealthSignal;
    public VectorValue startingPosition;


    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        transform.position = startingPosition.runtimeValue;
    }

    // Update is called once per frame
    void Update()
    {
        movementInput = Vector3.zero;
        movementInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetButtonDown("attack") 
            && currentState != PlayerState.attack 
            && currentState != PlayerState.stagger)
        {
            StartCoroutine(Attack());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator Attack()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }

    void UpdateAnimationAndMove()
    {
        if (movementInput != Vector3.zero)
        {
            CheckIfPlayerIsFacingUp();
            MoveCharacter();
            animator.SetBool("moving", true);
            animator.SetFloat("moveX", movementInput.x);
            animator.SetFloat("moveY", movementInput.y);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void CheckIfPlayerIsFacingUp()
    {
        if (movementInput.y > 0)
        {
            isFacingUp = true;
        }
        else
        {
            isFacingUp = false;
        }
    }

    void MoveCharacter()
    {
        movementInput.Normalize();
        myRigidbody.MovePosition(transform.position + movementInput * moveSpeed * Time.deltaTime);
    }

    public void PlayerGetsKnocked(float knockbackDuration, float damage)
    {
        StartCoroutine(KnockbackRoutine(knockbackDuration));
        TakeDamage(damage);
    }

    private void TakeDamage(float damage)
    {
        currentHealth.runtimeValue -= damage;
        if (currentHealth.runtimeValue <= 0)
        {
            this.gameObject.SetActive(false);
        }
        playerHealthSignal.Raise();
    }

    private IEnumerator KnockbackRoutine(float knockbackDuration)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockbackDuration);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
        }
    }
}
