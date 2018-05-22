using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerState2
{
    public int playerIndex;
    public Rigidbody agent;
    public Vector3 startingPos;
    public PlayerAgent agentScript;
}

public class MapManager : MonoBehaviour {
    [SerializeField]
    public Transform[] spawnlist;

    [SerializeField]
    public GameObject[] playerlist;
    public List<PlayerState2> playerStates = new List<PlayerState2>();
    public Magnetic mag;
    public int allPlayers = 5;
    private int remainedPlayers = 5;
    public Text alivepeop;
    // Use this for initialization
    void Start() {
        alivepeop = GameObject.FindWithTag("alive").GetComponent<Text>();

        for (int i = 0; i < spawnlist.Length; i++)
        {
            Transform temp = spawnlist[i];
            int randomIndex = Random.Range(i, spawnlist.Length);
            spawnlist[i] = spawnlist[randomIndex];
            spawnlist[randomIndex] = temp;
        }

        for (int i = 0; i < playerlist.Length; i++)
        {
            playerlist[i].transform.position = spawnlist[i].position;
        }
    }

    public void minusalive() 
    {
        int temp = int.Parse(alivepeop.text);
        alivepeop.text = (temp-1).ToString();
    }

    public Vector2[] relatedPlayer(Vector2 myPosition)
    {
        Vector2[] EnermyPosition = new Vector2[5];
        for (int i = 0; i < playerlist.Length; i++)
        {
            float relativex = playerlist[i].transform.position.x - myPosition.x;
            float relativey = playerlist[i].transform.position.y - myPosition.y;

           if (relativex < 50f && relativex > -50f && relativey > -20f && relativey < 20f)
            {
                if (relativex != 0f || relativey != 0f)
                {
                    EnermyPosition[i] = new Vector2(relativex, relativey);
                }
                else
                {
                    EnermyPosition[i] = new Vector2(999f, 999f);
                }
            }
            else
            {
                EnermyPosition[i] = new Vector2(999f, 999f);
            }
        }
        return EnermyPosition;
    }

    public void backtothestart()
    {
        SceneManager.LoadScene(0);
    }
}
