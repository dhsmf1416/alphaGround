using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Random;

public class CharacterManager : MonoBehaviour {
    [SerializeField]
    public Transform[] spawnlist;

    [SerializeField]
    public GameObject[] playerlist;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < spawnlist.Length; i++)
        {
            Transform temp = spawnlist[i];
            int randomIndex = Random.Range(i, spawnlist.Length);
            spawnlist[i] = spawnlist[randomIndex];
            spawnlist[randomIndex] = temp;
        }

        for (int i = 0; i < playerlist.Length; i++)
            playerlist[i].transform.position = spawnlist[i].position;
    }


    

// Update is called once per frame
void Update () {
		
	}
}
