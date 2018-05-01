using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class bullet : NetworkBehaviour
{

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!isServer)
            return;
        var hit = col.gameObject;
        var health = hit.GetComponent<player>();
        if (health == null)
        {
            //Destroy(gameObject);
            return;
        }
        
        health.TakeDamage(20);
        health.HurtAnimator();
        Destroy(gameObject);
    }
}