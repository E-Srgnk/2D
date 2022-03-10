using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private States State {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 5;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private SensorStair sensorStair;

    private bool isFire = false;
    private float fireDuration = 1.0f;
    private float fireCurrentTime;

    public bool isFacingRight = true;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        sensorStair = transform.Find("StairSensor").GetComponent<SensorStair>();
    }

    void Start() {

    }

    void Update() {
        if (isFire) fireCurrentTime += Time.deltaTime;
        if (fireCurrentTime > fireDuration) {
            isFire = false;
            fireCurrentTime = 0f;
        }

        if (sensorStair.isTrigger) {
            rb.gravityScale = 0f;
        } else {
            rb.gravityScale = 1f;
        }

        float h = Input.GetAxis("Horizontal");
        if (h > 0 && !isFacingRight || h < 0 && isFacingRight) 
            Flip();

        if (Input.GetButton("Fire1")) {
            isFire = true;
            State = States.fire;
        } else if (Input.GetButton("Horizontal") && !isFire) {
            Run();
        } else if (Input.GetButton("Vertical") && sensorStair.isTrigger) {
            Climb();
        } else if (!isFire) State = States.idle;
    }

    private void Run() {
        State = States.run;

        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void Climb() {
        State = States.climb;

        Vector3 direction = transform.up * Input.GetAxis("Vertical");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void Flip() {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

public enum States {
    idle,
    run,
    climb,
    fire
}
