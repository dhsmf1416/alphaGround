using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchMaker : MonoBehaviour {

    public GameObject otherButton;

    public void MatchMaking(bool isStart)
    {
        gameObject.SetActive(false);





        otherButton.SetActive(true);

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
