using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
	public Vector3 startPos;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        rb = GetComponent<Rigidbody>();
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(Vector3.left * 10);
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(Vector3.right * 10);
        if (Input.GetKey(KeyCode.W))
            rb.AddForce((new Vector3(0,0,1)) * 10);
        if (Input.GetKey(KeyCode.S))
            rb.AddForce((new Vector3(0, 0, -1)) * 10);
    }

    public void resetPos()
    {
        transform.position = startPos;
        rb.constraints = RigidbodyConstraints.None;
    }

    public void goal(int team){
		transform.position = startPos;
		if(team == 1){
			
			
		} else {
			
			
		}
	}
	
		
	public void outOfBounds(int team){
		transform.position = startPos;
		if(team == 1){
			
			
		} else {
			
			
		}
	}
}
