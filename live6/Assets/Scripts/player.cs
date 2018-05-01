using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerController))]
public class player : NetworkBehaviour {
    Text alivepeop;
    private float delayTimer = 0;
    private float magTimer = 1;
    public bool isSafety = true;
    public GameObject bulletPrefab;
    public float moveSpeed = 5;
    public float btwShotMs = 1;
    PlayerController controller;
    FixedJoystick joystick;
    FixedJoystick gunstick;
    float nextShotTime;
    private Animator anim;
    Slider healthSlider;
    GameObject mainCamera;
    bool alive = true;
    [SyncVar]
    public int currentHealth = 100;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        joystick = GameObject.FindWithTag("joystick").GetComponent<FixedJoystick>();
        gunstick = GameObject.FindWithTag("gunstick").GetComponent<FixedJoystick>();
        healthSlider = GameObject.FindWithTag("slider").GetComponent<Slider>();
        mainCamera = GameObject.FindWithTag("MainCamera");
        alivepeop = GameObject.FindWithTag("alive").GetComponent<Text>();
        // if(!isLocalPlayer)
        //     GameObject.DestroyImmediate(GetComponent<FixedJoystick>());
        //viewCamera = Camera.main
    }
 
    // Update is called once per frame

    public void TakeDamage(int amount)
    {

        currentHealth -= amount;
        if(currentHealth <= 0 && alive)
        {
            joystick.transform.position = new Vector3(100000, 10000, 0);
            gunstick.transform.position = new Vector3(100000, 10000, 0);
            NetworkServer.UnSpawn(gameObject);
            Destroy(gameObject);
        }
    }
    public void HurtAnimator()
    {
        anim.Play("Hurt", -1, 0);
    }
    void Update()
    {
        delayTimer += Time.deltaTime;
        if (isServer && !isSafety && delayTimer > magTimer)
        {
            TakeDamage(5);
            delayTimer = 0f;
        }
        if (!isLocalPlayer)
        {
            return;
        }
        alivepeop.text = GameObject.FindGameObjectsWithTag("player").Length.ToString();
        healthSlider.value = currentHealth;
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        Vector2 moveInput = new Vector2(joystick.Horizontal * 100f, joystick.Vertical * 100f);
        Vector2 moveVelocity = moveInput.normalized * moveSpeed;
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            anim.SetBool("isRun", true);
        else
            anim.SetBool("isRun", false);

            
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
            Vector2 velocity2 = lookPoint.normalized * 30;
            lookPoint = lookPoint.normalized + rigidbody.position;
            float bulletangle = Mathf.Atan2(gunstick.Vertical, gunstick.Horizontal) * Mathf.Rad2Deg;
            CmdFire(lookPoint, velocity2, bulletangle);
        }
    }
    [Command]
    void CmdFire(Vector2 lookPoint,Vector2 Velocity,float bulletangle)
    {
        //보는 방향에 따라 플레이어 모습 바꾸기
        Vector3 bulletrotation = new Vector3(0, 0, 1);
        GameObject bullet = Instantiate(bulletPrefab, lookPoint, Quaternion.AngleAxis(bulletangle, bulletrotation));
        bullet.GetComponent<Rigidbody2D>().velocity = Velocity;

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 2.0f);
    }
}
