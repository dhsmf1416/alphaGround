using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class PlayerController : MonoBehaviour
{ 
    Vector2 velocity;
    Rigidbody2D myRigidbody;
// Use this for initialization
void Start () {
       myRigidbody = GetComponent<Rigidbody2D>();
        
	}
    public void LookAt(Vector2 lookPoint)
    {
        Vector2 heightCorrectedPoint = new Vector2(lookPoint.x,lookPoint.y);
        //transform.LookAt(heightCorrectedPoint);
    }
    public void Move(Vector2 _velocity) {
        velocity = _velocity;
    }

    public void FixedUpdate() {
      myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);
      //  maincamera.moveCamera(myRigidbody.position);
    }
}
