    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class FallingPlatform : MonoBehaviour {
        public float fallingTime;
        
        private SpriteRenderer objectSprite;
        private TargetJoint2D targetJoint;
        private BoxCollider2D boxCollider;
        private Vector2 initialPosition;
        

        // Start is called before the first frame update
        void Start() {
            targetJoint = GetComponent<TargetJoint2D>();
            boxCollider = GetComponent<BoxCollider2D>();
            initialPosition = transform.position;
            objectSprite = GetComponent<SpriteRenderer>();
        }

        void OnCollisionEnter2D(Collision2D collision) {
            if(collision.gameObject.tag == "Player") {
                Invoke("Falling", fallingTime);
            }
        }


        void OnTriggerEnter2D(Collider2D collider) {
            if (collider.gameObject.layer == 9) {
                SetOpacity(0);
            }
        }

        void Falling() {
            targetJoint.enabled = false;
            boxCollider.isTrigger = true;
        }

        IEnumerator WaitingFalling() {
            yield return new WaitForSeconds(0.5f);
            SetOpacity(1);
            targetJoint.enabled = true;
            boxCollider.isTrigger = false;
            transform.position = initialPosition;
        }

        private void SetOpacity(float alpha) {
            Color currentColor = objectSprite.color;
            currentColor.a = alpha;
            objectSprite.color = currentColor;
        } 

        public void ResetPosition() {
            StartCoroutine(WaitingFalling());
        }
    }
