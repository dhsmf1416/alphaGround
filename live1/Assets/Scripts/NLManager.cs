using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class NLManager : NetworkManager
{
    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log(NetworkServer.connections.Count);
        if (NetworkServer.connections.Count >= 3 && networkSceneName == "lobby")
        {
            ServerChangeScene("InGame");
            
        }
    }
    public void AdminButton()
    {
        StartServer();

    }
    public void StartButton()
    {
        StartClient();
    }

}
