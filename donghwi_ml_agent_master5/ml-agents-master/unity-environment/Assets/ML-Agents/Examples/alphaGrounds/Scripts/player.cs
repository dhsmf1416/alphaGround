using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerController))]
public class player : MonoBehaviour {
    GameObject minipos;
    private float delayTimer = 0;
    private float magTimer = 1;
    public bool isSafety = true;
    public GameObject bulletPrefab;
    public float moveSpeed = 5;
    public float btwShotMs = 100f;
    PlayerController controller;
    public FixedJoystick joystick;
    public FixedJoystick gunstick;
    float nextShotTime;
    private Animator anim;
    public Slider healthSlider;
    GameObject mainCamera;
    public PlayerAgent pa;
    bool alive = true;
    public GameObject gameover;
    public GameObject aliveUI;
    public GameObject rank;

    void Start () {
        minipos = GameObject.FindWithTag("mini");
        anim = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        joystick = GameObject.FindWithTag("joystick").GetComponent<FixedJoystick>();
        gunstick = GameObject.FindWithTag("gunstick").GetComponent<FixedJoystick>();
        healthSlider = GameObject.FindWithTag("slider").GetComponent<Slider>();
        mainCamera = GameObject.FindWithTag("MainCamera");
        pa = gameObject.GetComponent<PlayerAgent>();
        // if(!isLocalPlayer)
        //     GameObject.DestroyImmediate(GetComponent<FixedJoystick>());
        //viewCamera = Camera.main
    }
 
    // Update is called once per frame

    
    public void HurtAnimator()
    {
        //anim.Play("Hurt", -1, 0);
    }
    void Update()
    {
        
        var rigidbody = GetComponent<Rigidbody2D>();
        Vector2 temp = new Vector2(rigidbody.position.x * 0.7611483249832844f + (-16.72782056696677f), rigidbody.position.y * 0.7601039473244426f + (-221.5443576289314f));
        Vector2 temp2 = new Vector2(temp.x * 0.4893758151165921f + 15.90383696045344f, temp.y * 0.492490617394153f + 113.9730049183103f);
        minipos.transform.position = new Vector2(temp2.x * 1.313804375805413f + 21.47708385856874f, temp2.y * 1.315609534090684f + 274.4658691206184f);
        //minipos.transform.position = new Vector2(temp2.x * 1.313804375805413f + 21.97708385856874f, temp2.y * 1.315609534090684f + 291.4658691206184f);
        
        
        healthSlider.value = pa.currentHealth;
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        Vector2 moveInput = new Vector2(joystick.Horizontal * 100f, joystick.Vertical * 100f);
        Vector2 moveVelocity = moveInput.normalized * moveSpeed * 3;

        /*
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            anim.SetBool("isRun", true);
        else
            anim.SetBool("isRun", false);
        */
            
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
            nextShotTime = Time.time + btwShotMs / 1000f;
            //var rigidbody = GetComponent<Rigidbody2D>();
            Vector2 lookPoint = new Vector2(gunstick.Horizontal, gunstick.Vertical);
            Vector2 velocity2 = lookPoint.normalized * 60;
            lookPoint = lookPoint.normalized + rigidbody.position;
            float bulletangle = Mathf.Atan2(gunstick.Vertical, gunstick.Horizontal) * Mathf.Rad2Deg;
            Fire(lookPoint, velocity2, bulletangle);
        }
    }
    void Fire(Vector2 lookPoint,Vector2 Velocity,float bulletangle)
    {
        //보는 방향에 따라 플레이어 모습 바꾸기
        Vector3 bulletrotation = new Vector3(0, 0, 1);
        GameObject bullet = Instantiate(bulletPrefab, lookPoint, Quaternion.AngleAxis(bulletangle, bulletrotation));
        bullet.GetComponent<bullet>().shooter = gameObject.GetComponent<PlayerAgent>();
        bullet.GetComponent<Rigidbody2D>().velocity = Velocity;

        Destroy(bullet, 1.3f);
    }
}
