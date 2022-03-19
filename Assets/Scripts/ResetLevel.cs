using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    public void Reset ()
    {
        ComplexTrigger.score = 0;
        //Debug.Log(ComplexTrigger.score);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
