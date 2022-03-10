using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorStair : MonoBehaviour {

    public bool isTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Stair")) {
            isTrigger = true;
            Debug.Log("enter trigger");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Stair")) {
            isTrigger = false;
            Debug.Log("exit trigger");
        }
    }

    void Start() {

    }

    void Update() {

    }
}
