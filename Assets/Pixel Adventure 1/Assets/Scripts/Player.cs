using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  public float Speed;
  public float JumpForce;
  public bool isJumping;
  public bool doubleJump;
  public SpriteRenderer sprite;

  private Rigidbody2D rigid2d;
  private Animator animator;

  // Start is called before the first frame update
  void Start()  {
    rigid2d = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update() {
    Move();
    Jump();
  }

  void Move() {
    Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
    transform.position += movement * Time.deltaTime * Speed;

    if(Input.GetAxis("Horizontal") > 0f) {
      animator.SetBool("walk", true);
      transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    if(Input.GetAxis("Horizontal") < 0f) {
      animator.SetBool("walk", true);
      transform.eulerAngles = new Vector3(0f, 180f, 0f);
    }

    if(Input.GetAxis("Horizontal") == 0f) {
      animator.SetBool("walk", false);
    }
  }

  void Jump() {
    if(Input.GetButtonDown("Jump")) {
      if(!isJumping) {
        rigid2d.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        doubleJump = true;
        animator.SetBool("jump", true);
      } else if(doubleJump) {
        rigid2d.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        doubleJump = false;
      }
    }
  }

  public void Hit() {
    StartCoroutine(Flick());
  }

  IEnumerator Flick() {
    sprite.color = new Color (1, 1, 1, 0);
    yield return new WaitForSeconds(0.2f);
    sprite.color = new Color (1, 1, 1, 1);
    yield return new WaitForSeconds(0.2f);
    sprite.color = new Color (1, 1, 1, 1);
    yield return new WaitForSeconds(0.2f);
    sprite.color = new Color (1, 1, 1, 0);
    yield return new WaitForSeconds(0.2f);
    sprite.color = new Color (1, 1, 1, 1);
  }

  void OnCollisionEnter2D(Collision2D colission) {
    if(colission.gameObject.layer == 8) {
      isJumping = false;
        animator.SetBool("jump", false);
    }
  }

  void OnCollisionExit2D(Collision2D colission) {
    if(colission.gameObject.layer == 8) {
      isJumping = true;
    }
  }
}
