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

[RequireComponent(typeof(Rigidbody))]
public class RightPaddle : MonoBehaviour
{
    public int score;
    public float speedRPaddle = 5;
    public bool isAI;

    private Ball ball;

    private Vector3 forwardDirection;

    public enum Side { Left, Right }
    [SerializeField] private Side side;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Look at current game object and call 'GetComponent'
        // off of object of type 'Rigidbody' and store in
        // rb variable as it is private as we do not want
        // other scripts to have access to its properties
        // or functionality
        rb = GetComponent<Rigidbody>();
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();

        if (side == Side.Left)
            forwardDirection = Vector3.back;
        else if (side == Side.Right)
            forwardDirection = Vector3.forward;
    }

    private void Update()
    {
        float targetXPosition = GetNewXPosition();

        ClampPosition(ref targetXPosition);

        transform.position = new Vector3(targetXPosition, transform.position.y, transform.position.z);
    }

    private void ClampPosition(ref float xPosition)
    {
        // Corresponds to the distance between Top and Bottom Walls.
        float minX = -4;
        float maxX = 4;

        xPosition = Mathf.Clamp(xPosition, minX, maxX);
    }


    // Update is called frame rate independently
    //void FixedUpdate()
    //{
    //    // If we press left we are moving the left paddle
    //    // on the x axis and we add force to our object
    //    // with Vector3 and to move left we move negative
    //    // 'speed' meters per second on the x axis and the
    //    // reverse for right
    //    if (OVRInput.Get(OVRInput.Button.Two))
    //    {
    //        rb.MovePosition
    //        (
    //            transform.position + Vector3.right * -speed * Time.deltaTime
    //        );
    //    }
    //    else if (OVRInput.Get(OVRInput.Button.One))
    //    {
    //        rb.MovePosition
    //        (
    //            transform.position + Vector3.right * speed * Time.deltaTime
    //        );
    //    }
    //}

    private float GetNewXPosition()
    {
        float result = transform.position.x;

        if (isAI)
        {
            //if(BallIncoming())
            if (ball.transform.position.z > 0)
                result = Mathf.MoveTowards(transform.position.x, ball.transform.position.x, speedRPaddle * Time.deltaTime);
        }
        else
        {
            float movement = Input.GetAxisRaw("Vertical") * speedRPaddle * Time.deltaTime;
            result = transform.position.x + movement;
        }

        return result;
    }

    //private bool BallIncoming()
    //{
    //    float dotP = Vector3.Dot(ball.velocity, forwardDirection);
    //    return dotP < 0f;
    //}
}