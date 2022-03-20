using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchItem : MonoBehaviour
{

    public Rigidbody2D rb; // Need to set to own RigidBody2D in editor

    public Rigidbody2D sling; // Need to set to RigidBody2D of sling in editor

    public LineRenderer lr; // Need to set to LineRenderer in editor

    // No longer used
    // Higher frequency should mean a lower releaseTime.
    // Default time of 0.15f works well with frequency of 1.5.
    // public float releaseTime = 0.15f; // Time from mouse release to spring release

    public float maxDistance = 2.0f; // Furthest distance the item can be dragged from the sling

    public float spawnTime = 1.0f; // Time between release and next item spawn

    public float maxSpeed = 15.0f; // Max speed an object can be launched at
    
    public int stepCount = 100; // Number of steps to simulate for trajectory

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
            // Multiplies the max speed by a number between 1 and 0 based on how far you pull the object back
            float speed = maxSpeed * (Vector3.Distance(mosPos, sling.position)/maxDistance);
            // Sets velocity to speed times the direction to the pivot
            Vector2 velocity = (sling.position - mosPos).normalized * speed;
            Vector2[] trajectory = Plot(rb, (Vector2)transform.position, velocity, stepCount);
            lr.positionCount = trajectory.Length;
            Vector3[] positions = new Vector3[trajectory.Length];
            for (int i = 0; i < trajectory.Length; i++)
            {
                positions[i] = trajectory[i];
            }
            lr.SetPositions(positions);
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
        Vector2 mosPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isGrabbed = false;
        rb.isKinematic = false;
        // Multiplies the max speed by a number between 1 and 0 based on how far you pull the object back
        float speed = maxSpeed * (Vector3.Distance(mosPos, sling.position)/maxDistance);
        // Sets velocity to speed times the direction to the pivot
        rb.velocity = (sling.position - mosPos).normalized * speed;
        GetComponent<SpringJoint2D>().enabled = false; // Release spring, not easily undone
        GetComponent<AudioSource>().Stop();
        // StartCoroutine(Release());
        StartCoroutine(NextObjSpawn());
        lr.enabled = false;
        this.enabled = false;
    }

    IEnumerator NextObjSpawn()
    {   
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

    /*
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
    */

    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] output = new Vector2[steps];
        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;
        float drag = 1f - timestep * rigidbody.drag;
        Vector2 moveStep = velocity * timestep;

        for (int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            output[i] = pos;
        }

        return output;
    }
}
