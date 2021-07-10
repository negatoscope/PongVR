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
public class LeftPaddle : MonoBehaviour
{
    public int score;
    public float speed = 10;

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
    }

    // Update is called frame rate independently
    void FixedUpdate()
    {
        // If we press left we are moving the left paddle
        // on the x axis and we add force to our object
        // with Vector3 and to move left we move negative
        // 'speed' meters per second on the x axis and the
        // reverse for right
        if(OVRInput.Get(OVRInput.RawButton.Y) || Input.GetKey("up"))
        {
            rb.MovePosition
            (
                transform.position + Vector3.right * -speed * Time.deltaTime
            );
        }
        else if(OVRInput.Get(OVRInput.RawButton.X) || Input.GetKey("down"))
        {
            rb.MovePosition
            (
                transform.position + Vector3.right * speed * Time.deltaTime
            );
        }
    }
}
