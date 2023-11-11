using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
  public int totalScore;
  public Text scoreText;

    public Player player;
    public Text lifeText;


  public static GameController instance;

  void Start() {
    instance = this;
    UpdateLifeText();
  }

  public void UpdateScoreText() {
    scoreText.text = totalScore.ToString();
  }

  
    public void UpdateLifeText() {
        lifeText.text = player.life.ToString();
    }
 }