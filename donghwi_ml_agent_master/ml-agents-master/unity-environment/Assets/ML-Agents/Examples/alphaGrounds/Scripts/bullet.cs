using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public PlayerAgent shooter;


    private void OnTriggerEnter2D(Collider2D col)
    {
        var hit = col.gameObject;

        

        if (hit.tag == "tree")
        {
            Destroy(gameObject);
            return;
        }

        if (hit.tag == "player")
        {
            PlayerAgent health = hit.GetComponent<PlayerAgent>();
            health.TakeDamage(7);
            Destroy(gameObject);
            shooter.AddReward(10);
        }

        return;
        
    }
}