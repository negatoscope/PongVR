//
// VRPong
// ******
// 
// Created by Kevin Thomas 01/04/20.
// Modified by Kevin Thomas 01/06/20.
//
// Apache License, Version 2.0
// 
// VRPong is a Oculus Rift and Oculus Quest game that is a 
// classic Pong clone where have two paddles to which
// your left controller handles the left paddle and the
// right controller to hanldle the right paddle.  Tons
// of retro fun in this game!
//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    public Rigidbody rb;
    Collider p_collider;
    //public float speedBall = 5;
    public Vector3 velocity;
    public float moveSpeed;
    private float originalSpeed;
    public float bounceStrength;
    [SerializeField] private float maxBounceAngle = 45f;
    public LeftPaddle leftPaddle;
    public RightPaddle rightPaddle;
    public TextMesh scoreText;
    public AudioSource hitWallSound;
    public AudioSource hitPaddleSound;
    public AudioSource scoreSound;
    public AudioSource winGameSound;

    private void Awake()
    {
        // Look at current game object and call 'GetComponent'
        // off of object of type 'Rigidbody' and store in
        // rb variable as it is private as we do not want
        // other scripts to have access to its properties
        // or functionality

        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    private void Start()
    {

        // Instantiate our sound objects
        var sounds = GetComponents<AudioSource>();
        hitWallSound = sounds[0];
        hitPaddleSound = sounds[1];
        scoreSound = sounds[2];
        winGameSound = sounds[3];


        originalSpeed = moveSpeed;

        // Call origin function
        ReturnToOrigin();
        //AddStartingForce();
        //StartCoroutine(ResetPosition());
        //ResetPosition();

        //velocity = Vector3.forward * moveSpeed;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        print(rb.velocity);
        //if (leftPaddle.score >= 3)
        //{
        //    rb.velocity = Vector3.zero;
        //}
        //else if (rightPaddle.score >= 3)
        //{
        //    rb.velocity = Vector3.zero;
        //}
        //else
        //{
        //    rb.velocity = velocity;
        //}
        //rb.velocity = velocity;
    }

    //Check to see if we hit our walls or paddle
    void OnCollisionEnter(Collision collision)
    {
        // If we hit the walls
        if (collision.collider.tag == "Walls")
        {
            // We play our sound effect
            hitWallSound.Play();

            // And bounce normally
            //Bounce();
            //velocity.x = -velocity.x;
        }

        // If we hit the paddles
        if (collision.collider.tag == "Paddles")
        {
            // We play our sound effect
            hitPaddleSound.Play();

            // And bounce normally
            //BounceFromPaddle(collision.collider);
            moveSpeed = this.moveSpeed + moveSpeed / 25;

        }
    }

    // Check if we hit a goal
    private void OnTriggerEnter(Collider other)
    {
        // If we hit a goal
        if (other.tag == "Goals")
        {
            // We play our sound effect
            scoreSound.Play();
            //StartCoroutine(ResetPosition());
            //ReturnToOrigin();
        }
    }

    // Return ball to origin after a goal which we call
    // in 'Goal.cs'
    public void ReturnToOrigin()
    {
        // Check for winner and enable the UI and display winner
        // or reset game and then we stop the time scale which
        // will stop the game upon a win as we then start the
        // Unity co-routine to our countown if neither if is
        // true
        if (leftPaddle.score >= 3)
        {
            scoreText.text = "Left Player Won!";

            // We play our sound effect
            winGameSound.Play();

            // Start the restart co-routine
            StartCoroutine(Restart());
        }
        else if(rightPaddle.score >= 3)
        {
            scoreText.text = "Right Player Won!";

            // We play our sound effect
            winGameSound.Play();

            // Start the restart co-routine
            StartCoroutine(Restart());
        }
        else
        {
            // Start the countdown co-routine
            StartCoroutine(Countdown());
        }
    }

    // Create countdown co-routine
    public IEnumerator Countdown()
    {
        // We compare one to two which is 50 percent then that
        // initial outcome depending on the value will either be
        // negative four to negative seven or it will be between
        // four and seven for both the x and z axis
        int velocityX
            = Random.Range(1, 3) == 1
            ? Random.Range(-4, -7)
            : Random.Range(4, 7);

        int velocityZ
            = Random.Range(1, 3) == 1
            ? Random.Range(-4, -7)
            : Random.Range(4, 7);

        // Stop the ball
        rb.velocity = new Vector3(0, 0, 0);

        // Place ball at origin
        transform.position = new Vector3(0, 0.5f, 0);

        // Init countown to reorient players and restart game
        // as this is async as we pause execution and on the
        // next frame re-init as this will happen for three
        // seconds
        for(int i = 3; i > 0; i--)
        {
            scoreText.text = "GET READY".ToString();

            yield return new WaitForSeconds(1);
        }

        // Disable countdown text
        scoreText.text = "";

        // We want to ensure x and z axis get a random
        // value upon each new score when the ball
        // returns to origin. Multiplier adds speed.
        moveSpeed = originalSpeed;
        rb.velocity = new Vector3(velocityX * moveSpeed, 0, velocityZ * moveSpeed);
        //rb.velocity = new Vector3(velocityX, 0, velocityZ);
        //rb.velocity = Vector3.forward * moveSpeed;
        //velocity = rb.velocity;
        //yield return velocity;

        // Yield back co-routine
        yield return null;
    }

    // Create restart co-routine
    public IEnumerator Restart()
    {
        // Stop the ball
        rb.velocity = new Vector3(0, 0, 0);
        

        // Place ball at origin
        transform.position = new Vector3(0, 0.5f, 0);

        yield return new WaitForSeconds(5);

        // Yield back co-routine
        yield return null;

        // Reload game scene
        SceneManager.LoadScene
        (
            SceneManager.GetActiveScene().name
        );
    }

    public void Bounce()
    {
        //velocity = -velocity;
        velocity.x = -velocity.x;
    }

    public void BounceFromPaddle(Collider collider)
    {
        float colXExtent = collider.bounds.extents.x;
        float xOffset = transform.position.x - collider.transform.position.x;
        float xRatio = xOffset / colXExtent;
        float bounceAngle = maxBounceAngle * xRatio * Mathf.Deg2Rad;

        Vector3 bounceDirection = new Vector3(Mathf.Sin(bounceAngle), 0, Mathf.Cos(bounceAngle));

        bounceDirection.z *= Mathf.Sign(-velocity.z);

        velocity = bounceDirection * this.moveSpeed * 6; // when the ball hits the paddle velocity goes down 6x, dont know why
        print(velocity);
    }

    public void AddForce(Vector3 force)
    {
        rb.AddForce(force);
    }

    IEnumerator ResetPosition()
    {
        if (leftPaddle.score >= 3)
        {
            scoreText.text = "Left Player Won!";

            // We play our sound effect
            winGameSound.Play();

            // Start the restart co-routine
            StartCoroutine(Restart());
        }
        else if (rightPaddle.score >= 3)
        {
            scoreText.text = "Right Player Won!";

            // We play our sound effect
            winGameSound.Play();

            // Start the restart co-routine
            StartCoroutine(Restart());
        }
        else
        {

            // Start the countdown co-routine
            //rb.position = Vector3.zero;
            rb.velocity = Vector3.zero;

            // Init countown to reorient players and restart game
            // as this is async as we pause execution and on the
            // next frame re-init as this will happen for three
            // seconds
            for (int i = 3; i > 0; i--)
            {
                scoreText.text = "GET READY".ToString();

                yield return new WaitForSeconds(2);
            }

            // Disable countdown text
            scoreText.text = "";

            AddStartingForce();
        }


    }

    public void AddStartingForce()
    {
        int velocityX
            = Random.Range(1, 3) == 1
            ? Random.Range(-4, -7)
            : Random.Range(4, 7);

        int velocityZ
            = Random.Range(1, 3) == 1
            ? Random.Range(-4, -7)
            : Random.Range(4, 7);

        Vector3 direction = new Vector3(velocityX, 0, velocityZ);
        //yield return new WaitForSeconds(7);

        //rb.velocity = new Vector3(0, 0, 0);
        //velocity = new Vector3(velocityX * moveSpeed, 0, velocityZ * moveSpeed);
        velocity = direction * this.moveSpeed;
        //rb.AddForce(direction * this.moveSpeed);
        //print(direction);
    }


}
