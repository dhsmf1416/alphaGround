using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAgent : Agent
{

    public bool shootavailable=false;
    public float direction = 0f;
    public bool isSafety = true;
    private float delayTimer = 0;
    private float magTimer = 1;
    public bool alive = true;
    public int currentHealth = 100;
    public GameObject bulletPrefab;
    private Vector2 moveVelocity;
    public PlayerController controller;
    [SerializeField]
    public Transform[] spawnlist;

    [SerializeField]
    public GameObject[] playerlist;


    public void TakeDamage(int amount)
    {
        AddReward(-amount);
        currentHealth -= amount;
        if (currentHealth <= 0 && alive)
            alive = false;
    }

    void Update()
    {
        delayTimer += Time.deltaTime;
        if (!isSafety && delayTimer > magTimer)
        {
            TakeDamage(5);
            delayTimer = 0f;
        }
    }

    //gun shoot function
    public void shoot(float rotateDir, bool shootavailable)
    {
        direction += Mathf.Deg2Rad * rotateDir;
        
        if (shootavailable)
        {
            float hori = Mathf.Cos(direction);
            float vert = Mathf.Sin(direction);
            Vector2 lookPoint = new Vector2(hori, vert);
            Vector2 tempvelocity = lookPoint.normalized * 30;
            lookPoint = lookPoint.normalized + new Vector2(transform.position.x,transform.position.y);
            float bulletangle = Mathf.Atan2(vert, hori) * Mathf.Rad2Deg;

            Vector3 bulletrotation = new Vector3(0, 0, 1);
            GameObject bullet1 = Instantiate(bulletPrefab, lookPoint, Quaternion.AngleAxis(bulletangle, bulletrotation));
         
            bullet1.GetComponent<bullet>().shooter = gameObject.GetComponent<PlayerAgent>();

            bullet1.GetComponent<Rigidbody2D>().velocity = tempvelocity;
            Destroy(bullet1, 2.0f);

            shootavailable = false;
        }
    }

    //action function
    public void MoveAgent(float[] act)
    {
        Vector3 dirToGo = Vector3.zero;
        float rotateDir = 0;

        int action = Mathf.FloorToInt(act[0]);
        
        switch (action)
        {
            case 0:
                dirToGo = transform.up * 1f;
                break;
            case 1:
                dirToGo = transform.up * -1f;
                break;
            case 2:
                dirToGo = transform.right * -1;
                break;
            case 3:
                dirToGo = transform.right * 1;
                break;
            case 4:
                rotateDir = 5f;
                break;
            case 5:
                rotateDir = -5f;
                break;
            case 6:
                break;
            case 7:
                shootavailable = true;
                break;
            default:
                break;
        }
        shoot(rotateDir, shootavailable);
        Vector2 moveInput = new Vector2(dirToGo.x, dirToGo.y);
        moveVelocity = moveInput.normalized * 100f;
        controller.Move(moveVelocity);
    }


    public override void CollectObservations()
    {
        
    }

    // to be implemented by the developer 
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        AddReward(1f / 3000f);
        MoveAgent(vectorAction);
    }

    // to be implemented by the developer
    public override void AgentReset()
    {
        for (int i = 0; i < spawnlist.Length; i++)
        {
            Transform temp = spawnlist[i];
            int randomIndex = Random.Range(i, spawnlist.Length);
            spawnlist[i] = spawnlist[randomIndex];
            spawnlist[randomIndex] = temp;
        }

        for (int i = 0; i < playerlist.Length; i++)
            playerlist[i].transform.position = spawnlist[i].position;

        shootavailable = false;
        direction = 0;
        isSafety = true;
        delayTimer = 0;
        magTimer = 1;
        alive = true;
        currentHealth = 100;
    }
    
}
