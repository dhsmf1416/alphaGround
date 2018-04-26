using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class SERVER : MonoBehaviour {
    public NetManager ourManager;
	// Use this for initialization
	void Start () {
        ourManager.SetupServer();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
