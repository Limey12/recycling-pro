using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchItem : MonoBehaviour
{

    public Rigidbody2D rb; // Need to set to own RigidBody2D in editor
    public Rigidbody2D point; // Need to set to RigidBody2D of sling shot in editor

    // Higher frequency should mean a lower releaseTime.
    // Default time of 0.15f works well with frequency of 1.5.
    public float releaseTime = 0.15f; // Time from mouse release to spring release

    public float maxDragDistance = 2.0f; // Max pull distance from point

    private bool isGrabbed = false;

    // Update is called once per frame
    void Update()
    {
        if(isGrabbed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(mousePos, point.position) > maxDragDistance)
                rb.position = point.position + (mousePos - point.position).normalized * maxDragDistance;
            else
                rb.position = mousePos;
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
    }
}
