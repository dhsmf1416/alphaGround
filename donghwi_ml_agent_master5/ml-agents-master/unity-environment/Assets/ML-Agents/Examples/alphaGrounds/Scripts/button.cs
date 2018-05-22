using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class button : MonoBehaviour {
    private networkManager netManager;
    public GameObject Buttons;
    public GameObject Levels;
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
    public void SingleButton()
    {
        Buttons.SetActive(false);
        Levels.SetActive(true);
    }
    public void Select_one()
    {
        Levels.SetActive(false);
        SceneManager.LoadScene(1);
    }
    public void Select_two()
    {
        Levels.SetActive(false);
        SceneManager.LoadScene(1);
    }
    public void Select_three()
    {
        Levels.SetActive(false);
        SceneManager.LoadScene(1);
    }
    public void Select_four()
    {
        Levels.SetActive(false);
        SceneManager.LoadScene(1);
    }
    public void Select_five()
    {
        Levels.SetActive(false);
        SceneManager.LoadScene(1);
    }
}
