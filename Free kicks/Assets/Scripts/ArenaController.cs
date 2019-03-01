using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
	public bool singleplayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
		
	void OnTriggerEnter(Collider collider)
    {
        BallController ball = collider.GetComponent<BallController>();
        if (ball != null)
        {
            ball.resetPos();
            ball.setFreeze(true);
            Debug.Log("Out of bounds!");
			if(!singleplayer)
				GameObject.Find("Controller").GetComponent<GameController>().changeTurns();
        }
    }
}
