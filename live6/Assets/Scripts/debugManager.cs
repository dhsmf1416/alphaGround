using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;
public class debugManager : NetworkManager
{


    public override void OnStartClient(NetworkClient client)
    {
        GameObject magneticField = (GameObject)Instantiate(spawnPrefabs[1], new Vector3(151, 128, -1), Quaternion.identity);
        NetworkServer.Spawn(magneticField);
    }
}