using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 7f;
    private bool isGrounded = false;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;  
    private Animator animator;

    private HeroState State
    {
        get { return (HeroState)animator.GetInteger("State");  }
        set { animator.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    { 
        CheckGround();

        animator.SetFloat("AirSpeedY", rb.velocity.y);

        if (isGrounded && Input.GetButtonDown("Jump")) Jump();
        else if (Input.GetButton("Horizontal")) Run();    
        else State = HeroState.Idle;
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        sprite.flipX = direction.x < 0.0f;

        if (isGrounded) State = HeroState.Run;
    }

    private void Jump()
    {
        animator.SetTrigger("Jump");
        isGrounded = false;
        animator.SetBool("Grounded", isGrounded);
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);

        isGrounded = colliders.Length > 1;

        animator.SetBool("Grounded", isGrounded);
    }
}

public enum HeroState
{
    Idle, 
    Run
}
