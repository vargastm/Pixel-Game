using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
  public float Speed;
  public float JumpForce;
  public int life;
  public bool isJumping;
  public bool doubleJump;
  public SpriteRenderer sprite;
  public GameObject gameOver;

  private Rigidbody2D rigid2d;
  private Animator animator;
  private bool recovery;

  // Start is called before the first frame update
  void Start()  {
    rigid2d = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    Time.timeScale = 1;
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

  void Death() {
    if(life <= 0) {
      gameOver.SetActive(true);
      Time.timeScale = 0;
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
    if(recovery == false) {
      life--;
      Death();
      GameController.instance.UpdateLifeText();  
      StartCoroutine(Flick());
    }
  }

  IEnumerator Flick() {
    recovery = true;
    sprite.color = new Color (1, 1, 1, 0);
    yield return new WaitForSeconds(0.2f);
    sprite.color = new Color (1, 1, 1, 1);
    yield return new WaitForSeconds(0.2f);
    sprite.color = new Color (1, 1, 1, 0);
    yield return new WaitForSeconds(0.2f);
    sprite.color = new Color (1, 1, 1, 1);
    recovery = false;
  }

  public void RestartGame() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
