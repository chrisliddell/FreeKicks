using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SingleplayerController : MonoBehaviour
{
	Fraction goalsScored;
	Fraction correctAnswers;
    GameObject ball;
    GameObject test;
    GameObject goal;
	public GameObject aimAssist;
    public GameObject striker;
    public GameObject goalkeeper;
    public GameObject barrier;
	public GameObject scoreText; 
    public Slider powerSlider;
	public GameObject endPanel;
	public GameObject questionsLeft;
    System.Random rand;
    private IEnumerator coroutine;
    Vector3 cameraPos;
    Quaternion cameraRot;
    float cameraFOV;
    public float power = 300f;
    public float maxPower = 1000f;
    public float timePressed = 0f;
	public bool failedShot;
	LineRenderer lineRenderer;
	int index = 0;
	int qNum;
	string testName;
	string[] questions;
	bool shuffle;

	
    // Start is called before the first frame update
    void Start()
    {
		shuffle = PlayerPrefs.GetInt("shuffleTest", 0) == 1 ? true:false;
		goalsScored = new Fraction();
		correctAnswers = new Fraction();
		lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.SetVertexCount(2);
		lineRenderer.GetComponent<Renderer>().enabled = true;
		lineRenderer.enabled = false;
		failedShot = false;
        powerSlider.gameObject.SetActive(false);
        GameObject camera = GameObject.Find("Main Camera");
        goal = GameObject.Find("goal2");
        ball = GameObject.Find("Ball");
        cameraPos = camera.transform.position;
        cameraRot = camera.transform.rotation;
        cameraFOV = Camera.main.fieldOfView;
        rand = new System.Random();
        test = GameObject.Find("Test");
        test.SetActive(false);
		endPanel.SetActive(false);
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
		if(shuffle) shuffleTest();
		scoreText.GetComponent<Text>().text = "0";
        int r = rand.Next(0, 4);
        Debug.Log("Starting game");
        striker.GetComponent<StrikerController>().moveStriker();

        barrier.GetComponent<BarrierController>().setSize(r);
        barrier.GetComponent<BarrierController>().moveBarrier();

        ball.GetComponent<BallController>().resetPos();
        striker.GetComponent<StrikerController>().getBall(ball);
    }

    public void reset()
    {
		failedShot = false;
		striker.gameObject.SetActive(true);
		aimAssist.SetActive(true);
        Debug.Log("Resetting game");
        striker.GetComponent<StrikerController>().moveStriker();
		switch(rand.Next(0, 10)){
			case 0:
			case 1: 
				barrier.GetComponent<BarrierController>().setSize(0);
				break;
			case 2:
			case 3: 
				barrier.GetComponent<BarrierController>().setSize(1);
				break;
			case 4:
			case 5:
			case 6:
				barrier.GetComponent<BarrierController>().setSize(2);
				break;
			case 7:
			case 8:
				barrier.GetComponent<BarrierController>().setSize(3);
				break;
			default:
				barrier.GetComponent<BarrierController>().setSize(4);
				break;
		}
        barrier.GetComponent<BarrierController>().moveBarrier();
        ball.GetComponent<BallController>().resetPos();
        striker.GetComponent<StrikerController>().getBall(ball);
    }
	
	//swap shuffle
	public void shuffleTest(){
		 Debug.Log("OLD TEST:\n");
		foreach(string s in questions) Debug.Log(s);
		for(int i = 0; i < questions.Length; i++){
			int r = rand.Next(0, questions.Length);
			string temp = questions[i];
			questions[i] = questions[r];
			questions[r] = temp;
		}
		Debug.Log("NEW TEST:\n");
		foreach(string s in questions) Debug.Log(s);
	}

    public void shootMode(GameObject player)
    {
		striker.gameObject.SetActive(true);
        Debug.Log("Player received ball.");
        GameObject camera = GameObject.Find("Main Camera");
        Camera.main.orthographic = false;
        Camera.main.fieldOfView = 30f;
        camera.transform.position = ball.transform.position + new Vector3(-6, 2, 0);
        camera.transform.LookAt(goal.transform);
        striker.gameObject.SetActive(false);
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

    public void checkAnswer(bool correct)
    {
        test.SetActive(false);
		correctAnswers.increaseDenominator(1);
        if (correct)
        {
			correctAnswers.increaseNumerator(1);
			aimAssist.SetActive(false);
            StopAllCoroutines();
            StartCoroutine(Wait());
        }
        else
        {
            reset();
        }
		qNum++;
    }
	
	public void EndGame()
    {
       	endPanel.SetActive(true);
		endPanel.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Points: "+ int.Parse(scoreText.GetComponent<Text>().text);
		endPanel.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Goals: "+ goalsScored.getNumerator();
		endPanel.transform.GetChild(3).gameObject.GetComponent<Text>().text = "Questions: "+ correctAnswers.getFraction()+" ("+correctAnswers.getPercentage().ToString("F2")+"%)";
	}
	   
	IEnumerator Wait()
    {
        print(Time.time);
        yield return new WaitUntil(() => shoot());
        test.SetActive(false);
        print(Time.time);
    }

    public bool shoot(){
		failedShot = true;
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 dir = ((ball.transform.position + (ray.direction) * 1000f) - ray.origin).normalized * 7.2f;
        if (Input.GetMouseButtonDown(0)){
            Debug.Log("Clicked");
            Debug.DrawRay(ray.origin, dir, Color.cyan, 200f, true);
            if (Physics.Raycast(ray.origin, dir, out hit, 200f)){
                if (hit.collider.tag == "Ball")
                {
                    Debug.Log("Hit ball");
					goalsScored.increaseDenominator(1);
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
        yield return new WaitForSeconds(4);
        if (!failedShot)
        {
            Debug.Log("Shot succesful");
			reset();
        }
        else
        {
            Debug.Log("Failed shot");
			reset();
        }
    }
	
	public void scoreGoal()
	{
		StopAllCoroutines();
		int score = int.Parse(scoreText.GetComponent<Text>().text);
		score+=50;
		scoreText.GetComponent<Text>().text = score + "";
        GameObject.Find("ScoreText").GetComponent<ScoreText>().show();
		goalsScored.increaseNumerator(1);
    }
	
	public void quitSingleplayer(){
		SceneManager.LoadScene("FreeKicksMenu");
	}
		
	public void restart(){
		SceneManager.LoadScene("Singleplayer");
	}
}
