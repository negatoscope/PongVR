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

public class LeftHit : MonoBehaviour
{
    public RightPaddle RightScore;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Check if ball has touched left goal as when
    // a specific goal has been breached the opposing
    // player gets a point and we need to reset the
    // back back to the middle of the game with a
    // small pause and move in a random direction
    private void OnTriggerEnter(Collider other)
    {
        RightScore.score++;

        // When either goal is collided with we
        // call the ball to origin wherever the
        // ball is located
        GameObject.Find("Ball").GetComponent<Ball>().ReturnToOrigin();

        // Randomize elements color each time a point is scored
        GameObject.Find("CenterEyeAnchor").GetComponent<Camera>().backgroundColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        GameObject.Find("Floor").GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        GameObject.Find("TopWall").GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        GameObject.Find("BottomWall").GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        GameObject.Find("LeftGoal").GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        GameObject.Find("RightGoal").GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        GameObject.Find("LeftPaddle").GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        GameObject.Find("RightPaddle").GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
