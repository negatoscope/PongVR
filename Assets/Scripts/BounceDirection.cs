using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BounceDirection : MonoBehaviour
{
    public float bounceStrength;
    private float maxBounceAngle = 45;
    private float speed = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        Collider collider = gameObject.GetComponent<Collider>();

        if (ball != null)
        //if (collision.gameObject.tag == "Ball")
        {

            float colXExtent = collider.bounds.extents.x; 
            float xOffset = ball.transform.position.x - collider.transform.position.x;
            float xRatio = xOffset / colXExtent;
            float bounceAngle = maxBounceAngle * xRatio * Mathf.Deg2Rad;

            Vector3 bounceDirection = new Vector3(Mathf.Sin(bounceAngle), 0, Mathf.Cos(bounceAngle));
            bounceDirection.z *= Mathf.Sign(ball.rb.velocity.z); // * (bounceStrength + 100) / 100; //* -ball.rb.velocity.z;
                                                                 //bounceDirection.x *= bounceDirection.x * ball.rb.velocity.x;
                                                                 //ball.AddForce(bounceDirection);


            //ball.rb.velocity = new Vector3(Mathf.Abs(ball.rb.velocity.x) * bounceDirection.x, 0, Mathf.Abs(ball.rb.velocity.z) * bounceDirection.z);
            //ball.rb.velocity = Vector3.zero;
            //ball.AddForce(bounceDirection * bounceStrength);
            ball.rb.velocity = bounceDirection * ball.moveSpeed * 7; // when the ball hits the paddle velocity goes down 7x, dont know why
            //ball.rb.AddForce(bounceDirection * bounceStrength);
            //print(bounceDirection.x);
            print(ball.rb.velocity);

            //ball.rb.velocity = bounceDirection * 5;
            //Vector3 normal = collision.GetContact(0).normal;
            //normal.x = -bounceAngle;
            //print(bounceDirection.z);
            //ball.AddForce(-normal * bounceStrength);
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