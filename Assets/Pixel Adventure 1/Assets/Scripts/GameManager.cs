using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public Transform point;

    void Start() {
        MovePlayerToSpawnPoint();
    }

    public void MovePlayerToSpawnPoint()  {
        Player player = FindObjectOfType<Player>();
        player.transform.position = point.position;
    }
}
