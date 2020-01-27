using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class controller : MonoBehaviour
{
    public Text scoreText;
    public Text winText;
    public float speed;                //Floating point variable to store the player's movement speed.
    private float timeLeft = 0;
    private Rigidbody2D rb2d;        //Store a reference to the Rigidbody2D component required to use 2D Physics.
    private int count;
    private int score;
    private move mover;

  

    private int ghosts;
    // Use this for initialization


    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
          mover = GameObject.FindObjectOfType<move>();
        count = 0;
        score = 0;
        SetCountText();
    //ghosts = 1;//
        winText.text = "";
        scoreText.text = "Score: " + count.ToString();
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);

          // Animation Parameters
  //  Vector2 dir = dest - (Vector2)transform.position;
    GetComponent<Animator>().SetFloat("DirX", movement.x);
    GetComponent<Animator>().SetFloat("DirY", movement.y);




    timeLeft -= Time.deltaTime;
    if(timeLeft ==0){
        ghosts = 1;
        mover.updateGhosts (ghosts);
    }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("PickUp"))
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            other.gameObject.SetActive(false);
            count = count + 1;
            score = score + 100;
            SetCountText();
        }
         if (other.gameObject.CompareTag("power"))
        {timeLeft = 20;
         other.gameObject.SetActive(false);
        ghosts = 0;
        mover.updateGhosts (ghosts);

          
        }
    }

    void SetCountText()
    {
      scoreText.text = "Score: " + score.ToString();
        if (count >= 25)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (count >= 50)
        {
            winText.text = "You Win!";
        }
    }
  
}