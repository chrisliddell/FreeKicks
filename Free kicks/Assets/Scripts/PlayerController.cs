using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int id;
    public int team;
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
		if(singleplayer){
			GameObject controller = GameObject.Find("Controller");
            controller.GetComponent<SingleplayerController>().failedShot = true;
			return;
		}
        BallController ball = collider.gameObject.GetComponent<BallController>();
        if (ball != null)
        {

            Vector3 newPos = transform.position;
            if (transform.rotation.z > 0)
            {
                newPos.x += 0.4f;
                newPos.y += 0.1f;
                ball.transform.position = newPos; //team1
            } else
            {
                newPos.x -= 0.4f;
                newPos.y += 0.1f;
                ball.transform.position = newPos; //team2
            }
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            GameObject controller = GameObject.Find("Controller");
            controller.GetComponent<GameController>().shootMode(this.gameObject);
        }
    }

}
