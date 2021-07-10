//
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

[RequireComponent(typeof(Rigidbody))]
public class RightPaddle : MonoBehaviour
{
    public int score;
    public float speed = 10;
    public bool isAI;

    private Ball ball;

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

    private float GetNewXPosition()
    {
        float result = transform.position.x;

        if (isAI)
        {
            if (ball.transform.position.z > 0)
                result = Mathf.MoveTowards(transform.position.x, ball.transform.position.x, speed * Time.deltaTime);
        }
        else
        {
            float movement = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
            result = transform.position.x + movement;
        }

        return result;
    }
}