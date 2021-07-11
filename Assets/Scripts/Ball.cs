﻿//
// PongVR
// ******
// 
// Created by Luis Eudave 10/07/21.
// 
// Based on VRPong by Kevin Thomas
//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    public Rigidbody rb;

    public float moveSpeed;
    private float originalSpeed;

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

        // Save original speed value for future use
        originalSpeed = moveSpeed;

        // Call origin function
        ReturnToOrigin();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        print(rb.velocity); // used to monitor ball behavior
    }

    //Check to see if we hit our walls or paddle
    void OnCollisionEnter(Collision collision)
    {
        // If we hit the walls
        if (collision.collider.tag == "Walls")
        {
            // We play our sound effect
            hitWallSound.Play();
        }

        // If we hit the paddles
        if (collision.collider.tag == "Paddles")
        {
            // We play our sound effect
            hitPaddleSound.Play();

            // Increment speed each time ball hits paddle
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
            scoreText.text = "You Win!";

            // We play our sound effect
            winGameSound.Play();

            // Start the restart co-routine
            StartCoroutine(Restart());
        }
        else if(rightPaddle.score >= 3)
        {
            scoreText.text = "CPU Wins!";

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

        // Restitute original speed value
        moveSpeed = originalSpeed;

        // We want to ensure x and z axis get a random
        // value upon each new score when the ball
        // returns to origin. Multiplier adds speed.
        rb.velocity = new Vector3(velocityX * moveSpeed, 0, velocityZ * moveSpeed);

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

    public void AddForce(Vector3 force)
    {
        rb.AddForce(force);
    }
}
