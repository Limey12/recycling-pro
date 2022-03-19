using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ComplexTrigger : MonoBehaviour
{

    //public Rigidbody2D triggerBody; 
    //public UnityEvent onTriggerEnter;
    public string checkTag;
    public ParticleSystem ps;
    public static int score = 0;
    public AudioClip noise;
    

    void OnTriggerEnter2D(Collider2D other){
        //do not trigger if there's no trigger target object
        //if (triggerBody == null) return;

        //only trigger if the triggerBody matches
        if (other.gameObject.tag == checkTag){
            score++;
            ps.Play();
            GetComponent<AudioSource>().Play();
            //Debug.Log(score);
            other.gameObject.GetComponent<Renderer>().enabled = false;
            other.gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(other.gameObject.GetComponent<Rigidbody>());

            //Check/Update high score
            if (TotalScore.scores[SceneManager.GetActiveScene().buildIndex - 1] < score)
            {
                TotalScore.scores[SceneManager.GetActiveScene().buildIndex - 1] = score;
            }
        }
    }
    
    void Awake()
    {
      score = 0;
      GetComponent<AudioSource>().clip = noise;
    }

}
