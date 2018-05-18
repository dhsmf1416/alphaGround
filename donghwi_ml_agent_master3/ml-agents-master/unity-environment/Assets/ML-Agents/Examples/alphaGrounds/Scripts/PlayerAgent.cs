﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAgent : Agent
{
    private float gungap = 1f;
    private float last_gun = 0f;
    public bool isSafety = true;
    public bool alive = true;
    public int currentHealth = 100;
    public GameObject bulletPrefab;
    private Vector2 moveVelocity;
    public PlayerController controller;
    public MapManager mapM;
    //int playerIndex;
    public Rigidbody agent;
    PlayerAcademy academy;
    //public MapManager area;

    [SerializeField]
    public Transform[] spawnlist;

    [SerializeField]
    public GameObject[] playerlist;

    public override void InitializeAgent()
    {
        currentHealth = 100;
    }
    public void TakeDamage(int amount)
    {
        if (alive)
        {
            //AddReward(-amount);
            currentHealth -= amount;
            if (currentHealth <= 0 && alive)
            {
                alive = false;
                //Done();
                transform.position = new Vector3(transform.position.x + 1000f, transform.position.y + 1000f, -1f);
                mapM.OneDied(gameObject);
            }
        }
    }

    void Update()
    {
        if (!isSafety)
        {
            //TakeDamage(2);
        }
    }

    //gun shoot function
    public void shoot(float hori, float vert)
    {
        Vector2 lookPoint = new Vector2(hori, vert);
        Vector2 tempvelocity = lookPoint.normalized * 30;
        lookPoint = lookPoint.normalized + new Vector2(transform.position.x,transform.position.y);
        float bulletangle = Mathf.Atan2(vert, hori) * Mathf.Rad2Deg;

        Vector3 bulletrotation = new Vector3(0, 0, 1);
        GameObject bullet1 = Instantiate(bulletPrefab, lookPoint, Quaternion.AngleAxis(bulletangle, bulletrotation));
         
        bullet1.GetComponent<bullet>().shooter = gameObject.GetComponent<PlayerAgent>();

        bullet1.GetComponent<Rigidbody2D>().velocity = tempvelocity;
        Destroy(bullet1, 1.0f);
    }

    //action function
    /*public void MoveAgent(float[] act)
    {
        Vector2 dirToGo = Vector2.zero;
        float rotateDir = 0f;

        int action = Mathf.FloorToInt(act[0]);
        
        switch (action)
        {
            case 0:
                dirToGo = new Vector2(1, 0);
                break;
            case 1:
                dirToGo = new Vector2(-1, 0);
                break;
            case 2:
                dirToGo = new Vector2(0, 1);
                break;
            case 3:
                dirToGo = new Vector2(0, -1);
                break;
            default:
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
        //shoot(rotateDir, shootavailable);
        Vector2 moveInput = new Vector2(dirToGo.x, dirToGo.y);
        moveVelocity = moveInput.normalized * 5f;
        controller.Move(moveVelocity);
    }*/


    public override void CollectObservations()
    {
        // 1. 자기자신의 위치
        // 2. 자기가 보이는 시야에서 적의 위치
        // 3. 남은 플레이어 수
        // 4. 자기장의 중심의 위치
        // 5. 자기장의 현재 radius
        Vector2 myPosition = new Vector2 (transform.position.x, transform.position.y);
        Vector2[] EnemyPosition = mapM.relatedPlayer(myPosition);
        //int remainedPlayers = mapM.remainedPlayers;
        Vector2 MagPosition = new Vector2 (mapM.mag.CC.position.x, mapM.mag.CC.position.y);
        Vector2 MagScale = new Vector2 (mapM.mag.CC.localScale.x, mapM.mag.CC.localScale.y);
        
        float relativeX = MagPosition.x - myPosition.x;
        float relativeY = MagPosition.y - myPosition.y;
       
        /*
        Collider[] objects = Physics.OverlapSphere(transform.position, 30);
        foreach (Collider s in objects)
        {
            if (s.name == "PineTree.2(Clone)")
                continue;
            else if (s.name == "Water(Clone)")
                AddVectorObs(1);
            else
                AddVectorObs(-1);
        }
        */


        AddVectorObs(myPosition.x/200f);
        AddVectorObs(myPosition.y/200f);
        //AddVectorObs(relativeX/200f);
        //AddVectorObs(relativeY/200f);
        //AddVectorObs(MagScale.x/2000f);
        //AddVectorObs(new Vector2(relativeX, relativeY));
        for (int i = 0; i < EnemyPosition.Length; i++)
        {
            if (EnemyPosition[i].x != 0 && EnemyPosition[i].y != 0)
            {
                EnemyPosition[i].x = EnemyPosition[i].x / 100f;
                EnemyPosition[i].y = EnemyPosition[i].y / 40f;
            }
            AddVectorObs(EnemyPosition[i].x);
            AddVectorObs(EnemyPosition[i].y);
        }
        //AddVectorObs(remainedPlayers);
        if (alive)
            AddVectorObs(1);
        else
            AddVectorObs(-1);
    }

    // to be implemented by the developer 
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        /*if (alive)
        {
            MoveAgent(vectorAction);
            if (!isSafety)
            {
                SetReward(-1f);
            }
            else
            {
                SetReward(1f);
            }
        }
        if (alive)
        {
            MoveAgent(vectorAction);
            if (Mathf.FloorToInt(vectorAction[0])!=2)
            {
                SetReward(-1f);
            }
            else
            {
                SetReward(1f);
            }
        }*/
        //else
        //AddReward(-10f);

        if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
        {
            if (alive)
            {
                /*
                float action_x = 2f * Mathf.Clamp(vectorAction[0], -1f, 1f);
                float action_y = 2f * Mathf.Clamp(vectorAction[1], -1f, 1f);

                Vector2 moveInput = new Vector2(action_x, action_y);
                moveVelocity = moveInput * 5f;
                controller.Move(moveVelocity);

                Vector2 MagPosition = new Vector2(mapM.mag.CC.position.x, mapM.mag.CC.position.y);
                Vector2 myPosition = new Vector2(transform.position.x, transform.position.y);

                float distance = (MagPosition.x - myPosition.x) * (MagPosition.x - myPosition.x) + (MagPosition.y - myPosition.y) * (MagPosition.y - myPosition.y);
                float reward = distance / -40000f;
                float inreward = distance / -100000f;


                if (isSafety)
                    AddReward(inreward);
                else
                    AddReward(reward);
                    */
                
                float aim_x = 2f * Mathf.Clamp(vectorAction[0], -1f, 1f);
                float aim_y = 2f * Mathf.Clamp(vectorAction[1], -1f, 1f);


                if (Time.time > last_gun + gungap && (aim_x != 0 || aim_y != 0))
                {
                    shoot(aim_x, aim_y);
                    last_gun = Time.time;
                }
            }
            else
            {
                AddReward(-0.01f);
            }
        }
    }

    // to be implemented by the developer
    public override void AgentReset()
    {

        //shootavailable = false;
       // direction = 0;
        isSafety = true;
        alive = true;
        currentHealth = 100;
    }
    
}
