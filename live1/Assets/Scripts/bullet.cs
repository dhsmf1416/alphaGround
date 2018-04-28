using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class bullet : NetworkBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hit = collision.gameObject;
        var health = hit.GetComponent<player>();

        health.TakeDamage(20);
        Destroy(gameObject);
    }
}