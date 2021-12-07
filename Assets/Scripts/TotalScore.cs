using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour
{
    public Text scoreText;
    public Text evaluation;
    public int totalObjects;
    public static int[] scores = new int[]{0, 0, 0};

    // void Start()
    // {
    //     int total = scores[0] + scores[1] + scores[2];
    //     scoreText.text = (total).ToString() + " / " + totalObjects.ToString();
    // }

    void Awake()
    {
        int total = scores[0] + scores[1] + scores[2];
        scoreText.text = (total).ToString() + " / " + totalObjects.ToString();
        if (total == 0)
        {
            evaluation.text = "Be sure to check the How To Play menu to get an understanding of how to be a recycling pro!";
        }
        if(total > 0 & total <= 4)
        {
            evaluation.text = "By clicking the info button on each level, you can check which bin an object should be recycled in!";
        }
        else if (total > 4 & total <=8)
        {
            evaluation.text = "You're starting to get the hang of this! Practice launching objects so you won't miss the correct bin!";
        }
        else if (total > 8 & total <=11)
        {
            evaluation.text = "You're so close to getting a perfect score, just keep on trying!";
        }
        else if (total == 12)
        {
            evaluation.text = "Wow, a perfect score! You're a Recycling Pro!";
        }

    }
}
