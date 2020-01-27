using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class move : MonoBehaviour
{
    public Transform[] waypoints;
    int cur = 0;
    int vuln =5;
    private Renderer rend;
    private Animator ghostAnimator;
    public float speed = 0.3f;
    public Text loseText;


    void Start(){
        ghostAnimator = GetComponent<Animator>();
        loseText.text = "";
    }

    void FixedUpdate()
    { // Waypoint not reached yet? then move closer
   
    
        if (transform.position != waypoints[cur].position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position,
                                            waypoints[cur].position,
                                            speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
        // Waypoint reached, select next one
        else cur = (cur + 1) % waypoints.Length;

        // Animation
     //   Vector2 dir = waypoints[cur].position - transform.position;
       // GetComponent<Animator>().SetFloat("DirX", dir.x);
      //  GetComponent<Animator>().SetFloat("DirY", dir.y);
     
    }

    public void updateGhosts(int ghosts){
        vuln = ghosts;
       ghostAnimator.SetInteger("ghostsNum", ghosts);
    }
    void OnTriggerEnter2D(Collider2D co)
    {
      //  if(vuln ==1 )
        if (co.name == "pacman"){

            Destroy(co.gameObject);
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
    }


}
