using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetector2Controller : MonoBehaviour
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
			Debug.Log("Goal for team 1!");
            ball.goal(1);
        }
    }
}
