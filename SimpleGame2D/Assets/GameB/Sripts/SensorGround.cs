using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorGround : MonoBehaviour {

    public bool isGrounded = true;

    void Start() {

    }

    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Ground")) {
            isGrounded = true;
            Debug.Log("enter groind");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Ground")) {
            isGrounded = false;
            Debug.Log("exit groind");
        }
    }
}
