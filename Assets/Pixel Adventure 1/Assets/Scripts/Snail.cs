using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour {
    private Animator animator;

    public float speed;
    public float moveTime;
    public Transform headPoint;

    private bool directionRight = false;
    private float timer;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if(directionRight) {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            
        } else {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        timer += Time.deltaTime;
        if(timer >= moveTime) {
            Flip();
            timer = 0f;
        }
    }

        private void Flip() {
        directionRight = !directionRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            float height = collision.contacts[0].point.y - headPoint.position.y;
           
            if(height > 0) {
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8, ForceMode2D.Impulse);
                speed = 0;
                animator.SetTrigger("die");
                Destroy(gameObject, 0.33f);
            } else {
                collision.collider.GetComponent<Player>().Hit();
            }
        }
    }

}
