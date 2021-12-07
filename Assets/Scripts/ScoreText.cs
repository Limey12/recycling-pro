using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Text scoreText;
    public int totalObjects;
    //public static int[] scores = new int[]{0, 0, 0};

    // Update is called once per frame
    void Update()
    {
        scoreText.text = ComplexTrigger.score.ToString() + " / " + totalObjects.ToString();
    }

    //void setScore()
    // void Awake()
    // {
    //     int total = scores[0] + scores[1] + scores[2];
    //     scoreText.text = (total).ToString() + " / " + totalObjects.ToString();
    // }
}
