using UnityEngine;

public class Snail : MonoBehaviour {
    public float speed = 1f;  
    private int direction = 1;  

    private void Update() {
        Move();
        CheckCollision();
    }

    private void Move() {
        Vector2 movement = new Vector2(speed * direction * Time.deltaTime, 0);
        transform.Translate(movement);
    }

    private void CheckCollision() {
        Vector2 startPoint = new Vector2(transform.position.x + direction * 0.5f, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast(startPoint, new Vector2(direction, 0), 0.1f);

        if (hit.collider != null) {
            direction *= -1;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
