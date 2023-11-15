using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour {
    void Start() {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(pressRestart);
    }

    void pressRestart() {
        SceneManager.LoadScene(1);
        Player playerScript = FindObjectOfType<Player>();
        playerScript.ResetLife();
    }
}
