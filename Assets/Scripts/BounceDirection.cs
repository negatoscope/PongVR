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


public class BounceDirection : MonoBehaviour
{
    public float maxBounceAngle = 45;

    private void OnCollisionEnter(Collision collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        Collider collider = gameObject.GetComponent<Collider>();

        if (ball != null)
        {
            // Calculate bounce angle depending on where ball hits the paddle 
            float colXExtent = collider.bounds.extents.x; 
            float xOffset = ball.transform.position.x - collider.transform.position.x;
            float xRatio = xOffset / colXExtent;
            float bounceAngle = maxBounceAngle * xRatio * Mathf.Deg2Rad;

            Vector3 bounceDirection = new Vector3(Mathf.Sin(bounceAngle), 0, Mathf.Cos(bounceAngle));
            bounceDirection.z *= Mathf.Sign(ball.rb.velocity.z); 

            // Add new direction to rb.velocity
            ball.rb.velocity = bounceDirection * ball.moveSpeed * 7; // when the ball hits the paddle velocity goes down 7x, dont know why
        }
    }
}