using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    GameObject ball;
    GameObject test;
    GameObject goal1;
    GameObject goal2;
    System.Random rand;
    private IEnumerator coroutine;
    public Vector3[] team1DefensePositions = { new Vector3(-34.2f, 89f, -70f), new Vector3(-116f, -455f, -57f), new Vector3(-612f, -228f, -61f) };
    public Vector3[] team2DefensePositions = { new Vector3(-330f, 82f, -76f), new Vector3(-283f, -475f, -61f), new Vector3(242f, -242f, -58f) };
    public Vector3[] team1AttackPositions = { new Vector3(-330f, 82f, -76f), new Vector3(-283f, -475f, -61f), new Vector3(242f, -242f, -58f) };
    public Vector3[] team2AttackPositions = { new Vector3(-34.2f, 89f, -70f), new Vector3(-116f, -455f, -57f), new Vector3(-612f, -228f, -61f) };
    Vector3 cameraPos;
    Quaternion cameraRot;
    float cameraFOV;
    public bool playing;
    public int stage;
    public int currentPlayer;
    public int currentAnswer;

    // Start is called before the first frame update
    void Start()
    {
        GameObject camera = GameObject.Find("Main Camera");

        goal1 = GameObject.Find("goal1");
        goal2 = GameObject.Find("goal2");
        ball = GameObject.Find("Ball");
        cameraPos = camera.transform.position;
        cameraRot = camera.transform.rotation;
        cameraFOV = Camera.main.fieldOfView;
        rand = new System.Random();
        test = GameObject.Find("Test");
        test.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        playing = true;
        stage = 0; // 0 is defending, 1 is attacking, 2 is striking 
        getPositions();
        ball.GetComponent<BallController>().resetPos();
        float r = rand.Next(0, 2);
        Debug.Log(r);
        reset();
        if (r > 0f)
        {
            Debug.Log("Player 1 starts");
            currentPlayer = 1;
            ball.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0, 0) * 1200);

        }
        else
        {
            Debug.Log("Player 2 starts");
            currentPlayer = 2;
            ball.GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0) * 1200);
			
        }
    }

    public void getPositions()
    {
        Vector3 pos;
        GameObject player;

        player = GameObject.Find("Player1A");
        pos = player.transform.position;
        team1DefensePositions[0] = pos;
        pos.x += 5f;
        team1AttackPositions[0] = pos;

        player = GameObject.Find("Player1B");
        pos = player.transform.position;
        team1DefensePositions[1] = pos;
        pos.x += 5f;
        team1AttackPositions[1] = pos;

        player = GameObject.Find("Player1C");
        pos = player.transform.position;
        team1DefensePositions[2] = pos;
        pos.x += 5f;
        team1AttackPositions[2] = pos;

        player = GameObject.Find("Player2A");
        pos = player.transform.position;
        team2DefensePositions[0] = pos;
        pos.x -= 5f;
        team2AttackPositions[0] = pos;

        player = GameObject.Find("Player2B");
        pos = player.transform.position;
        team2DefensePositions[1] = pos;
        pos.x -= 5f;
        team2AttackPositions[1] = pos;

        player = GameObject.Find("Player2C");
        pos = player.transform.position;
        team2DefensePositions[2] = pos;
        pos.x -= 54f;
        team2AttackPositions[2] = pos;

    }

    public void reset() {
        stage = 0;
        Camera.main.orthographic = true;
        Camera.main.fieldOfView = cameraFOV;
        GameObject camera = GameObject.Find("Main Camera");
        camera.transform.position = cameraPos;
        camera.transform.rotation = cameraRot; 

        GameObject ball = GameObject.Find("Ball");
        ball.GetComponent<BallController>().resetPos();

        GameObject player;
        player = GameObject.Find("Player1A");
        player.transform.position = team1DefensePositions[0];
        player.GetComponent<MeshRenderer>().enabled = true;
        player = GameObject.Find("Player1B");
        player.transform.position = team1DefensePositions[1];
        player.GetComponent<MeshRenderer>().enabled = true;
        player = GameObject.Find("Player1C");
        player.transform.position = team1DefensePositions[2];
        player.GetComponent<MeshRenderer>().enabled = true;
        player = GameObject.Find("Player2A");
        player.transform.position = team2DefensePositions[0];
        player.GetComponent<MeshRenderer>().enabled = true;
        player = GameObject.Find("Player2B");
        player.transform.position = team2DefensePositions[1];
        player.GetComponent<MeshRenderer>().enabled = true;
        player = GameObject.Find("Player2C");
        player.transform.position = team2DefensePositions[2];
        player.GetComponent<MeshRenderer>().enabled = true;
        player = GameObject.Find("Ball");
        player.GetComponent<BallController>().resetPos();

        goal1.SetActive(true);
        goal2.SetActive(true);
    }

    public void turn()
    {
        GameObject player;
        if (stage == 0)
        {
            player = GameObject.Find("Player1A");
            player.transform.position = team1DefensePositions[0];
            player = GameObject.Find("Player1B");
            player.transform.position = team1DefensePositions[1];
            player = GameObject.Find("Player1C");
            player.transform.position = team1DefensePositions[2];
            player = GameObject.Find("Player2A");
            player.transform.position = team2DefensePositions[0];
            player = GameObject.Find("Player2B");
            player.transform.position = team2DefensePositions[1];
            player = GameObject.Find("Player2C");
            player.transform.position = team2DefensePositions[2];

            showQuestion();

           

        }
        else if (currentPlayer == 1) {
                player = GameObject.Find("Player1A");
                player.transform.position = team1AttackPositions[0];
                player = GameObject.Find("Player1B");
                player.transform.position = team1AttackPositions[1];
                player = GameObject.Find("Player1C");
                player.transform.position = team1AttackPositions[2];
                player = GameObject.Find("Player2A");
                player.transform.position = team2DefensePositions[0];
                player = GameObject.Find("Player2B");
                player.transform.position = team2DefensePositions[1];
                player = GameObject.Find("Player2C");
                player.transform.position = team2DefensePositions[2];
        }
        else
        {
                player = GameObject.Find("Player1A");
                player.transform.position = team1DefensePositions[0];
                player = GameObject.Find("Player1B");
                player.transform.position = team1DefensePositions[1];
                player = GameObject.Find("Player1C");
                player.transform.position = team1DefensePositions[2];
                player = GameObject.Find("Player2A");
                player.transform.position = team2AttackPositions[0];
                player = GameObject.Find("Player2B");
                player.transform.position = team2AttackPositions[1];
                player = GameObject.Find("Player2C");
                player.transform.position = team2AttackPositions[2];
        }


        playing = false;
        currentPlayer = 1 == currentPlayer ? 2 : 1;
    }

    public void shootMode(GameObject player)
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        movePlayers(pc.id, pc.team);
        GameObject camera = GameObject.Find("Main Camera");
        if (player.transform.rotation.z > 0)
        {
            Camera.main.orthographic = false;
            Camera.main.fieldOfView = 30f;
            camera.transform.position = ball.transform.position + new Vector3(-6, 2, 0);
            camera.transform.LookAt(GameObject.Find("goal2").transform);
            player.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            Camera.main.orthographic = false;
            Camera.main.fieldOfView = 30f;
            camera.transform.position = ball.transform.position + new Vector3(6, 2, 0);
            camera.transform.LookAt(GameObject.Find("goal1").transform);
            player.GetComponent<MeshRenderer>().enabled = false;
        }
        showQuestion();
    }

    public void showQuestion()
    {
        test.SetActive(true);
        var tc = test.GetComponent<TestController>();
    }

    public void checkAnswer(int ans)
    {
        test.SetActive(false);
        if (ans == currentAnswer)
        {
            Debug.Log("Correct");
			if(stage == 0){
				if(currentPlayer == 1) //remove goal so they can see
				{
					GameObject.Find("goal1").SetActive(false);
				}
				else
				{
					GameObject.Find("goal2").SetActive(false);
				}
                StartCoroutine(Wait());

			}
        }
        else
        {
            Debug.Log("Incorrect");
            reset();
            if (currentPlayer == 1)
            {
                currentPlayer = 2;
                Debug.Log("Player 2 starts.");
                ball.GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0) * 1200);
            }
            else
            {
                currentPlayer = 1;
                Debug.Log("Player 1 starts.");
                ball.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0, 0) * 1200);
            }
        }
    }

    IEnumerator Wait()
    {
        print(Time.time);
        yield return new WaitUntil(() => shoot());
        print(Time.time);
    }

    public void movePlayers(int id, int team)
    {
        if (stage == 0) return;
        GameObject auxPlayer1, auxPlayer2;
        if (team == 1) {
            if (id == 1)
            {
                auxPlayer1 = GameObject.Find("Player1B");
                auxPlayer2 = GameObject.Find("Player1C");
            }
            else if (id == 2)
            {
                auxPlayer1 = GameObject.Find("Player1A");
                auxPlayer2 = GameObject.Find("Player1C");
            }
            else
            {
                auxPlayer1 = GameObject.Find("Player1A");
                auxPlayer2 = GameObject.Find("Player1B");
            }
            auxPlayer1.transform.position += new Vector3(3, 0, 0);
            auxPlayer2.transform.position += new Vector3(3, 0, 0);
        }
        else
        {
            if (id == 1)
            {
                auxPlayer1 = GameObject.Find("Player2B");
                auxPlayer2 = GameObject.Find("Player2C");
            }
            else if (id == 2)
            {
                auxPlayer1 = GameObject.Find("Player2A");
                auxPlayer2 = GameObject.Find("Player2C");
            }
            else
            {
                auxPlayer1 = GameObject.Find("Player2A");
                auxPlayer2 = GameObject.Find("Player2B");
            }
            auxPlayer1.transform.position -= new Vector3(3, 0, 0);
            auxPlayer2.transform.position -= new Vector3(3, 0, 0);
        }
    }

    int count = 0;
	public bool shoot(){
		if (Input.GetMouseButtonDown(0)){
            Debug.Log("Clicked");
            RaycastHit hit = new RaycastHit();
		    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var select = GameObject.Find("Ball").transform;
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.cyan, 20f, false);
            if (Physics.Raycast(ray.origin, ray.direction*100, out hit, 200f)){ // (ray, out hit, 100.0f)){
                count++;
                if(hit.transform.tag == "Ball")
                {

                    Debug.Log("Hit ball");
                }
                else
                {
                    if (count > 10) return true;
                    Debug.Log("Missed Ball");
                    return false;
                }
            }
           
            return true;
		}
        else
        {
            return false;
        }
	}
}
