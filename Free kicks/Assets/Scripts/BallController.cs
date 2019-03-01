using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
	public Vector3 startPos;
	public bool singleplayer;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        setFreeze(true);
    }

    // Update is called once per frame
    void Update()
    {/*
        rb = GetComponent<Rigidbody>();
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            setFreeze(false);
            if (Input.GetKey(KeyCode.A))
                rb.AddForce(Vector3.left * 5);
            if (Input.GetKey(KeyCode.D))
                rb.AddForce(Vector3.right * 5);
            if (Input.GetKey(KeyCode.W))
                rb.AddForce((new Vector3(0, 0, 1)) * 5);
            if (Input.GetKey(KeyCode.S))
                rb.AddForce((new Vector3(0, 0, -1)) * 5);
        }*/
    }

    public void resetPos()
    {
        setFreeze(false);
        transform.position = startPos;
        setFreeze(true);
        setFreeze(false);
    }

    public void goal(int team){
		if(!singleplayer)
			GameObject.Find("Controller").GetComponent<GameController>().goal(team);
		else
			GameObject.Find("Controller").GetComponent<SingleplayerController>().scoreGoal();
	}

    public void setFreeze(bool f)
    {
        if (f)
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }

    public void updateAims()
    {
        Vector3 pos = GameObject.Find("aim1").transform.position;
        pos.z = transform.position.z;
        GameObject.Find("aim1").transform.position = pos;
        pos = GameObject.Find("aim2").transform.position;
        pos.z = transform.position.z;
        GameObject.Find("aim2").transform.position = pos;
    }
	
	public void updateAim()
    {
        Vector3 pos = GameObject.Find("aim").transform.position;
        pos.z = transform.position.z;
        GameObject.Find("aim").transform.position = pos;
    }

    public void moveTo(Vector3 pos)
    {
        transform.position = pos;
    }
}
