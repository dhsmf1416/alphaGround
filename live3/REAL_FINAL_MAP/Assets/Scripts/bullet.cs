using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class bullet : NetworkBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isServer)
            return;
        var hit = collision.gameObject;
        var health = hit.GetComponent<player>();
        if (health == null)
        {
            return;
        }
        
        health.TakeDamage(20);
        health.HurtAnimator();
        Destroy(gameObject);
    }
}