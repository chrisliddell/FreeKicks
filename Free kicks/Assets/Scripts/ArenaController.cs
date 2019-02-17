using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
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
        BallController ball = collider.gameObject.GetComponent<BallController>();
        if (ball != null)
        {
			Debug.Log("Out of bounds!");
            ball.outOfBounds(1); //param is whos in possession of the ball
        }
    }
}
