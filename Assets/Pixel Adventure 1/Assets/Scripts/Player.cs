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
  public bool isBlowing;
  public SpriteRenderer sprite;
  
  private GameManager gameManager;
  private Rigidbody2D rigid2d;
  private Animator animator;
  private bool recovery;

  private static Player instance;

  void Awake() {
    if (instance == null) {
        instance = this;
        DontDestroyOnLoad(gameObject);
        gameManager = FindObjectOfType<GameManager>();
    } else {
        Destroy(gameObject);
    }
  } 


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
      SceneManager.LoadScene(0);
    }
  }

  public void ResetLife() {
    life = 3;
    GameController.instance.UpdateLifeText();  
    GameController.instance.totalScore = 0;
    GameController.instance.UpdateScoreText();
  }

  void Jump() {
    if(Input.GetButtonDown("Jump") && !isBlowing) {
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
    }
      Death();
      GameController.instance.UpdateLifeText();  
      StartCoroutine(Flick());
      FindObjectOfType<GameManager>().MovePlayerToSpawnPoint();
      ResetPlatformsPosition();
  }

  void ResetPlatformsPosition() {
    FallingPlatform[] platforms = FindObjectsOfType<FallingPlatform>();
    foreach (FallingPlatform platform in platforms) {
      platform.ResetPosition();
    }
  }

  IEnumerator Flick() {
    recovery = true;
    sprite.color = new Color (1, 1, 1, 0);
    yield return new WaitForSeconds(0.3f);
    sprite.color = new Color (1, 1, 1, 1);
    recovery = false;
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


  void OnTriggerStay2D(Collider2D collider) {
    if(collider.gameObject.layer == 11) {
      isBlowing = true;
    }
  }

  void OnTriggerExit2D(Collider2D collider) {
    if(collider.gameObject.layer == 11) {
      isBlowing = false;
    }
  }
}
