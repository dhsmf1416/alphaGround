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

    public void SetupClient()
    {
        StartClient();
        myClient = new NetworkClient();
        myClient.Connect("localhost", 7777);
    }
}
