using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BounceNoise : MonoBehaviour
{
    public AudioClip noise;
    

    void Start()
    {
        GetComponent<AudioSource>().clip = noise;
        Debug.Log(GetComponent<AudioSource>());
    }

    // Play audio on collision
    void OnCollisionEnter2d()
    {
        GetComponent<AudioSource>().Play();
        Debug.Log(GetComponent<AudioSource>());

    }
}
