using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    private Rigidbody2D rb2d;     
    public Transform[] waypoints;
    int cur = 0;

    public float speed = 0.3f;
    void FixedUpdate()
    {
        // Waypoint not reached yet? then move closer
        if (transform.position != waypoints[cur].position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position,
                                            waypoints[cur].position,
                                            speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
        else cur = (cur + 1) % waypoints.Length;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
         if (other.gameObject.CompareTag("pacman"))
          other.gameObject.SetActive(false);
    }


}
