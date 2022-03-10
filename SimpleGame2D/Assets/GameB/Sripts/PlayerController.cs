using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private States State {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    [SerializeField] private float speed = 2f;
    [SerializeField] private float jumpForce = 6f;
    private bool isFacingRight = true;
    private bool grounded = false;

    private Animator anim;
    private Rigidbody2D playerRb;
    private SensorGround sensorGround;
    private SpriteRenderer sprite;

    private void Awake() {
        playerRb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        sensorGround = transform.Find("SensorGround").GetComponent<SensorGround>();
    }

    void Start() {

    }

    private void FixedUpdate() {
        CheckGround();
    }

    void Update() {
        if (sensorGround.isGrounded) State = States.idle;

        if (Input.GetButton("Horizontal")) {
            Run();
        }
        if (sensorGround.isGrounded && Input.GetButtonDown("Jump")) {
            Jump();
        }
    }

    private void Run() {
        if (grounded)
            State = States.run;

        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        sprite.flipX = direction.x < 0.0f;
    }

    private void Jump() {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround() {
        if (!grounded && sensorGround.isGrounded) {
            grounded = true;
        }
        if (grounded && !sensorGround.isGrounded) {
            grounded = false;
        }

        if (!grounded) State = States.jump;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("GoldCoin")) {
            Destroy(collision.gameObject);
        } else if (collision.CompareTag("SilverCoin")) {
            Destroy(collision.gameObject);
        } else if (collision.CompareTag("BronzeCoin")) {
            Destroy(collision.gameObject);
        }
    }

    public void ReceiveDamage() {
        Destroy(gameObject);
    }
}


public enum States {
    idle,
    run,
    jump
}