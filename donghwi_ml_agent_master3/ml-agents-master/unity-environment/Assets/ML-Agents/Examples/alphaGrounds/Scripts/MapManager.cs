using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Use this for initialization
    void Start () {
        
    }
	public void OneDied(GameObject diedplayer)
    {
        Debug.Log(remainedPlayers);
        PlayerAgent player = diedplayer.GetComponent<PlayerAgent>();
        switch (remainedPlayers)
        {
            case 2:
                player.AddReward(-0.5f);
                break;
            case 3:
                player.AddReward(-1f);
                break;
            case 4:
                player.AddReward(-1.5f);
                break;
            case 5:
                player.AddReward(-2f);
                break;
        }
        remainedPlayers--;
        if (remainedPlayers == 1)
            gameFinish();
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
                    EnermyPosition[i] = new Vector2(99999f, 99999f);
                }
            }
            else
            {
                EnermyPosition[i] = new Vector2(99999f, 99999f);
            }
        }
        return EnermyPosition;
    }
    void gameFinish()
    {
        Debug.Log("finish");
        remainedPlayers = allPlayers;

        mag.newMagnetic();
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
            PlayerAgent pa = playerlist[i].GetComponent<PlayerAgent>();
            //playerlist[i].GetComponent<PlayerAgent>().Done();
            pa.AgentReset();
        }
    }
    // Update is called once per frame
    void Update () {
		
	}

    /*public void AllPlayersDone(float reward)
    {
        foreach (PlayerState2 ps in playerStates)
        {
            if (ps.agentScript.gameObject.activeInHierarchy)
            {
                if (reward != 0)
                {
                    ps.agentScript.AddReward(reward);
                }
                ps.agentScript.Done();
            }

        }
    }*/
}
