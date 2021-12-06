using UnityEngine;
using UnityEngine.Events;

public class ComplexTrigger : MonoBehaviour
{

    //public Rigidbody2D triggerBody; 
    //public UnityEvent onTriggerEnter;
    public string checkTag;
    public ParticleSystem ps;
    public static int score = 0;


    void OnTriggerEnter2D(Collider2D other){
        //do not trigger if there's no trigger target object
        //if (triggerBody == null) return;

        //only trigger if the triggerBody matches
        if (other.gameObject.tag == checkTag){
            score++;
            ps.Play();
            Debug.Log(score);
            other.gameObject.GetComponent<Renderer>().enabled = false;
            other.gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(other.gameObject.GetComponent<Rigidbody>());
        }
    }
    
    void Awake()
    {
      score = 0;
    }

}
