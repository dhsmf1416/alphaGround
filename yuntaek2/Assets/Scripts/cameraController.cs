using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	public void moveCamera(Vector3 where)
    {
        transform.position = new Vector3(where.x, transform.position.y, where.z);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
