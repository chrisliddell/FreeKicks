using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerController : MonoBehaviour
{
    public GameObject strikerArea;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveStriker()
    {
        transform.position = startPos;
        Mesh planeMesh = strikerArea.GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;

        float minX = strikerArea.transform.position.x - strikerArea.transform.localScale.x * bounds.size.x * 0.5f;
        float minZ = strikerArea.transform.position.z - strikerArea.transform.localScale.z * bounds.size.z * 0.5f;
        Vector3 pos = new Vector3(Random.Range(minX, -minX), strikerArea.transform.position.y, Random.Range(minZ, -minZ));
        transform.position = pos;
        Debug.Log("Striker position: " + pos);
;    }

    void OnTriggerEnter(Collider collider)
    {
        BallController ball = collider.gameObject.GetComponent<BallController>();
        if (ball != null)
        {

            Vector3 newPos = transform.position;
            if (transform.rotation.z > 0)
            {
                newPos.x += 0.4f;
                newPos.y += 0.1f;
                ball.transform.position = newPos; //team1
            }
            else
            {
                newPos.x -= 0.4f;
                newPos.y += 0.1f;
                ball.transform.position = newPos; //team2
            }
            Rigidbody rb = ball.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            GameObject controller = GameObject.Find("Controller");
            controller.GetComponent<SingleplayerController>().shootMode(this.gameObject);
        }
    }

    public void getBall(GameObject ball)
    {
        Vector3 newPos = transform.position;
        if (transform.rotation.z > 0)
        {
            newPos.x += 0.4f;
            newPos.y += 0.1f;
            ball.transform.position = newPos; //team1
        }
        else
        {
            newPos.x -= 0.4f;
            newPos.y += 0.1f;
            ball.transform.position = newPos; //team2
        }
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        GameObject controller = GameObject.Find("Controller");
        controller.GetComponent<SingleplayerController>().shootMode(this.gameObject);
    }
}
