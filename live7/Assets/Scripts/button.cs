using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
public class button : Button {
    private networkManager netManager;
	// Use this for initialization
	void Start () {
        netManager = GameObject.FindObjectOfType<networkManager>();
    }
    public void QuitButton()
    {
        gameObject.SetActive(false);
        Application.Quit();
    }
    public void CancelButton()
    {
        gameObject.SetActive(false);
        netManager.StopClient();
        netManager.StartMatchMaker();
    }
    public void ServerButton()
    {
        gameObject.SetActive(false);
        netManager.getServer();

    }
    public void ClientButton()
    {
        gameObject.SetActive(false);
        netManager.getClient();
    }
}
