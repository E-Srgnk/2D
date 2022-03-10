using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private float speed = 2f;
    private SensorEnemyDeath sensorDeath;
    private SpriteRenderer sprite;
    private Vector3 direction;

    private LeftSensorGroundEnemy leftSensorGround;
    private RightSensorGroundEnemy rightSensorGround;

    private void Awake() {
        sensorDeath = GetComponentInChildren<SensorEnemyDeath>();
        sprite = GetComponent<SpriteRenderer>();
        leftSensorGround = GetComponentInChildren<LeftSensorGroundEnemy>();
        rightSensorGround = GetComponentInChildren<RightSensorGroundEnemy>();
    }

    private void Start() {
        direction = transform.right;
    }

    private void FixedUpdate() {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player) {
            if (sensorDeath.sensorDeath) {
                ReceiveDamage();
            } else {
                player.ReceiveDamage();
            }
        }
    }

    private void Move() {
        if (!rightSensorGround.inGround || !leftSensorGround.inGround) {
            sprite.flipX = direction.x < 0f;
            direction *= -1f;
        }

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void ReceiveDamage() {
        Destroy(gameObject);
    }
}
