using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public Text txtScore;
    public Slider sliderHP;

    //누적 점수
    private int totScore = 0;

    
    void Start()
    {
        DispScore(0);
    }

    public void DispScore(int score)
    {
        totScore += score;
        txtScore.text = "SCORE <color=#ff0000>" + totScore.ToString() + "</color>";
    }

    public void DispHP(int hp)
    {
        sliderHP.value = hp;
    }
}
