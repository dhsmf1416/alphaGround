using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    public Transform CC;
    public float speed = 5f;
    [SerializeField]
    public GameObject[] RandomInitialPosition;

    //GameObject minicirclepos;
    public void newMagnetic()
    {
        CC.localScale = new Vector3(3000f, 3000f, 1f);
        for (int i = 0; i < RandomInitialPosition.Length; i++)
        {
            GameObject temp = RandomInitialPosition[i];
            int randomIndex = Random.Range(i, RandomInitialPosition.Length);
            RandomInitialPosition[i] = RandomInitialPosition[randomIndex];
            RandomInitialPosition[randomIndex] = temp;
        }

        transform.position = RandomInitialPosition[0].transform.position;
        //minicirclepos = GameObject.FindWithTag("minicircle");
    }
    private void Start()
    {
        newMagnetic();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var isPlayer = collision.gameObject.GetComponent<PlayerAgent>();
        if (isPlayer == null)
            return;
        isPlayer.isSafety = true;
 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var isPlayer = collision.gameObject.GetComponent<PlayerAgent>();
        if (isPlayer == null)
            return;
        isPlayer.isSafety = false;
    }

    // Update is called once per frame
    void Update () {
        if (CC.localScale.x > 0f && CC.localScale.y > 0f)
            CC.localScale = CC.localScale - new Vector3(speed, speed, 0);
        
        //Update minimap circle
        //Vector2 temp = new Vector2(CC.position.x * 0.7611483249832844f + (-16.72782056696677f), CC.position.y * 0.7601039473244426f + (-221.5443576289314f));
        //Vector2 temp2 = new Vector2(temp.x * 0.4893758151165921f + 15.90383696045344f, temp.y * 0.492490617394153f + 113.9730049183103f);
        //minicirclepos.transform.position = new Vector2(temp2.x * 1.313804375805413f + 21.97708385856874f, temp2.y * 1.315609534090684f + 291.4658691206184f);

        //minicirclepos.transform.localScale = CC.localScale * 13f / 30f ;

    }
}
