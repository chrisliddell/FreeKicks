using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SingleplayerController : MonoBehaviour
{
    GameObject ball;
    GameObject test;
    GameObject goal;
	public GameObject aimAssist;
    public GameObject striker;
    public GameObject goalkeeper;
    public GameObject barrier;
	public GameObject scoreText; 
    public Slider powerSlider;
    System.Random rand;
    private IEnumerator coroutine;
    Vector3 cameraPos;
    Quaternion cameraRot;
    float cameraFOV;
    public float power = 300f;
    public float maxPower = 1000f;
    public float timePressed = 0f;
    public int currentAnswer;
	public bool failedShot;
	LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    { 
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
        int r = rand.Next(0, 4);
        Debug.Log("Resetting game");
        striker.GetComponent<StrikerController>().moveStriker();

        barrier.GetComponent<BarrierController>().setSize(r);
        barrier.GetComponent<BarrierController>().moveBarrier();

        ball.GetComponent<BallController>().resetPos();
        striker.GetComponent<StrikerController>().getBall(ball);
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
			aimAssist.SetActive(false);
            StopAllCoroutines();
            StartCoroutine(Wait());
        }
        else
        {
            reset();
        }
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
    }
	
	public void quitSingleplayer(){
		SceneManager.LoadScene("UserInterface");
	}
}
