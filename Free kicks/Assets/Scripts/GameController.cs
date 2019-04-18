using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
	Fraction goalsScored1;
	Fraction correctAnswers1;
	Fraction goalsScored2;
	Fraction correctAnswers2;
    GameObject ball;
    GameObject test;
    GameObject goal1;
    GameObject goal2;
	public GameObject aimAssist;
	public GameObject scoreP1;
    public GameObject scoreP2;
    public GameObject playerWithBall;
    public GameObject enemyDefender;
	public GameObject endPanel;
    public Slider powerSlider;
	public GameObject questionsLeft;
    System.Random rand;
    private IEnumerator coroutine;
    public Vector3[] team1DefensePositions = { new Vector3(-34.2f, 89f, -70f), new Vector3(-116f, -455f, -57f), new Vector3(-612f, -228f, -61f) };
    public Vector3[] team2DefensePositions = { new Vector3(-330f, 82f, -76f), new Vector3(-283f, -475f, -61f), new Vector3(242f, -242f, -58f) };
    public Vector3[] team1AttackPositions = { new Vector3(-330f, 82f, -76f), new Vector3(-283f, -475f, -61f), new Vector3(242f, -242f, -58f) };
    public Vector3[] team2AttackPositions = { new Vector3(-34.2f, 89f, -70f), new Vector3(-116f, -455f, -57f), new Vector3(-612f, -228f, -61f) };
    Vector3 cameraPos;
    Quaternion cameraRot;
    float cameraFOV;
	public GameObject[] players;
    public float power = 300f;
    public float maxPower = 1000f;
    public float timePressed = 0f;
    public bool playing, passSuccesful;
    public int stage;
    public int currentPlayer;
    public float startForce = 300f;
	public bool scoring;
	LineRenderer lineRenderer;
	int index = 0;
	int qNum;
	string testName;
	string[] questions;


    // Start is called before the first frame update
    void Start()
    {
		goalsScored1 = new Fraction();
		correctAnswers1 = new Fraction();
		goalsScored2 = new Fraction();
		correctAnswers2 = new Fraction();
		lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount(2);
		lineRenderer.GetComponent<Renderer>().enabled = true;
		lineRenderer.enabled = false;
        passSuccesful = false;
		scoring = false;
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
		endPanel.SetActive(false);
		getPositions();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startGame()
    {
		test.SetActive(false);
		endPanel.SetActive(false);
		index = PlayerPrefs.GetInt("playingIndex", 0);
		if(index > 0){
			testName = PlayerPrefs.GetString(index+"", "").Split(':')[0].Replace("{", "");
			string[] temp = PlayerPrefs.GetString(index+"", "").Replace("\n", "").Replace("\t", "").Split('{');
			questions = new string[temp.Length-2];
			for(int i = 2; i < temp.Length; i++)
				questions[i-2] = temp[i];
		}
		qNum = 0;
		
		questionsLeft.GetComponent<Text>().text = "Questions Left: "+questions.Length;
		scoreP1.GetComponent<Text>().text = "0";
		scoreP2.GetComponent<Text>().text = "0";
        playing = true;
        stage = -1; //-1 is not playing, 0 is defending, 1 is attacking, 2 is striking 
        ball.GetComponent<BallController>().resetPos();
        float r = rand.Next(0, 2);
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
		for(int i = 0; i < players.Length; i++){
			players[i].SetActive(true);
			if(i<3){
				pos = players[i].transform.position;
				team1DefensePositions[i] = pos;
				pos.x += 5f;
				team1AttackPositions[i] = pos;
			} else {
				pos = players[i].transform.position;
				team2DefensePositions[i-3] = pos;
				pos.x -= 5f;
				team2AttackPositions[i-3] = pos;
			}
		}
    }

    public void reset() {
        StopAllCoroutines();
        stage = -1;
		scoring = false;
		aimAssist.SetActive(true);
        enemyDefender.SetActive(true);
        playerWithBall.SetActive(true);
        Camera.main.orthographic = true;
        Camera.main.fieldOfView = cameraFOV;
        GameObject camera = GameObject.Find("Main Camera");
        camera.transform.position = cameraPos;
        camera.transform.rotation = cameraRot; 
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

        shufflePlayers(1);
        shufflePlayers(2);

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
        stage++;
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
        if(index > 0)
			showQuestion();
		else
			checkAnswer(true);
    }

    public void showQuestion()
    {
		if(qNum >= questions.Length){
			StopAllCoroutines();
			EndGame();
			return;
		}
		questionsLeft.GetComponent<Text>().text = "Questions Left: "+(questions.Length-(qNum+1));
        test.SetActive(true);
        var tc = test.GetComponent<TestController>();
		tc.SetData(questions[qNum].Split(':')[0], questions[qNum].Split('[')[1].Split(']')[0].Split(','), questions[qNum].Split(']')[1].Split(':')[1].Replace("}", ""));
	}
	
	public void EndGame()
    {
       	endPanel.SetActive(true);
		endPanel.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Text>().text = "Points: "+ int.Parse(scoreP1.GetComponent<Text>().text);
		endPanel.transform.GetChild(2).GetChild(1).gameObject.GetComponent<Text>().text = "Points: "+ int.Parse(scoreP2.GetComponent<Text>().text);
		endPanel.transform.GetChild(1).GetChild(2).gameObject.GetComponent<Text>().text = "Goals: "+ goalsScored1.getFraction()+" ("+goalsScored1.getPercentage()+"%)";
		endPanel.transform.GetChild(2).GetChild(2).gameObject.GetComponent<Text>().text = "Goals: "+ goalsScored2.getFraction()+" ("+goalsScored2.getPercentage()+"%)";
		endPanel.transform.GetChild(1).GetChild(3).gameObject.GetComponent<Text>().text = "Questions: "+ correctAnswers1.getFraction()+" ("+correctAnswers1.getPercentage().ToString("F2")+"%)";
		endPanel.transform.GetChild(2).GetChild(3).gameObject.GetComponent<Text>().text = "Questions: "+ correctAnswers2.getFraction()+" ("+correctAnswers2.getPercentage().ToString("F2")+"%)";
    }

    public void checkAnswer(bool correct)
    {
        test.SetActive(false);
		if(currentPlayer == 1)
			correctAnswers1.increaseDenominator(1);
		else
			correctAnswers2.increaseDenominator(1);
        if (correct)
        {
            Debug.Log("Correct");
			aimAssist.SetActive(false);
            if (stage == 0)
            {
                if (currentPlayer == 1) //remove goal so they can see
                {
					correctAnswers1.increaseNumerator(1);
                    goal1.SetActive(false);
                }
                else
                {
					correctAnswers2.increaseNumerator(1);
                    goal2.SetActive(false);
                }
            }
            StopAllCoroutines();
            StartCoroutine(Wait());
        }
        else
        {
            changeTurns();
        }
		qNum++;
    }

    public void changeTurns()
    {
        enemyDefender.SetActive(true);
        playerWithBall.gameObject.SetActive(true);
		scoring = false;
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
        float dist;
        string name = "";
        if (stage == 0) return;
        if (stage == 1) dist = 8;
        else dist = 0;
        GameObject auxPlayer1, auxPlayer2;
        if (team == 1) {
            if (id == 1)
            {
                auxPlayer1 = GameObject.Find("Player1B");
                auxPlayer2 = GameObject.Find("Player1C");
                name = "Player2A";
            }
            else if (id == 2)
            {
                auxPlayer1 = GameObject.Find("Player1A");
                auxPlayer2 = GameObject.Find("Player1C");
                name = "Player2B";
            }
            else
            {
                auxPlayer1 = GameObject.Find("Player1A");
                auxPlayer2 = GameObject.Find("Player1B");
                name = "Player2C";
            }
            auxPlayer1.transform.position += new Vector3(dist, 0, 0);
            auxPlayer2.transform.position += new Vector3(dist, 0, 0);
        }
        else
        {
            if (id == 1)
            {
                auxPlayer1 = GameObject.Find("Player2B");
                auxPlayer2 = GameObject.Find("Player2C");
                name = "Player1A";
            }
            else if (id == 2)
            {
                auxPlayer1 = GameObject.Find("Player2A");
                auxPlayer2 = GameObject.Find("Player2C");
                name = "Player1B";
            }
            else
            {
                auxPlayer1 = GameObject.Find("Player2A");
                auxPlayer2 = GameObject.Find("Player2B");
                name = "Player1C";
            }
            auxPlayer1.transform.position -= new Vector3(dist, 0, 0);
            auxPlayer2.transform.position -= new Vector3(dist, 0, 0);
        }
        if (stage == 3)
        {
            enemyDefender = GameObject.Find(name);
            enemyDefender.SetActive(false);
        }
    }


    public bool shoot(){
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 dir = ((ball.transform.position + (ray.direction) * 1000f) - ray.origin).normalized * 7.2f;
        if (Input.GetMouseButtonDown(0)){
            Debug.Log("Clicked");
            Debug.DrawRay(ray.origin, dir, Color.cyan, 200f, true);
            if (Physics.Raycast(ray.origin, dir, out hit, 200f)){
                if (hit.collider.tag == "Ball")
                {
					if(currentPlayer == 1)
						goalsScored1.increaseDenominator(1);
					else
						goalsScored2.increaseDenominator(1);
					
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
			lineRenderer.enabled = false;
            return true;
		}
        else
        {
            if (Physics.Raycast(ray.origin, dir, out hit, 200f))
            {
                if (hit.collider.tag == "Ball" && aimAssist.GetComponent<Toggle>().isOn)
                {
					lineRenderer.enabled = true;
					Vector3 strike = Vector3.Reflect(hit.normal.normalized, Vector3.up).normalized;
					Quaternion quat = Quaternion.AngleAxis(180, Vector3.right);
					strike = quat * strike;
					strike = Vector3.Reflect(strike, Vector3.right).normalized;
					strike = Vector3.Reflect(strike, Vector3.up).normalized;
					Vector3 endPoint = hit.point + (2f * strike); 
					lineRenderer.SetPosition(0, hit.point);
					lineRenderer.SetPosition(1, endPoint);
                }
                else
                {
                    lineRenderer.enabled = false;
                }
            }
            return false;
        }
	}

    IEnumerator HoldClick(Vector3 dir, Vector3 hitPoint)
    {
        timePressed = Time.time;
        yield return new WaitUntil(() => releasedClick());
        timePressed = Time.time - timePressed;
        Vector3 strike = Vector3.Reflect(dir.normalized, Vector3.up);
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
		scoring = true;
		passSuccesful = true;
		StopAllCoroutines();
					
		int score;
		if(team == 1){
			goalsScored1.increaseNumerator(1);
			score = int.Parse(scoreP1.GetComponent<Text>().text);
			score+=50;
			scoreP1.GetComponent<Text>().text = score + "";
		} else {
			goalsScored2.increaseNumerator(1);
			score = int.Parse(scoreP2.GetComponent<Text>().text);
			score+=50;
			scoreP2.GetComponent<Text>().text = score + "";
        }
		GameObject.Find("ScoreText").GetComponent<ScoreText>().show(currentPlayer);
    }
    
    public void shufflePlayers(int team)
    {
        int count = 0;
        GameObject player;
        BallController bc = ball.GetComponent<BallController>();
        Vector3 center = ball.GetComponent<BallController>().startPos + new Vector3(0, -1.4f, 0);
        Vector3 dir;
        int dist = 10;
        do
        {
            if (team == 1)
            {
                dir = new Vector3(1, 0, 0);
                if (stage <= 0)
                {
                    player = GameObject.Find("Player1A");
                    player.transform.position = team1DefensePositions[0];
                    player.transform.position += new Vector3(rand.Next(-dist, dist)/10, 0, rand.Next(-dist, dist)/10);
                    player = GameObject.Find("Player1B");
                    player.transform.position = team1DefensePositions[1];
                    player.transform.position += new Vector3(rand.Next(-dist, dist)/10, 0, rand.Next(-dist, dist)/10);

                }
                else
                {
                    player = GameObject.Find("Player1A");
                    player.transform.position = team1AttackPositions[0];
                    player.transform.position += new Vector3(rand.Next(-dist, dist)/10, 0, rand.Next(-dist, dist) / 10);
                    player = GameObject.Find("Player1B");
                    player.transform.position = team1AttackPositions[1];
                    player.transform.position += new Vector3(rand.Next(-dist, dist) / 10, 0, rand.Next(-dist, dist) / 10);
                }
            }
            else
            {
                dir = new Vector3(-1, 0, 0);
                if (stage <= 0)
                {
                    player = GameObject.Find("Player2A");
                    player.transform.position = team2DefensePositions[0];
                    player.transform.position += new Vector3(rand.Next(-dist, dist) / 10, 0, rand.Next(-dist, dist) / 10);
                    player = GameObject.Find("Player2B");
                    player.transform.position = team2DefensePositions[1];
                    player.transform.position += new Vector3(rand.Next(-dist, dist) / 10, 0, rand.Next(-dist, dist) / 10);

                }
                else
                {
                    player = GameObject.Find("Player2A");
                    player.transform.position = team2AttackPositions[0];
                    player.transform.position += new Vector3(rand.Next(-dist, dist) / 10, 0, rand.Next(-dist, dist) / 10);
                    player = GameObject.Find("Player2B");
                    player.transform.position = team2AttackPositions[1];
                    player.transform.position += new Vector3(rand.Next(-dist, dist) / 10, 0, rand.Next(-dist, dist) / 10);
                }
            }
            count++;
        } while ((Physics.Raycast(center + new Vector3(0, 0, 0.5f), dir, 10f) || Physics.Raycast(center + new Vector3(0, 0, -0.5f), dir, 10f)) && count < 5);
	}
	
	public void quitMultiplayer(){
		SceneManager.LoadScene("FreeKicksMenu");
	}
	
	public void restart(){
		SceneManager.LoadScene("Multiplayer");
	}
}
