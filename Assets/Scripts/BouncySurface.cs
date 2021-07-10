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

// Can be used on walls and paddles to avoid "english" paddle hit
public class BouncySurface : MonoBehaviour
{
    public float bounceStrength;

    private void OnCollisionEnter(Collision collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            Vector3 normal = collision.GetContact(0).normal;
            ball.AddForce(-normal * bounceStrength);
        }
    }
}
