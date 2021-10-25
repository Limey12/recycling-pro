using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchItem : MonoBehaviour
{

    public Rigidbody2D rb; // Need to set to own RigidBody2D in editor

    // Higher frequency should mean a lower releaseTime.
    // Default time of 0.15f works well with frequency of 1.5.
    public float releaseTime = 0.15f; // Time from mouse release to spring release

    private bool isGrabbed = false;

    // Update is called once per frame
    void Update()
    {
        if(isGrabbed)
        {
            rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    // OnMouseDown is called when item is grabbed (clicked)
    void OnMouseDown()
    {
        isGrabbed = true;
        rb.isKinematic = true; // No longer effected by spring/gravity
    }

    // OnMouseUp is called when item is released
    void OnMouseUp()
    {
        isGrabbed = false;
        rb.isKinematic = false;

        StartCoroutine(Release());
    }

    IEnumerator Release ()
    {
        yield return new WaitForSeconds(releaseTime);

        GetComponent<SpringJoint2D>().enabled = false; // Release spring, not easily undone
        this.enabled = false;
    }
}
