using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Magnetic : NetworkBehaviour {
    public Transform CC;
    public float speed = 0.002f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var isPlayer = collision.gameObject.GetComponent<player>();
        if (isPlayer == null)
            return;
        isPlayer.isSafety = true;
 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var isPlayer = collision.gameObject.GetComponent<player>();
        if (isPlayer == null)
            return;
        isPlayer.isSafety = false;
    }

    // Update is called once per frame
    void Update () {
        CC.localScale = CC.localScale - new Vector3(speed, speed, 0);
	}
}
