using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class NetManager : NetworkManager {
    NetworkClient myClient;

    public override void OnStartServer()
    {
        Debug.Log("SERVER");
    }
    public override void OnStartClient(NetworkClient client)
    {
        Debug.Log("CLIENT");
    }
    public override void OnStopClient()
    {
        Debug.Log("STOP");
    }
    public void SetupServer()
    {
        Debug.Log("SetupServer()");
        StartServer();
        NetworkServer.Listen(4444);
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);

    }

    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }
}
