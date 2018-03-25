using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class player : MonoBehaviour {
    public float moveSpeed = 5;
    Camera viewCamera;
    PlayerController controller;
    GunController gunController;
    public FixedJoystick joystick;
    public FixedJoystick gunstick;
    // Use this for initialization
    void Start () {

        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //var rigidbody = GetComponent<Rigidbody>();
        Vector3 moveInput = new Vector3(joystick.Horizontal * 100f,
                                         0,
                                         joystick.Vertical * 100f);


        /*
        Vector3 moveInput = new Vector3(joystick.Horizontal,
                                         0,
                                         joystick.Vertical);*/
// Vector3 moveInput = new Vector3(-Input.GetAxisRaw("Horizontal"), 0, -Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);

      /*  Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray,out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            controller.LookAt(point);
        }*/

        if (gunstick.Vertical != 0 && gunstick.Horizontal != 0)
        {
            var rigidbody = GetComponent<Rigidbody>();
            controller.LookAt(rigidbody.position + new Vector3(-gunstick.Horizontal, 0, -gunstick.Vertical));
            gunController.Shoot();
        }

    }
}
