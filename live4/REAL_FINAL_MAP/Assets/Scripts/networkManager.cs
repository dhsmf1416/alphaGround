using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
public class networkManager : NetworkManager
{
    private void Start()
    {
        StartMatchMaker();
    }
    public void ServerButton(GameObject button)
    {
        button.SetActive(false);
        matchMaker.CreateMatch("roomName", 4, true, "", "", "", 0, 0, OnInternetMatchCreate);
       
    }
    public void ClientButton(GameObject button)
    {
        button.SetActive(false);
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