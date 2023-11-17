using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour {
    public float jumpForce;
    
    private Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D colission) {
        if(colission.gameObject.tag == "Player") {
            anim.SetTrigger("Jump");
            colission.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}
