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

public class RightHit : MonoBehaviour
{
    public LeftPaddle LeftScore;
    private List<Vector3> positions;
    private List<Vector3> rotations;

    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector3>(); // create new empty list
        rotations = new List<Vector3>(); // create new empty list
        positions.Add(new Vector3(-3f, 0f, -6f)); // p_centerUp
        positions.Add(new Vector3(4f, -7f, -6f)); // p_centerDown
        positions.Add(new Vector3(-3f, 0f, -17f)); // p_playerUp
        positions.Add(new Vector3(-3f, -7f, -17f)); // p_playerDown
        positions.Add(new Vector3(-3f, -10f, -16f)); // p_playerPaddle
        positions.Add(new Vector3(-3f, 0f, 5f)); // p_aiUp
        positions.Add(new Vector3(-3f, -7f, 5f)); // p_aiDown
        positions.Add(new Vector3(-3f, -10f, 5f)); // p_aiPaddle
        rotations.Add(new Vector3(90f, -90f, 0f)); // r_centerUp
        rotations.Add(new Vector3(35f, -90f, 0f)); // r_centerDown
        rotations.Add(new Vector3(45f, 0f, 0f)); // r_playerUp
        rotations.Add(new Vector3(45f, 0f, 0f)); // r_playerDown
        rotations.Add(new Vector3(35f, 0f, 0f)); // r_playerPaddle
        rotations.Add(new Vector3(45f, 180f, 0f)); // r_aiUp
        rotations.Add(new Vector3(45f, 180f, 0f)); // r_aiDown
        rotations.Add(new Vector3(35f, 180f, 0f)); // r_aiPaddle
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Check if ball has touched right goal as when
    // a specific goal has been breached the opposing
    // player gets a point and we need to reset the
    // back back to the middle of the game with a
    // small pause and move in a random direction
    private void OnTriggerEnter(Collider other)
    {
        LeftScore.score++;

        // When either goal is collided with we
        // call the ball to origin wherever the
        // ball is located
        GameObject.Find("Ball").GetComponent<Ball>().ReturnToOrigin();

        // Randomize elements color each time a point is scored
        //GameObject.Find("OVRCameraR").GetComponent<Transform>().position = new Vector3(4f, -7f, -6f); 
        //GameObject.Find("OVRCameraR").GetComponent<Transform>().rotation = Quaternion.Euler(35f, -90f, 0f);
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
