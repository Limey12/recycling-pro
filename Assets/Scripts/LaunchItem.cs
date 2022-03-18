using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchItem : MonoBehaviour
{

    public Rigidbody2D rb; // Need to set to own RigidBody2D in editor
    public Rigidbody2D sling; // Need to set to RigidBody2D of sling in editor

    // Higher frequency should mean a lower releaseTime.
    // Default time of 0.15f works well with frequency of 1.5.
    public float releaseTime = 0.15f; // Time from mouse release to spring release

    public float maxDistance = 2.0f; // Furthest distance the item can be dragged from the sling

    public float spawnTime = 1.0f; // Time between release and next item spawn
    
    public GameObject nextObj; // Next item to spawn

    public GameObject returnB; // Buttons to spawn when no more items
    public GameObject resultsB;

    private bool isGrabbed = false;

    public AudioClip stretch;

    void Start()
    {
        GetComponent<AudioSource>().clip = stretch;
    }
    // Update is called once per frame
    void Update()
    {
        if (isGrabbed)
        {
            Vector2 mosPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mosPos, sling.position) > maxDistance)
            {
                rb.position = sling.position + (mosPos - sling.position).normalized * maxDistance;
            }
            else
            {
                rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }

    // OnMouseDown is called when item is grabbed (clicked)
    void OnMouseDown()
    {
        isGrabbed = true;
        rb.isKinematic = true; // No longer effected by spring/gravity
        GetComponent<AudioSource>().Play();
    }

    // OnMouseUp is called when item is released
    void OnMouseUp()
    {
        isGrabbed = false;
        rb.isKinematic = false;
        GetComponent<AudioSource>().Stop();
        StartCoroutine(Release());
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);

        GetComponent<SpringJoint2D>().enabled = false; // Release spring, not easily undone
        this.enabled = false;
        
        yield return new WaitForSeconds(spawnTime);
        
        if(nextObj != null)
        {
          nextObj.SetActive(true);
        } else 
        {
          returnB.SetActive(true);
          resultsB.SetActive(true);
        }
    }
}
