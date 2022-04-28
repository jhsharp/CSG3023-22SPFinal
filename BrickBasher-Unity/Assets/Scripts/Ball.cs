/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: Jacob Sharp
 * Last Edited: April 28, 2022
 * 
 * Description: Controls the ball and sets up the intial game behaviors. 
****/

/*** Using Namespaces ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ball : MonoBehaviour
{
    [Header("General Settings")]
    public int numBalls;
    public int score;
    public Text ballTxt;
    public Text scoreTxt;

    [Header("Ball Settings")]
    public float initialForceY;
    private Vector3 initialForceVector;
    public float speed;
    public GameObject paddle;
    
    private bool isInPlay;
    private Rigidbody rb;
    private AudioSource audioSource;





    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        initialForceVector = new Vector3(0, initialForceY, 0);
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }//end Awake()


        // Start is called before the first frame update
        void Start()
    {
        SetStartingPos(); //set the starting position

    }//end Start()


    // Update is called once per frame
    void Update()
    {
        // update text
        ballTxt.text = "Balls: " + numBalls;
        scoreTxt.text = "Score: " + score;

        // check if ball isn't in play
        if (!isInPlay)
        {
            // move with paddle
            Vector3 newPos = transform.position;
            newPos.x = paddle.transform.position.x;
            transform.position = newPos;

            // launch ball when space is pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isInPlay = true;
                Move();
            }
        }
    }//end Update()


    private void LateUpdate()
    {
        if (isInPlay)
        {
            rb.velocity = speed * rb.velocity.normalized;
        }
    }//end LateUpdate()


    void SetStartingPos()
    {
        isInPlay = false;//ball is not in play
        rb.velocity = Vector3.zero;//set velocity to keep ball stationary

        Vector3 pos = new Vector3();
        pos.x = paddle.transform.position.x; //x position of paddel
        pos.y = paddle.transform.position.y + paddle.transform.localScale.y; //Y position of paddle plus it's height

        transform.position = pos;//set starting position of the ball 
    }//end SetStartingPos()

    private void Move() 
    {
        // launch the ball with the initial force
        rb.AddForce(initialForceVector);
    }

    private void OnCollisionEnter(Collision col)
    {
        // play audio
        audioSource.Play();

        // find collided object
        GameObject obj = col.gameObject;

        // destroy collided bricks and score
        if (obj.tag == "Brick")
        {
            score += 100;
            Destroy(obj);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // decrement ball count when out of bounds and reset the position
        if (other.tag == "OutBounds")
        {
            numBalls--;
            if (numBalls > 0) Invoke("SetStartingPos", 2);
        }
    }
}
