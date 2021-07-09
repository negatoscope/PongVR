using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncySurface : MonoBehaviour
{
    public float bounceStrength;
    private float maxBounceAngle = 45;

    private void OnCollisionEnter(Collision collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        //if (collision.gameObject.tag == "Paddles")
        {
            
            //float colXExtent = collision.collider.bounds.extents.x; 
            //float xOffset = transform.position.x - collision.collider.transform.position.x;
            //float xRatio = xOffset / colXExtent;
            //float bounceAngle = maxBounceAngle * xRatio * Mathf.Deg2Rad;

            //Vector3 bounceDirection = new Vector3(Mathf.Sin(bounceAngle), 0, Mathf.Cos(bounceAngle));
            //bounceDirection.z *= Mathf.Sign(-ball.rb.velocity.z);
            //ball.AddForce(bounceDirection * this.bounceStrength);
            
            //ball.rb.velocity = bounceDirection * 5;
            Vector3 normal = collision.GetContact(0).normal;
            //normal.x = -bounceAngle;
            //print(bounceDirection.z);
            ball.AddForce(-normal * bounceStrength);
            //print(ball.rb.velocity);

        }
        //else
        //{

        //    Vector3 normal = collision.GetContact(0).normal;
        //    //print(normal);
        //    //ball.AddForce(-normal * this.bounceStrength);
        //}

        //if (ball != null)
        //{
        //    Vector3 normal = collision.GetContact(0).normal;
        //    ball.AddForce(-normal * this.bounceStrength);
        //}
    }

}
