using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSensorGroundEnemy : MonoBehaviour {
    public bool inGround = true;

    void Start() {

    }

    void Update() {

    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Ground")) {
            inGround = false;
            Debug.Log("right exit");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Ground")) {
            inGround = true;
            Debug.Log("right enter");
        }
    }
}
