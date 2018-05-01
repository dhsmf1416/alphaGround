using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;
public class networkManager : NetworkManager
{
    private void Start()
    {
        StartMatchMaker();
    }
    public void quitClient()
    {
        Application.Quit();
    }
    public void getServer()
    {
        matchMaker.CreateMatch("roomName", 10, true, "", "", "", 0, 0, OnInternetMatchCreate);
    }
    public void getClient()
    {
        matchMaker.ListMatches(0, 10, "roomName", true, 0, 0, OnInternetMatchList);
    }
    private void OnInternetMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (success)
        {
            if (matches.Count != 0)
            {
                //Debug.Log("A list of matches was returned");

                //join the last server (just in case there are two...)
                matchMaker.JoinMatch(matches[matches.Count - 1].networkId, "", "", "", 0, 0, OnJoinInternetMatch);
            }
            else
            {
                Debug.Log("No matches in requested room!");
            }
        }
        else
        {
            Debug.LogError("Couldn't connect to match maker");
        }
    }
    private void OnJoinInternetMatch(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            //Debug.Log("Able to join a match");

            MatchInfo hostInfo = matchInfo;
            StartClient(hostInfo);
        }
        else
        {
            Debug.LogError("Join match failed");
        }
    }
    public override void OnServerConnect(NetworkConnection conn)
    {
        if(NetworkServer.connections.Count == 4)
        {
            ServerChangeScene("Main");
        }
    }
    public override void OnServerSceneChanged(string sceneName)
    {
        if (sceneName != "Main")
            return;
        List<Vector3> availList = GameObject.FindWithTag("levelmanager").GetComponent<LevelManager>().avail;
        GameObject magneticField = (GameObject)Instantiate(spawnPrefabs[1], new Vector3(151, 128, -1), Quaternion.identity);
        for (int i = 1; i < NetworkServer.connections.Count; i++)
        {
            GameObject go = (GameObject)Instantiate(playerPrefab,availList[Random.Range(0,availList.Count)], Quaternion.identity);
            NetworkServer.AddPlayerForConnection(NetworkServer.connections[i], go, 0);
        }
        NetworkServer.Spawn(magneticField);
    }
    public void OnInternetMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            //Debug.Log("Create match succeeded");

            MatchInfo hostInfo = matchInfo;
            NetworkServer.Listen(hostInfo, 9000);


            StartServer(hostInfo);

        }
        else
        {
            Debug.LogError("Create match failed");
        }
    }
}