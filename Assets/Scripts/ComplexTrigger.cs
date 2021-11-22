using UnityEngine;
using UnityEngine.Events;

public class ComplexTrigger : MonoBehaviour
{

    //public Rigidbody2D triggerBody; 
    //public UnityEvent onTriggerEnter;
    public string checkTag;
    public ParticleSystem ps;


    void OnTriggerEnter2D(Collider2D other){
        //do not trigger if there's no trigger target object
        //if (triggerBody == null) return;

        //only trigger if the triggerBody matches
        if (other.gameObject.tag == checkTag){
            ps.Play();
        }
    }

}
