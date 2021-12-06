using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    public Text scoreText;
    public int totalObjects;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = ComplexTrigger.score.ToString() + " / " + totalObjects.ToString();
    }
}
