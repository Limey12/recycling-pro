using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
           Debug.Log("start");
    }
    void OnCollisionEnter2d(Collision2D col)
    {
        Debug.Log("Please");

    }
    void OnCollisionExit2d()
    {
        Debug.Log("Please1");

    }
    void OnCollisionStay2d()
    {
        Debug.Log("Please2");

    }

    void OnTriggerEnter2D()
    {
        Debug.Log("Please3");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
