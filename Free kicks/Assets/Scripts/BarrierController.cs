using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    public GameObject barrierArea;
    public GameObject striker;
    public GameObject[] players;
    public bool isActive;
    public int maxSize = 5;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSize(int n)
    {
        if (n == 0)
        {
            isActive = false;
        }
        else
        {
            isActive = true;
            Vector3 pos = players[0].transform.position;
            for (int i = 1; i < players.Length; i++) //clean array
            {
                if (players[i] != null) { 
                    Destroy(players[i]);
                    Debug.Log("Destroying player #" + i);
                }
            }
            for (int i = 1; i < n; i++)
            {
                Debug.Log("Creating player #" + i);
                pos.z += 0.8f;
                players[i] = Instantiate(players[0], pos, players[0].transform.rotation, transform);
            }
        }
    }

    public void moveBarrier()
    {
        transform.position = startPos;
        Vector3 pos = new Vector3(transform.position.x + Random.Range(1, -1), startPos.y, striker.transform.position.z + Random.Range(0.5f, -0.5f));
        transform.position = pos;
        Debug.Log("Barrier position: " + pos);
    }
}
