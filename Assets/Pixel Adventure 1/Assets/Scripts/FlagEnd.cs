using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPoint : MonoBehaviour {
  private void OnTriggerEnter2D(Collider2D collision) {
    if(collision.CompareTag("Player")) {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
  }
}
