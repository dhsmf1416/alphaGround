using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour {
    public Transform CC;
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
        CC.localScale = CC.localScale - new Vector3(0.02f, 0.02f, 0);
	}
}
