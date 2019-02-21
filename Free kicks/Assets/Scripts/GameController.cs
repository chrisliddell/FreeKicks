using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    GameObject ball;
    GameObject test;
    GameObject goal1;
    GameObject goal2;
    public GameObject playerWithBall;
    public Slider powerSlider;
    System.Random rand;
    private IEnumerator coroutine;
    public Vector3[] team1DefensePositions = { new Vector3(-34.2f, 89f, -70f), new Vector3(-116f, -455f, -57f), new Vector3(-612f, -228f, -61f) };
    public Vector3[] team2DefensePositions = { new Vector3(-330f, 82f, -76f), new Vector3(-283f, -475f, -61f), new Vector3(242f, -242f, -58f) };
    public Vector3[] team1AttackPositions = { new Vector3(-330f, 82f, -76f), new Vector3(-283f, -475f, -61f), new Vector3(242f, -242f, -58f) };
    public Vector3[] team2AttackPositions = { new Vector3(-34.2f, 89f, -70f), new Vector3(-116f, -455f, -57f), new Vector3(-612f, -228f, -61f) };
    Vector3 cameraPos;
    Quaternion cameraRot;
    float cameraFOV;
    public float power = 300f;
    public float maxPower = 1000f;
    public float timePressed = 0f;
    public bool playing, passSuccesful;
    public int stage;
    public int currentPlayer;
    public int currentAnswer;
    public float startForce = 300f;

    // Start is called before the first frame update
    void Start()
    {
        passSuccesful = false;
        powerSlider.gameObject.SetActive(false);
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
        if (r < 0f)
        {
            currentPlayer = 1;

        }
        else
        {
            currentPlayer = 2;
        }
        StartCoroutine(startDelay());
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
        StopAllCoroutines();
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
        if (player.GetComponent<PlayerController>().team != currentPlayer)
        {
            Debug.Log("Passed to a player of another team.");
            passSuccesful = false;
            return;
        }
        passSuccesful = true;
        playerWithBall.gameObject.SetActive(true);
        playerWithBall = player;
        PlayerController pc = player.GetComponent<PlayerController>();
        movePlayers(pc.id, pc.team);
        GameObject camera = GameObject.Find("Main Camera");
        if (player.transform.rotation.z > 0)
        {
            Camera.main.orthographic = false;
            Camera.main.fieldOfView = 30f;
            camera.transform.position = ball.transform.position + new Vector3(-6, 2, 0);
            camera.transform.LookAt(goal2.transform);
        }
        else
        {
            Camera.main.orthographic = false;
            Camera.main.fieldOfView = 30f;
            camera.transform.position = ball.transform.position + new Vector3(6, 2, 0);
            camera.transform.LookAt(goal1.transform);
        }
        playerWithBall.gameObject.SetActive(false);
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
					goal1.SetActive(false);
				}
				else
				{
					goal2.SetActive(false);
				}
                StartCoroutine(Wait());
            }
        }
        else
        {
            changeTurns();

        }
    }

    public void changeTurns()
    {
        playerWithBall.gameObject.SetActive(true);
        Debug.Log("Incorrect");
        reset();
        StartCoroutine(startDelay());
    }

    IEnumerator startDelay()
    {
        timePressed = Time.time;
        yield return new WaitForSeconds(1);
        if (currentPlayer == 1)
        {
            currentPlayer = 2;
            Debug.Log("Player 2 starts.");
            ball.GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0) * startForce);
        }
        else
        {
            currentPlayer = 1;
            Debug.Log("Player 1 starts.");
            ball.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0, 0) * startForce);
        }
    }

    IEnumerator Wait()
    {
        print(Time.time);
        yield return new WaitUntil(() => shoot());
        test.SetActive(false);
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
    
	public bool shoot(){
		if (Input.GetMouseButtonDown(0)){
            Debug.Log("Clicked");
            RaycastHit hit = new RaycastHit();
		    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var select = GameObject.Find("Ball").transform;
            Vector3 dir = ((select.position + ray.direction * 150f) - ray.origin).normalized * 6.2f;
            Debug.DrawRay(ray.origin, dir, Color.cyan, 200f, true);
            if (Physics.Raycast(ray.origin, dir, out hit, 200f)){
                    if (hit.collider.tag == "Ball")
                    {
                        Debug.Log("Hit ball");
                        powerSlider.gameObject.SetActive(true);
                        StartCoroutine(HoldClick(dir, hit.point));
                   
                }
                else
                {
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

    IEnumerator HoldClick(Vector3 dir, Vector3 hitPoint)
    {
        timePressed = Time.time;
        yield return new WaitUntil(() => releasedClick());
        timePressed = Time.time - timePressed;
        Vector3 strike = Vector3.Reflect(dir.normalized, Vector3.up);
        Debug.Log("Time pressed: " + timePressed + " Power: " + timePressed * power);
        strike.z += (ball.transform.position.z - hitPoint.z) * 5; //5is constant of angularity
        if (timePressed * power > maxPower)
            strike *= maxPower;
        else
            strike *= timePressed * power;
        Debug.DrawRay(hitPoint, strike, Color.red, 200f, true);
        ball.GetComponent<BallController>().setFreeze(false);
        ball.GetComponent<Rigidbody>().AddForceAtPosition(strike, hitPoint);
        powerSlider.value = 0;
        powerSlider.gameObject.SetActive(false);
		passSuccesful = false;
        StartCoroutine(timer());
    }

    public bool releasedClick()
    {
        //Debug.Log("Delta time:" + Time.deltaTime);
        powerSlider.value += 10;
        return Input.GetMouseButtonUp(0);
    }

    IEnumerator timer()
    {
        timePressed = Time.time;
        yield return new WaitForSeconds(5);
		Debug.Log(passSuccesful);
        if (passSuccesful)
        {
            Debug.Log("Pass succesful");
        }
        else
        {
            Debug.Log("Failed pass");
            changeTurns();
        }
    }
	
	public void goal(int team)
	{
		passSuccesful = true;
		changeTurns();
	}
}
