using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerController))]
public class player : NetworkBehaviour {

    public GameObject bulletPrefab;
    public float moveSpeed = 5;
    public float btwShotMs = 10000;
    PlayerController controller;
    FixedJoystick joystick;
    FixedJoystick gunstick;
    float nextShotTime;
    public int currentHealth = 100;
    public int prevHealth;
    Slider healthSlider;
    GameObject myCamera;
    private Animator anim;
    public bool isDead = false;

    // Use this for initialization
    void Start () {
        myCamera = GameObject.FindWithTag("MainCamera");

        anim = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        joystick = GameObject.FindWithTag("joystick").GetComponent<FixedJoystick>();
        gunstick = GameObject.FindWithTag("gunstick").GetComponent<FixedJoystick>();
        healthSlider = GameObject.FindWithTag("slider").GetComponent<Slider>();


        // if(!isLocalPlayer)
        //     GameObject.DestroyImmediate(GetComponent<FixedJoystick>());
        //viewCamera = Camera.main
    }

    // Update is called once per frame

    public void TakeDamage(int amount)
    {
        if (!isLocalPlayer || isDead)
            return;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
    }

    public void HurtAnimator()
    {
        anim.Play("Hurt", -1, 0);
        if (currentHealth <= 0 && !isDead)
            Death();
    }

    public void Death()
    {
        isDead = true;
        anim.SetTrigger("Die");

        // joystick, gunstick disable
    }
   
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        myCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);


        Vector2 moveInput = new Vector2(joystick.Horizontal * 100f, joystick.Vertical * 100f);
        Vector2 moveVelocity = moveInput.normalized * moveSpeed;
        if (joystick.Horizontal != 0 && joystick.Vertical != 0)
            anim.SetBool("IsRun", true);
        else
            anim.SetBool("IsRun", false);
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

            nextShotTime = Time.time + 0.25f;
            var rigidbody = GetComponent<Rigidbody2D>();
            Vector2 lookPoint = new Vector2(gunstick.Horizontal, gunstick.Vertical);
            Vector2 velocity2 = lookPoint.normalized * 15;
            lookPoint = lookPoint.normalized+rigidbody.position;
            float bulletangle = Mathf.Atan2(gunstick.Vertical, gunstick.Horizontal) * Mathf.Rad2Deg;
            CmdFire(lookPoint,velocity2, bulletangle);
        }
    }
    [Command]
    void CmdFire(Vector2 lookPoint,Vector2 Velocity, float bulletangle)
    {
        //보는 방향에 따라 플레이어 모습 바꾸기

        Vector3 bulletrotation = new Vector3(0, 0, 1);
        GameObject bullet = Instantiate(bulletPrefab, lookPoint, Quaternion.AngleAxis(bulletangle, bulletrotation));
        bullet.GetComponent<Rigidbody2D>().velocity = Velocity;

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 2.0f);
    }
}
