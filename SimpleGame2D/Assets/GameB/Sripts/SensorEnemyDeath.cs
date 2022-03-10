using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorEnemyDeath : MonoBehaviour {

    public bool sensorDeath = false;

    void Start() {

    }

    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            sensorDeath = true;
        }
    }
}
