using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerController))]
public class player : NetworkBehaviour {

    public GameObject bulletPrefab;
    public float moveSpeed = 5;
    public float btwShotMs = 1;
    PlayerController controller;
    FixedJoystick joystick;
    FixedJoystick gunstick;
    GameObject MainCamera;
    float nextShotTime;
    public int currentHealth = 100;
    Slider healthSlider;
    
    // Use this for initialization
    void Start () {
        controller = GetComponent<PlayerController>();
        joystick = GameObject.FindWithTag("joystick").GetComponent<FixedJoystick>();
        gunstick = GameObject.FindWithTag("gunstick").GetComponent<FixedJoystick>();
        healthSlider = GameObject.FindWithTag("slider").GetComponent<Slider>();
        MainCamera = GameObject.FindWithTag("MainCamera");
        // if(!isLocalPlayer)
        //     GameObject.DestroyImmediate(GetComponent<FixedJoystick>());
        //viewCamera = Camera.main
    }

    // Update is called once per frame

    public void TakeDamage(int amount)
    {
        if (!isLocalPlayer)
            return;

        currentHealth -= amount;
        healthSlider.value = currentHealth;
    }
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        MainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        Vector2 moveInput = new Vector2(joystick.Horizontal * 100f, joystick.Vertical * 100f);
        Vector2 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);
        //Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        //Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        //float rayDistance;
        /*
        if (groundPlane.Raycast(ray,out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            controller.LookAt(point);
        }
        */

        if (gunstick.Vertical != 0 && gunstick.Horizontal != 0 && Time.time > nextShotTime)
        {

            nextShotTime = Time.time + btwShotMs / 1000;
            var rigidbody = GetComponent<Rigidbody2D>();
            Vector2 lookPoint = new Vector2(gunstick.Horizontal, gunstick.Vertical);
            Vector2 velocity2 = lookPoint.normalized * 15;
            lookPoint = lookPoint.normalized+rigidbody.position;
            CmdFire(lookPoint,velocity2);
        }
    }
    [Command]
    void CmdFire(Vector2 lookPoint,Vector2 Velocity)
    {
        //보는 방향에 따라 플레이어 모습 바꾸기
        GameObject bullet = Instantiate(bulletPrefab, lookPoint, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = Velocity;

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 2.0f);
    }
}
