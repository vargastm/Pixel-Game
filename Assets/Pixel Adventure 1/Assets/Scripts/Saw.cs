using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour {
    public float speed;
    public float moveTime;

    private bool directionRight = false;
    private float timer;

    // Update is called once per frame
    void Update() {
        if(directionRight) {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        } else {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        timer += Time.deltaTime;
        if(timer >= moveTime) {
            directionRight = !directionRight;
            timer = 0f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.CompareTag("Player")) {
            collision.GetComponent<Player>().Hit();
        }
    }
}
